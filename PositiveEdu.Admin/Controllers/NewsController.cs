using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PositiveEdu.DAL;

namespace PositiveEdu.Admin.Controllers
{
    public class NewsController : BaseController
    {

        public ActionResult Index(int page = 1)
        {
            int pageSize = 15;
            var query = DB.News.AsNoTracking().OrderByDescending(x => x.publishDate);
            var result = query.ToPagedList(page, pageSize);
            return View(result);
        }

        public ActionResult Create()
        {
            return View();
        }

        [ValidateAntiForgeryToken, ValidateInput(false), HttpPost]
        public ActionResult Create(string title, string mainContent, DateTime publishDate)
        {
            News item = new News()
            {
                title = title,
                mainContent = mainContent,
                publishDate = publishDate,
                createdTime = DateTime.Now,
                updatedTime = DateTime.Now,
                createdUserId = AuthAdmin.AdminId,
                updatedUserId = AuthAdmin.AdminId,
            };
            DB.News.Add(item);
            DB.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var item = DB.News.Where(x => x.id == id).FirstOrDefault();
            return View(item);
        }

        [ValidateAntiForgeryToken, ValidateInput(false), HttpPost]
        public ActionResult Edit(int id, string title, string mainContent, DateTime publishDate)
        {
            var item = DB.News.Where(x => x.id == id).FirstOrDefault();
            if (item != null)
            {
                item.title = title;
                item.mainContent = mainContent;
                item.publishDate = publishDate;
                item.updatedTime = DateTime.Now;
                item.updatedUserId = AuthAdmin.AdminId;

                DB.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var item = DB.News.Where(x => x.id == id).FirstOrDefault();
            DB.News.Remove(item);
            DB.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}