using Newtonsoft.Json;
using PositiveEdu.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PositiveEdu.Admin.Controllers
{
    [Authorize]
    public abstract class BaseController : Controller
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

        protected override void Dispose(bool disposing)
        {
            if (db != null)
            {
                db.Dispose();
                db = null;
            }

            base.Dispose(disposing);
        }

        protected const string richEditorViewPath = "~/Views/RichEditor/RichEditor.cshtml";

        protected AuthAdmin AuthAdmin
        {
            get
            {
                if (User.Identity.IsAuthenticated)
                {
                    try
                    {
                        AuthAdmin auth = JsonConvert.DeserializeObject<AuthAdmin>(User.Identity.Name);
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


        [ChildActionOnly]
        public ActionResult Navigation()
        {
            ViewBag.AuthAdmin = AuthAdmin;

            var query = DB.ROLE_FUNCTIONS.AsNoTracking().Where(x => x.IS_DELETE != 1).AsQueryable();
            var functions = query.Where(x => AuthAdmin.FunctionsId.Contains(x.UUID) || x.TYPE == 0).ToList();

            return View("_Navigation", functions);
        }

    }
}