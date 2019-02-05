using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PositiveEdu.WebApi.ModelsRes;
using PositiveEdu.DAL;
using PositiveEdu.Common;

namespace PositiveEdu.WebApi.Controllers
{
    /// <summary>
    /// 用户账户
    /// </summary>
    [RoutePrefix("api/Account")]
    public class AccountController : BaseApiController
    {
        /// <summary>
        /// 发送注册验证码
        /// </summary>
        /// <param name="mobile">手机号码</param>
        /// <returns>发送结果</returns>
        [Route("SignUp/SMS"), HttpPost]
        public HttpResponseMessage SendSignUpSMS([FromBody] string mobile)
        {
            if (string.IsNullOrWhiteSpace(mobile))
                return Request.CreateResponse(HttpStatusCode.InternalServerError, MakeMessage("手机号不能为空"));


            string code = WebFunctions.GetRandomNumber(4);
            string content = "注册验证码(" + code + ")，用于验证(" + ConstValue.SmsEffectiveTime.ToString() + "分钟内有效)。请勿向任何人提供您收到的验证码";

            string result = string.Empty;
            try
            {
                result = SmsSender.Send(mobile, content);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, MakeMessage("发送失败:" + ex.Message));
            }

            SMSLog newSms = new SMSLog()
            {
                code = code,
                phone = mobile,
                smsType = ConstValue.SmsType.SignUp,
                status = ConstValue.SmsStatus.Pending,
                textContent = content,
                createTime = DateTime.Now,
                result = result,
            };
            DB.SMSLog.Add(newSms);
            DB.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK, MakeMessage("已发送"));
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("SignUp"), HttpPost]
        public HttpResponseMessage SignUp(ReqModelSignUp model)
        {
            if (string.IsNullOrWhiteSpace(model.Mobile) || string.IsNullOrWhiteSpace(model.Password))
                return Request.CreateResponse(HttpStatusCode.InternalServerError, MakeMessage("手机号或密码不正确"));

            var smsLogs = DB.SMSLog.Where(x => x.phone == model.Mobile && x.status == ConstValue.SmsStatus.Pending && x.smsType == ConstValue.SmsType.SignUp).OrderByDescending(x => x.createTime).ToList();
            var available = smsLogs.Where(x => x.createTime >= DateTime.Now.AddMinutes(-ConstValue.SmsEffectiveTime)).FirstOrDefault();
            if (available != null)
            {
                if (available.code == model.ValidateCode)
                {
                    var customer = DB.Customer.Where(x => x.MOBILE == model.Mobile).FirstOrDefault();
                    if (customer == null)
                    {
                        //新用户
                        Customer cus = new Customer()
                        {
                            MOBILE = model.Mobile,
                            USER_PASSWORD = model.Password,
                            CREATE_TIME = DateTime.Now,
                        };
                        DB.Customer.Add(cus);
                        DB.SaveChanges();
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(customer.USER_PASSWORD))
                        {
                            //手机登录过的用户
                            customer.USER_PASSWORD = model.Password;
                            DB.SaveChanges();
                        }
                        else
                            return Request.CreateResponse(HttpStatusCode.InternalServerError, MakeMessage("用户已存在"));
                    }

                    ClearSmsLogs(smsLogs, available);
                    return Request.CreateResponse(HttpStatusCode.OK, MakeMessage("注册成功"));
                }
            }

            return Request.CreateResponse(HttpStatusCode.InternalServerError, MakeMessage("验证码不正确"));
        }

        /// <summary>
        /// 发送登录验证码
        /// </summary>
        /// <param name="mobile">手机号码</param>
        /// <returns>发送结果</returns>
        [Route("SignIn/SMS"), HttpPost]
        public HttpResponseMessage SendSignInSMS([FromBody] string mobile)
        {
            if (string.IsNullOrWhiteSpace(mobile))
                return Request.CreateResponse(HttpStatusCode.InternalServerError, MakeMessage("手机号不能为空"));

            string code = WebFunctions.GetRandomNumber(4);
            string content = "登录验证码(" + code + ")，用于验证(" + ConstValue.SmsEffectiveTime.ToString() + "分钟内有效)。请勿向任何人提供您收到的验证码";

            string result = string.Empty;
            try
            {
                result = SmsSender.Send(mobile, content);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, MakeMessage("发送失败:" + ex.Message));
            }

            SMSLog newSms = new SMSLog()
            {
                code = code,
                phone = mobile,
                smsType = ConstValue.SmsType.Login,
                status = ConstValue.SmsStatus.Pending,
                textContent = content,
                createTime = DateTime.Now,
                result = result,
            };
            DB.SMSLog.Add(newSms);
            DB.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK, MakeMessage("已发送"));
        }

        /// <summary>
        /// 发送忘记密码验证码
        /// </summary>
        /// <param name="mobile">手机号码</param>
        /// <returns>发送结果</returns>
        [Route("Forget/SMS"), HttpPost]
        public HttpResponseMessage SendForgetSMS([FromBody] string mobile)
        {
            if (string.IsNullOrWhiteSpace(mobile))
                return Request.CreateResponse(HttpStatusCode.InternalServerError, MakeMessage("手机号不能为空"));

            string code = WebFunctions.GetRandomNumber(4);
            string content = "登录验证码(" + code + ")，用于修改密码(" + ConstValue.SmsEffectiveTime.ToString() + "分钟内有效)。请勿向任何人提供您收到的验证码";

            string result = string.Empty;
            try
            {
                result = SmsSender.Send(mobile, content);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, MakeMessage("发送失败:" + ex.Message));
            }

            SMSLog newSms = new SMSLog()
            {
                code = code,
                phone = mobile,
                smsType = ConstValue.SmsType.Forget,
                status = ConstValue.SmsStatus.Pending,
                textContent = content,
                createTime = DateTime.Now,
                result = result,
            };
            DB.SMSLog.Add(newSms);
            DB.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK, MakeMessage("已发送"));
        }

        /// <summary>
        /// 忘记密码 修改密码
        /// </summary>
        /// <param name="model">手机号、新密码、验证码</param>
        /// <returns>修改结果</returns>
        [Route("Forget/ChangePassword"), HttpPost]
        public HttpResponseMessage ChangePassword([FromBody] ReqModelForgetChangePassword model)
        {
            if (string.IsNullOrWhiteSpace(model.Mobile) || string.IsNullOrWhiteSpace(model.Password))
                return Request.CreateResponse(HttpStatusCode.InternalServerError, MakeMessage("手机号或密码不正确"));

            var smsLogs = DB.SMSLog.Where(x => x.phone == model.Mobile && x.status == ConstValue.SmsStatus.Pending && x.smsType == ConstValue.SmsType.Forget).OrderByDescending(x => x.createTime).ToList();
            var available = smsLogs.Where(x => x.createTime >= DateTime.Now.AddMinutes(-ConstValue.SmsEffectiveTime)).FirstOrDefault();
            if (available != null)
            {
                if (available.code == model.ValidateCode)
                {
                    var customer = DB.Customer.Where(x => x.MOBILE == model.Mobile).FirstOrDefault();
                    if (customer == null)
                        return Request.CreateResponse(HttpStatusCode.InternalServerError, MakeMessage("用户不存在"));

                    customer.USER_PASSWORD = model.Password;
                    DB.SaveChanges();

                    ClearSmsLogs(smsLogs, available);
                    return Request.CreateResponse(HttpStatusCode.OK, MakeMessage("密码修改成功"));
                }
            }

            return Request.CreateResponse(HttpStatusCode.InternalServerError, MakeMessage("验证码不正确"));
        }

        private void ClearSmsLogs(List<SMSLog> matchs, SMSLog available)
        {
            foreach (var sms in matchs)
            {
                sms.status = ConstValue.SmsStatus.Invalid;
            }

            if (available != null)
                available.status = ConstValue.SmsStatus.Used;

            DB.SaveChanges();
        }
    }
}
