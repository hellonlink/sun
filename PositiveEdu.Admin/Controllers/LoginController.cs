using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PositiveEdu.DAL;

namespace PositiveEdu.Admin.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.ReturnUrl = Request.QueryString["ReturnUrl"];
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password, string returnUrl)
        {
            using (PeContext db = new PeContext())
            {
                var admin = db.ADMIN.Where(x => x.USER_NAME == username.Trim()).FirstOrDefault();


                


                var a2 = db.ROLE_FUNCTION_RELATION.ToList();
                var a3 = db.ROLE_FUNCTIONS.ToList();
                var a4 = db.ADMIN_ROLE_RELATION.ToList();
                var a = db.ADMIN.ToList();
                if (admin == null)
                {
                    ModelState.AddModelError("Username", "用户不存在");
                    ViewBag.ReturnUrl = returnUrl;
                    return View("Index");
                }

                if (password.Trim() != admin.PASSWORD)
                {
                    ModelState.AddModelError("Password", "密码错误");
                    ViewBag.ReturnUrl = returnUrl;
                    return View("Index");
                }

                AuthAdmin authAdmin = new AuthAdmin()
                {
                    AdminId = admin.UUID,
                    UserName = admin.USER_NAME,
                    RealName = admin.REAL_NAME,
                    Mobile = admin.PHONE,
                };

                //var a = db.ADMIN.ToList();
                //var a1 = db.ADMIN_ROLE_RELATION.ToList();


                //角色权限，用户与角色有对应关系
                if (admin.ADMIN_ROLE_RELATION.Count > 0)
                    authAdmin.FirstRoleName = admin.ADMIN_ROLE_RELATION.FirstOrDefault().ROLE.NAME;

                authAdmin.FunctionsId = new List<int>();
                foreach (var adminRole in admin.ADMIN_ROLE_RELATION)
                {//查询出所有
                    var funs = adminRole.ROLE.ROLE_FUNCTION_RELATION.Select(x => x.FUNCTION_ID.Value).ToList();
                    authAdmin.FunctionsId = authAdmin.FunctionsId.Union(funs).ToList();
                }
                //对应方法名就是权限
                string json = JsonConvert.SerializeObject(authAdmin);

                FormsAuthentication.SetAuthCookie(json, false);
            }
            //白名单
            if (!string.IsNullOrWhiteSpace(returnUrl))
                return Redirect(returnUrl);
            else
                return Redirect(FormsAuthentication.DefaultUrl);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect(FormsAuthentication.LoginUrl);
        }
    }
}