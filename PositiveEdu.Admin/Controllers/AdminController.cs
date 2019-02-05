using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PositiveEdu.DAL;
using PositiveEdu.Common;

namespace PositiveEdu.Admin.Controllers
{
    public class AdminController : BaseController
    {
        public ActionResult Index(int page = 1)
        {
            int pageSize = 15;
            var query = DB.ADMIN.AsNoTracking().OrderByDescending(x => x.CRATE_TIME);
            var result = query.ToPagedList(page, pageSize);
            return View(result);
        }

        public ActionResult Create()
        {
            ViewBag.Roles = DB.ROLE.AsNoTracking().OrderBy(x => x.UUID).ToList();

            return View();
        }


        public ActionResult ROLE_FUNCTIONSCreate()
        {

            ViewBag.Roles = DB.ROLE_FUNCTIONS.AsNoTracking().Where(x => x.TYPE == 0).ToList();

            return View();
        }

        [ValidateAntiForgeryToken, ValidateInput(false), HttpPost]
        public ActionResult ROLE_FUNCTIONSCreate(string NAME, string Controller, string Action)
        {



            var _ROLE_FUNCTIONS = new ROLE_FUNCTIONS();
            _ROLE_FUNCTIONS.NAME = NAME;
            _ROLE_FUNCTIONS.Controller = Controller;

            if (Request.Form["role"] == null)
            {

                _ROLE_FUNCTIONS.TYPE = 0;

            }
            else
            {
                _ROLE_FUNCTIONS.Action = Action;
                _ROLE_FUNCTIONS.TYPE = 1;
                _ROLE_FUNCTIONS.PARENT = Convert.ToInt32(Request.Form["role"]);
            }
            _ROLE_FUNCTIONS.IS_DELETE = 0;
            _ROLE_FUNCTIONS.CREATE_TIME = DateTime.Now;
            var a = DB.ROLE_FUNCTIONS.Where(x => x.TYPE == _ROLE_FUNCTIONS.TYPE && x.Controller == _ROLE_FUNCTIONS.Controller).ToList();
            if (a.Count == 0 && _ROLE_FUNCTIONS.TYPE == 0)
            {
                var c = DB.ROLE_FUNCTIONS.Where(x => x.TYPE == _ROLE_FUNCTIONS.TYPE).OrderByDescending(x => x.SORT).Take(1).FirstOrDefault();
                _ROLE_FUNCTIONS.SORT = c.SORT + 1;
            }
            else
            {
                var p = a.OrderByDescending(x => x.SORT).Take(1).FirstOrDefault();
                if (p == null)
                {
                    _ROLE_FUNCTIONS.SORT = 1;
                }
                else
                {
                    _ROLE_FUNCTIONS.SORT = p.SORT + 1;

                }

            }



            DB.ROLE_FUNCTIONS.Add(_ROLE_FUNCTIONS);
            DB.SaveChanges();

            return RedirectToAction("Index");
        }


        [ValidateAntiForgeryToken, ValidateInput(false), HttpPost]
        public ActionResult Create(string username, string password)
        {
            var item = new ADMIN();
            item.USER_NAME = username;
            item.PASSWORD = password;
            item.IS_DELETE = 0;
            item.UPDATE_TIME = DateTime.Now;
            item.ADMIN_ROLE_RELATION = new List<ADMIN_ROLE_RELATION>();

            var role = Request.Form["role"];
            var rolesId = role.Split(new char[] { ',' });
            foreach (var roleId in rolesId)
            {
                var rId = WebFunctions.StringToIntNullable(roleId);
                if (rId.HasValue)
                {
                    var adminRole = new ADMIN_ROLE_RELATION();
                    adminRole.ROLE_ID = rId;
                    adminRole.CREATE_TIME = DateTime.Now;
                    adminRole.UPDATE_TIME = DateTime.Now;
                    adminRole.IS_DELETE = 0;

                    item.ADMIN_ROLE_RELATION.Add(adminRole);
                }
            }
            DB.ADMIN.Add(item);
            DB.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Roles = DB.ROLE.AsNoTracking().OrderBy(x => x.UUID).ToList();

            var item = DB.ADMIN.AsNoTracking().Where(x => x.UUID == id).FirstOrDefault();
            return View(item);
        }

        [ValidateAntiForgeryToken, ValidateInput(false), HttpPost]
        public ActionResult Edit(int id, string username, string password)
        {
            var item = DB.ADMIN.Where(x => x.UUID == id).FirstOrDefault();
            if (item != null)
            {
                item.USER_NAME = username;
                item.PASSWORD = password;
                item.UPDATE_TIME = DateTime.Now;

                if (item.ADMIN_ROLE_RELATION != null)
                {
                    DB.ADMIN_ROLE_RELATION.RemoveRange(item.ADMIN_ROLE_RELATION);
                    item.ADMIN_ROLE_RELATION.Clear();
                }

                var role = Request.Form["role"];
                if (!string.IsNullOrWhiteSpace(role))
                {
                    var rolesId = role.Split(new char[] { ',' });
                    foreach (var roleId in rolesId)
                    {
                        var rId = WebFunctions.StringToIntNullable(roleId);
                        if (rId.HasValue)
                        {
                            var adminRole = new ADMIN_ROLE_RELATION();
                            adminRole.ROLE_ID = rId;
                            adminRole.CREATE_TIME = DateTime.Now;
                            adminRole.UPDATE_TIME = DateTime.Now;
                            adminRole.IS_DELETE = 0;

                            item.ADMIN_ROLE_RELATION.Add(adminRole);
                        }
                    }
                }

                DB.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var item = DB.ADMIN.Where(x => x.UUID == id).FirstOrDefault();
            if (item != null)
            {
                DB.ADMIN_ROLE_RELATION.RemoveRange(item.ADMIN_ROLE_RELATION);
                DB.ADMIN.Remove(item);
                DB.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Role()
        {
            var result = DB.ROLE.AsNoTracking().OrderBy(x => x.UUID).ToList();
            return View(result);
        }

        public ActionResult EditRole(int id)
        {
            ViewBag.Functions = DB.ROLE_FUNCTIONS.AsNoTracking().Where(x => x.IS_DELETE != 1).OrderBy(x => x.TYPE).ThenBy(x => x.PARENT).ThenBy(x => x.SORT).ToList();

            var item = DB.ROLE.AsNoTracking().Where(x => x.UUID == id).FirstOrDefault();
            return View(item);
        }

        [ValidateAntiForgeryToken, ValidateInput(false), HttpPost]
        public ActionResult EditRole(int id, string name)
        {
            var item = DB.ROLE.Where(x => x.UUID == id).FirstOrDefault();
            if (item != null)
            {
                item.NAME = name;
                item.UPDATE_TIME = DateTime.Now;

                if (item.ROLE_FUNCTION_RELATION != null)
                {
                    DB.ROLE_FUNCTION_RELATION.RemoveRange(item.ROLE_FUNCTION_RELATION);
                    item.ROLE_FUNCTION_RELATION.Clear();
                }

                var function = Request.Form["function"];
                if (!string.IsNullOrWhiteSpace(function))
                {
                    var funsId = function.Split(new char[] { ',' });
                    foreach (var funId in funsId)
                    {
                        var fId = WebFunctions.StringToIntNullable(funId);
                        if (fId.HasValue)
                        {
                            var roleFun = new ROLE_FUNCTION_RELATION();
                            roleFun.FUNCTION_ID = fId;
                            roleFun.CREATE_TIME = DateTime.Now;
                            roleFun.UPDATE_TIME = DateTime.Now;
                            roleFun.IS_DELETE = 0;

                            item.ROLE_FUNCTION_RELATION.Add(roleFun);
                        }
                    }
                }

                DB.SaveChanges();
            }

            return RedirectToAction("Role");
        }
    }
}