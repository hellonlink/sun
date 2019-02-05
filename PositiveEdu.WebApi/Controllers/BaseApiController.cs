using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using PositiveEdu.DAL;
using PositiveEdu.Common;
using System.Security.Claims;

namespace PositiveEdu.WebApi.Controllers
{
    public class BaseApiController : ApiController
    {
        private PeContext db = null;
        protected PeContext DB
        {
            get
            {
                if (db == null)
                    db = new PeContext();

                return db;
            }
        }

        protected AuthCustomer AuthCustomer
        {
            get
            {
                if (User.Identity.IsAuthenticated)
                {
                    try
                    {
                        var identity = (ClaimsIdentity)User.Identity;
                        var sub = identity.Claims.Where(x => x.Type == "sub").FirstOrDefault();
                        AuthCustomer auth = JsonConvert.DeserializeObject<AuthCustomer>(sub.Value);
                        return auth;
                    }
                    catch
                    {
                        return null;
                    }
                }
                else
                    return null;
            }
        }

        public BaseApiController()
        {
            
        }

        protected Dictionary<string, string> MakeMessage(string message)
        {
            Dictionary<string, string> msg = new Dictionary<string, string>();
            msg.Add("message", message);
            return msg;
        }

        protected override void Dispose(bool disposing)
        {
            if (db != null)
            {
                db.Dispose();
                db = null;
            }

            base.Dispose(disposing);
        }

        protected string ToAbsoluteResourceUrl(string url)
        {
            return WebFunctions.ToAbsoluteResourceUrl(url);
        }

        protected string WrapToHtmlNewLine(string content)
        {
            return WebFunctions.WrapToHtmlNewLine(content);
        }

        protected string ContentToHtml(string content)
        {
            return WebFunctions.ContentToHtml(content);
        }
    }
}
