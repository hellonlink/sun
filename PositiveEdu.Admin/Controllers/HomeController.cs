using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PositiveEdu.Admin.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SetPassword()
        {
            return View();
        }

        [ValidateAntiForgeryToken, ValidateInput(false), HttpPost]
        public ActionResult SetPassword(string oldPsw, string newPsw, string confirmPsw)
        {
            if (string.IsNullOrWhiteSpace(newPsw))
            {
                ModelState.AddModelError("Password", "新密码不能为空");
                return View();
            }

            if (newPsw.Trim() != confirmPsw.Trim())
            {
                ModelState.AddModelError("Password", "两次密码不一样");
                return View();
            }

            var item = DB.ADMIN.Where(x => x.UUID == AuthAdmin.AdminId).FirstOrDefault();
            if (item.PASSWORD != oldPsw.Trim())
            {
                ModelState.AddModelError("Password", "旧密码错误");
                return View();
            }

            item.PASSWORD = newPsw.Trim();
            item.UPDATE_TIME = DateTime.Now;
            DB.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}