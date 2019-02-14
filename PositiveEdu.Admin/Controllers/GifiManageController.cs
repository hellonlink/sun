using Newtonsoft.Json;
using PagedList;
using PositiveEdu.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PositiveEdu.Admin.Controllers
{
    public class GifiManageController : BaseController
    {
        // GET: GifiManage
        public ActionResult Index(int page = 1)
        {

            int pageSize = 15;
            var query = DB.T_Gifts.AsNoTracking().AsQueryable();
            var GiftName = Request.QueryString["GiftName"] == null ? null : (Request.QueryString["GiftName"].ToString());
            if (!string.IsNullOrEmpty(GiftName)) 
            {
                query = query.Where(x => x.GiftName.Contains(GiftName));
                ViewBag.GiftName = GiftName;
            }
            else
            {
                ViewBag.GiftName = null;

            }



            var GiftNo = Request.QueryString["GiftNo"] == null ? null : (Request.QueryString["GiftNo"].ToString());
            if (!string.IsNullOrEmpty(GiftNo))
            {
                query = query.Where(x => x.GiftNo.Contains(GiftNo));
                ViewBag.GiftNo = GiftNo;
            }
            else
            {
                ViewBag.GiftNo = null;

            }

            var T_GiftsTagId = Request.QueryString["T_GiftsTagId"] == null ? null : (Request.QueryString["T_GiftsTagId"].ToString());
            if (!string.IsNullOrEmpty(T_GiftsTagId))
            {
                if (T_GiftsTagId == "全部")
                {

                }
                else
                {
                    int? af = ((int?)Convert.ToInt32(T_GiftsTagId));
                    query = query.Where(x => x.T_GiftsTagId == af);
                    ViewBag.T_GiftsTagId = T_GiftsTagId;
                }

            }
            else
            {
                ViewBag.T_GiftsTagId = "全部";

            }

            var GiftType = Request.QueryString["GiftType"] == null ? null : (Request.QueryString["GiftType"].ToString());
            if (!string.IsNullOrEmpty(GiftType))
            {

                int? af = ((int?)Convert.ToInt32(GiftType));
                query = query.Where(x => x.GiftType == af);
                ViewBag.GiftType = GiftType;


            }
            else
            {
                ViewBag.GiftType = null;

            }

            var IsShelf = Request.QueryString["IsShelf"] == null ? null : (Request.QueryString["IsShelf"].ToString());
            if (!string.IsNullOrEmpty(IsShelf))
            {

                int? af = ((int?)Convert.ToInt32(IsShelf));
                query = query.Where(x => x.IsShelf == af);
                ViewBag.IsShelf = IsShelf;


            }
            else
            {
                ViewBag.IsShelf = null;

            }

            var result = query.OrderBy(x => x.id).ToPagedList(page, pageSize);

            return View(result);



        }
        public ActionResult TagDelete(int id)
        {
            var item = DB.T_GiftsTag.Where(x => x.id == id).FirstOrDefault();
            if (item != null)
            {
                DB.T_GiftsTag.Remove(item);

                DB.SaveChanges();
            }

            return RedirectToAction("GiftsTagIndex");
        }
        public ActionResult GiftsTagCreate()
        {

            return View();
        }
        [ValidateAntiForgeryToken, ValidateInput(false), HttpPost]
        public ActionResult GiftsTagCreate(string TagName, int? IsUse)
        {

            var u = JsonConvert.DeserializeObject<AuthAdmin>(User.Identity.Name);


            DB.T_GiftsTag.Add(new T_GiftsTag()
            {
                TagName = TagName,

                CreatedOn = DateTime.Now,
                CreatedBy = u.RealName,
                IsUse = IsUse
            });
            DB.SaveChanges();
            return RedirectToAction("GiftsTagIndex");

        }
        public ActionResult GiftsTagIndex(int page = 1)
        {
            IPagedList<T_GiftsTag> result = NewMethod1(page);
            return View(result);
        }
        public ActionResult TagEdit(int id)
        {
            var a = DB.T_GiftsTag.Where(x => x.id == id).FirstOrDefault();
            return View(a);
        }
        [ValidateAntiForgeryToken, ValidateInput(false), HttpPost]
        public ActionResult TagEdit(string TagName, int? IsUse, int? id)
        {
            var u = JsonConvert.DeserializeObject<AuthAdmin>(User.Identity.Name);
            var a = DB.T_GiftsTag.Where(x => x.id == id).FirstOrDefault();
            if (a != null)
            {
                a.TagName = TagName;
                a.IsUse = IsUse;
                a.UpdatedOn = DateTime.Now;
                a.UpdatedBy = u.RealName;
                DB.SaveChanges();
            }





            return RedirectToAction("GiftsTagIndex");

        }
        private IPagedList<T_GiftsTag> NewMethod1(int page)
        {
            int pageSize = 15;
            var query = DB.T_GiftsTag.AsNoTracking().AsQueryable();
            var _TagName = Request.QueryString["_TagName"] == null ? null : (Request.QueryString["_TagName"].ToString());
            if (!string.IsNullOrEmpty(_TagName))
            {
                query = query.Where(x => x.TagName.Contains(_TagName));
                ViewBag._TagName = _TagName;
            }
            else
            {
                ViewBag._TagName = null;

            }
            var result = query.OrderBy(x => x.id).ToPagedList(page, pageSize);
            return result;
        }

    }
}