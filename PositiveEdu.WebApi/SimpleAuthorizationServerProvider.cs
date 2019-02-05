using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using Newtonsoft.Json;
using PositiveEdu.DAL;
using PositiveEdu.Common;

namespace PositiveEdu.WebApi
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            /*
             * 对用户名、密码进行数据校验，这里我们省略
            using (AuthRepository _repo = new AuthRepository())
            {
                IdentityUser user = await _repo.FindUser(context.UserName, context.Password);

                if (user == null)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }
            }*/

            string loginType = "1";

            System.IO.StreamReader sr = new System.IO.StreamReader(context.Request.Body);
            sr.DiscardBufferedData();
            sr.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
            sr.BaseStream.Position = 0;
            string param = sr.ReadToEnd();

            string[] arr = param.Split(new char[] { '&' });
            foreach(var pair in arr)
            {
                string[] kv = pair.Split(new char[] { '=' });
                if (kv.Length == 2)
                {
                    if (kv[0].ToLower() == "logintype")
                    {
                        loginType = kv[1];
                    }
                }
            }

            if (loginType != "2")
                NormalLogin(context);
            else
                MobileLogin(context);
        }

        private void NormalLogin(OAuthGrantResourceOwnerCredentialsContext context)
        {
            AuthCustomer auth = new AuthCustomer();
            using (PeContext db = new PeContext())
            {
                var customer = db.Customer.Where(x => x.MOBILE == context.UserName.Trim()).FirstOrDefault();
                if (customer == null)
                {
                    context.SetError("invalid_grant", "用户名或密码不正确。");
                    return;
                }

                if (customer.USER_PASSWORD.Trim() != context.Password.Trim())
                {
                    context.SetError("invalid_grant", "用户名或密码不正确。");
                    return;
                }

                customer.LAST_LOGIN_TIME = DateTime.Now;
                db.SaveChanges();

                auth = new AuthCustomer()
                {
                    CustomerId = customer.CUSTOMER_ID,
                    Mobile = customer.MOBILE,
                    RealName = customer.REAL_NAME,
                };
            }

            string json = JsonConvert.SerializeObject(auth);

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);

            identity.AddClaim(new Claim("sub", json));
            identity.AddClaim(new Claim("role", "user"));

            context.Validated(identity);
        }

        private void MobileLogin(OAuthGrantResourceOwnerCredentialsContext context)
        {
            AuthCustomer auth = new AuthCustomer();
            using (PeContext db = new PeContext())
            {
                var smsLogs = db.SMSLog.Where(x => x.phone == context.UserName && x.status == ConstValue.SmsStatus.Pending && x.smsType == ConstValue.SmsType.Login).OrderByDescending(x => x.createTime).ToList();
                var available = smsLogs.Where(x => x.createTime >= DateTime.Now.AddMinutes(-ConstValue.SmsEffectiveTime)).FirstOrDefault();
                if (available != null)
                {
                    if (context.Password == available.code)
                    {
                        var customer = db.Customer.Where(x => x.MOBILE == context.UserName).FirstOrDefault();
                        if (customer == null)
                        {
                            Customer cus = new Customer()
                            {
                                MOBILE = context.UserName,
                                CREATE_TIME = DateTime.Now,
                            };
                            db.Customer.Add(cus);
                            db.SaveChanges();

                            customer = db.Customer.Where(x => x.MOBILE == context.UserName).FirstOrDefault();
                        }

                        customer.LAST_LOGIN_TIME = DateTime.Now;
                        db.SaveChanges();

                        ClearSmsLogs(smsLogs, available, db);

                        auth = new AuthCustomer()
                        {
                            CustomerId = customer.CUSTOMER_ID,
                            Mobile = customer.MOBILE,
                            RealName = customer.REAL_NAME,
                        };

                        string json = JsonConvert.SerializeObject(auth);

                        var identity = new ClaimsIdentity(context.Options.AuthenticationType);

                        identity.AddClaim(new Claim("sub", json));
                        identity.AddClaim(new Claim("role", "user"));

                        context.Validated(identity);
                        return;
                    }
                }
            }

            context.SetError("invalid_grant", "验证码不正确。");
        }

        private void ClearSmsLogs(List<SMSLog> matchs, SMSLog available, PeContext db)
        {
            foreach (var sms in matchs)
            {
                sms.status = ConstValue.SmsStatus.Invalid;
            }

            if (available != null)
                available.status = ConstValue.SmsStatus.Used;

            db.SaveChanges();
        }
    }

    public class AuthCustomer
    {
        public int CustomerId { get; set; }

        public string Mobile { get; set; }

        public string RealName { get; set; }
    }
}