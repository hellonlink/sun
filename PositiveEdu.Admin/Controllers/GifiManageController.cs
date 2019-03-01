﻿using Newtonsoft.Json;
using PagedList;
using PositiveEdu.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PositiveEdu.Admin.Controllers
{
    public class GifiManageController : BaseController
    {
        // GET: GifiManage

        #region 礼品
        #region 礼品分类
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
        #endregion
        /// <summary>
        /// 礼品列表
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
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


        /// <summary>
        /// 卷池管理
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult PoolIndex(int page = 1)
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



        #region 实体礼品
        public ActionResult RealGiftsCreate()
        {

            return View(new T_Gifts());
        }
        [ValidateAntiForgeryToken, ValidateInput(false), HttpPost]
        public ActionResult RealGiftsCreate(string TagName, int? IsUse)
        {

            var a = new T_Gifts();
            a.GiftName = Request.Form["GiftName"] == "" ? a.GiftName : Request.Form["GiftName"].ToString();
            a.GiftType = Request.Form["GiftType"] == "" ? a.GiftType : Convert.ToInt32(Request.Form["GiftType"].ToString());
            a.GiftNo = Request.Form["GiftNo"] == "" ? a.GiftNo : Request.Form["GiftNo"].ToString();
            a.GiftIntroductionText = Request.Form["GiftIntroductionText"] == "" ? a.GiftIntroductionText : Request.Form["GiftIntroductionText"].ToString();
            a.GiftIntroductionPT = Request.Form["GiftIntroductionPT"] == "" ? a.GiftIntroductionPT : Request.Form["GiftIntroductionPT"].ToString();
            a.T_GiftsTagId = Request.Form["T_GiftsTagId"] == "" ? a.T_GiftsTagId : Convert.ToInt32(Request.Form["T_GiftsTagId"].ToString());
            a.GiftInventory = Request.Form["GiftInventory"] == "" ? a.GiftInventory : Convert.ToInt32(Request.Form["GiftInventory"].ToString());
            a.IsShelf = Request.Form["IsShelf"] == "" ? a.IsShelf : Convert.ToInt32(Request.Form["IsShelf"].ToString());
            a.IsExchange = Request.Form["IsExchange"] == "" ? a.IsExchange : Convert.ToInt32(Request.Form["IsExchange"].ToString());
            a.RedeemPoints = Request.Form["RedeemPoints"] == "" ? a.RedeemPoints : Convert.ToInt32(Request.Form["RedeemPoints"].ToString());
            a.Tag1 = Request.Form["Tag1"] == "" ? a.Tag1 : Request.Form["Tag1"].ToString();
            a.Tag2 = Request.Form["Tag2"] == "" ? a.Tag2 : Request.Form["Tag2"].ToString();
            a.Tag3 = Request.Form["Tag3"] == "" ? a.Tag3 : Request.Form["Tag3"].ToString();
            a.Tag4 = Request.Form["Tag4"] == "" ? a.Tag4 : Request.Form["Tag4"].ToString();
            a.Tag5 = Request.Form["Tag5"] == "" ? a.Tag5 : Request.Form["Tag5"].ToString();
            a.GiftMainPicture = Request.Form["GiftMainPicture"] == "" ? a.GiftMainPicture : Request.Form["GiftMainPicture"].ToString();
            a.GiftThumbnailPicture = Request.Form["GiftThumbnailPicture"] == "" ? a.GiftThumbnailPicture : Request.Form["GiftThumbnailPicture"].ToString();
            var u = JsonConvert.DeserializeObject<AuthAdmin>(User.Identity.Name);


            DB.T_Gifts.Add(new T_Gifts()
            {
                GiftName = a.GiftName,
                GiftType = a.GiftType,
                GiftNo = a.GiftNo,
                GiftIntroductionText = a.GiftIntroductionText,
                GiftIntroductionPT = a.GiftIntroductionPT,
                T_GiftsTagId = a.T_GiftsTagId,
                GiftInventory = a.GiftInventory,
                IsShelf = a.IsShelf,
                IsExchange = a.IsExchange,
                RedeemPoints = a.RedeemPoints,
                Tag1 = a.Tag1,
                Tag2 = a.Tag2,
                Tag3 = a.Tag3,
                Tag4 = a.Tag4,
                Tag5 = a.Tag5,
                GiftMainPicture = a.GiftMainPicture,
                GiftThumbnailPicture = a.GiftThumbnailPicture,
                CreatedOn = DateTime.Now,
                CreatedBy = u.RealName,

            });
            DB.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult RealGiftsEdit(int? id)
        {

            return View(DB.T_Gifts.Where(x => x.id == id).FirstOrDefault());

        }
        [ValidateAntiForgeryToken, ValidateInput(false), HttpPost]
        public ActionResult RealGiftsEdit()
        {

            var a = new T_Gifts();
            a.GiftName = Request.Form["GiftName"] == "" ? a.GiftName : Request.Form["GiftName"].ToString();
            a.GiftType = Request.Form["GiftType"] == "" ? a.GiftType : Convert.ToInt32(Request.Form["GiftType"].ToString());
            a.GiftNo = Request.Form["GiftNo"] == "" ? a.GiftNo : Request.Form["GiftNo"].ToString();
            a.GiftIntroductionText = Request.Form["GiftIntroductionText"] == "" ? a.GiftIntroductionText : Request.Form["GiftIntroductionText"].ToString();
            a.GiftIntroductionPT = Request.Form["GiftIntroductionPT"] == "" ? a.GiftIntroductionPT : Request.Form["GiftIntroductionPT"].ToString();
            a.T_GiftsTagId = Request.Form["T_GiftsTagId"] == "" ? a.T_GiftsTagId : Convert.ToInt32(Request.Form["T_GiftsTagId"].ToString());
            a.GiftInventory = Request.Form["GiftInventory"] == "" ? a.GiftInventory : Convert.ToInt32(Request.Form["GiftInventory"].ToString());
            a.IsShelf = Request.Form["IsShelf"] == "" ? a.IsShelf : Convert.ToInt32(Request.Form["IsShelf"].ToString());
            a.IsExchange = Request.Form["IsExchange"] == "" ? a.IsExchange : Convert.ToInt32(Request.Form["IsExchange"].ToString());
            a.RedeemPoints = Request.Form["RedeemPoints"] == "" ? a.RedeemPoints : Convert.ToInt32(Request.Form["RedeemPoints"].ToString());
            a.Tag1 = Request.Form["Tag1"] == "" ? a.Tag1 : Request.Form["Tag1"].ToString();
            a.Tag2 = Request.Form["Tag2"] == "" ? a.Tag2 : Request.Form["Tag2"].ToString();
            a.Tag3 = Request.Form["Tag3"] == "" ? a.Tag3 : Request.Form["Tag3"].ToString();
            a.Tag4 = Request.Form["Tag4"] == "" ? a.Tag4 : Request.Form["Tag4"].ToString();
            a.Tag5 = Request.Form["Tag5"] == "" ? a.Tag5 : Request.Form["Tag5"].ToString();
            a.GiftMainPicture = Request.Form["GiftMainPicture"] == "" ? a.GiftMainPicture : Request.Form["GiftMainPicture"].ToString();
            a.GiftThumbnailPicture = Request.Form["GiftThumbnailPicture"] == "" ? a.GiftThumbnailPicture : Request.Form["GiftThumbnailPicture"].ToString();
            var u = JsonConvert.DeserializeObject<AuthAdmin>(User.Identity.Name);
            var id = Convert.ToInt32(Request.Form["id"].ToString());

            var ap = DB.T_Gifts.Where(x => x.id == id).FirstOrDefault();


            if (ap != null)
            {


                ap.GiftName = a.GiftName;
                ap.GiftType = a.GiftType;
                ap.GiftNo = a.GiftNo;
                ap.GiftIntroductionText = a.GiftIntroductionText;
                ap.GiftIntroductionPT = a.GiftIntroductionPT;
                ap.T_GiftsTagId = a.T_GiftsTagId;
                ap.GiftInventory = a.GiftInventory;
                ap.IsShelf = a.IsShelf;
                ap.IsExchange = a.IsExchange;
                ap.RedeemPoints = a.RedeemPoints;
                ap.Tag1 = a.Tag1;
                ap.Tag2 = a.Tag2;
                ap.Tag3 = a.Tag3;
                ap.Tag4 = a.Tag4;
                ap.Tag5 = a.Tag5;
                ap.GiftMainPicture = a.GiftMainPicture;
                ap.GiftThumbnailPicture = a.GiftThumbnailPicture;
                ap.UpdatedOn = DateTime.Now;
                ap.UpdatedBy = u.RealName;

            }
            DB.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult RealGiftsDetail(int? id)
        {



            return View(DB.T_Gifts.Where(x => x.id == id).FirstOrDefault());
        }
        public ActionResult RealGiftsDete()
        {
            var id = Convert.ToInt32(Request.Form["id"].ToString());

            var ap = DB.T_Gifts.Where(x => x.id == id).FirstOrDefault();


            DB.T_Gifts.Remove(ap);
            DB.SaveChanges();


            return RedirectToAction("Index");

        }
        #endregion

        #region 第三方证券


        public ActionResult OthersGiftsCreate()
        {

            return View(new T_Gifts());
        }
        [ValidateAntiForgeryToken, ValidateInput(false), HttpPost]
        public ActionResult OthersGiftsCreate(string TagName, int? IsUse)
        {

            var a = new T_Gifts();
            a.GiftName = Request.Form["GiftName"] == "" ? a.GiftName : Request.Form["GiftName"].ToString();
            a.GiftType = Request.Form["GiftType"] == "" ? a.GiftType : Convert.ToInt32(Request.Form["GiftType"].ToString());
            a.IsCoupon = Request.Form["IsCoupon"] == "" ? a.IsCoupon : Convert.ToInt32(Request.Form["IsCoupon"].ToString());
            a.GiftIntroductionText = Request.Form["GiftIntroductionText"] == "" ? a.GiftIntroductionText : Request.Form["GiftIntroductionText"].ToString();
            a.GiftIntroductionPT = Request.Form["GiftIntroductionPT"] == "" ? a.GiftIntroductionPT : Request.Form["GiftIntroductionPT"].ToString();
            a.T_GiftsTagId = Request.Form["T_GiftsTagId"] == "" ? a.T_GiftsTagId : Convert.ToInt32(Request.Form["T_GiftsTagId"].ToString());
            a.IsShelf = Request.Form["IsShelf"] == "" ? a.IsShelf : Convert.ToInt32(Request.Form["IsShelf"].ToString());
            a.IsExchange = Request.Form["IsExchange"] == "" ? a.IsExchange : Convert.ToInt32(Request.Form["IsExchange"].ToString());
            a.RedeemPoints = Request.Form["RedeemPoints"] == "" ? a.RedeemPoints : Convert.ToInt32(Request.Form["RedeemPoints"].ToString());
            a.Tag1 = Request.Form["Tag1"] == "" ? a.Tag1 : Request.Form["Tag1"].ToString();
            a.Tag2 = Request.Form["Tag2"] == "" ? a.Tag2 : Request.Form["Tag2"].ToString();
            a.Tag3 = Request.Form["Tag3"] == "" ? a.Tag3 : Request.Form["Tag3"].ToString();
            a.Tag4 = Request.Form["Tag4"] == "" ? a.Tag4 : Request.Form["Tag4"].ToString();
            a.Tag5 = Request.Form["Tag5"] == "" ? a.Tag5 : Request.Form["Tag5"].ToString();
            a.GiftMainPicture = Request.Form["GiftMainPicture"] == "" ? a.GiftMainPicture : Request.Form["GiftMainPicture"].ToString();
            a.GiftThumbnailPicture = Request.Form["GiftThumbnailPicture"] == "" ? a.GiftThumbnailPicture : Request.Form["GiftThumbnailPicture"].ToString();

            var u = JsonConvert.DeserializeObject<AuthAdmin>(User.Identity.Name);


            DB.T_Gifts.Add(new T_Gifts()
            {
                GiftName = a.GiftName,
                GiftType = a.GiftType,
                GiftNo = a.GiftNo,
                GiftIntroductionText = a.GiftIntroductionText,
                GiftIntroductionPT = a.GiftIntroductionPT,
                T_GiftsTagId = a.T_GiftsTagId,
                GiftInventory = a.GiftInventory,
                IsShelf = a.IsShelf,
                IsExchange = a.IsExchange,
                RedeemPoints = a.RedeemPoints,
                Tag1 = a.Tag1,
                Tag2 = a.Tag2,
                Tag3 = a.Tag3,
                Tag4 = a.Tag4,
                Tag5 = a.Tag5,
                GiftMainPicture = a.GiftMainPicture,
                GiftThumbnailPicture = a.GiftThumbnailPicture,
                CreatedOn = DateTime.Now,
                CreatedBy = u.RealName,
                IsCoupon = a.IsCoupon,
                SaveType = a.SaveType,
                SaveMoney = a.SaveMoney,
                BeginTime = a.BeginTime,
                StopTime = a.StopTime,
                MoneyLimit = a.MoneyLimit,
                OpenCodeCouponNo = a.OpenCodeCouponNo

            });
            DB.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult OthersGiftsEdit(int? id)
        {

            return View(DB.T_Gifts.Where(x => x.id == id).FirstOrDefault());

        }
        [ValidateAntiForgeryToken, ValidateInput(false), HttpPost]
        public ActionResult OthersGiftsEdit()
        {


            var a = new T_Gifts();
            a.GiftName = Request.Form["GiftName"] == "" ? a.GiftName : Request.Form["GiftName"].ToString();
            a.GiftType = Request.Form["GiftType"] == "" ? a.GiftType : Convert.ToInt32(Request.Form["GiftType"].ToString());
            a.IsCoupon = Request.Form["IsCoupon"] == "" ? a.IsCoupon : Convert.ToInt32(Request.Form["IsCoupon"].ToString());
            a.GiftIntroductionPT = Request.Form["GiftIntroductionPT"] == "" ? a.GiftIntroductionPT : Request.Form["GiftIntroductionPT"].ToString();
            a.T_GiftsTagId = Request.Form["T_GiftsTagId"] == "" ? a.T_GiftsTagId : Convert.ToInt32(Request.Form["T_GiftsTagId"].ToString());
            a.IsShelf = Request.Form["IsShelf"] == "" ? a.IsShelf : Convert.ToInt32(Request.Form["IsShelf"].ToString());
            a.IsExchange = Request.Form["IsExchange"] == "" ? a.IsExchange : Convert.ToInt32(Request.Form["IsExchange"].ToString());
            a.RedeemPoints = Request.Form["RedeemPoints"] == "" ? a.RedeemPoints : Convert.ToInt32(Request.Form["RedeemPoints"].ToString());
            a.Tag1 = Request.Form["Tag1"] == "" ? a.Tag1 : Request.Form["Tag1"].ToString();
            a.Tag2 = Request.Form["Tag2"] == "" ? a.Tag2 : Request.Form["Tag2"].ToString();
            a.Tag3 = Request.Form["Tag3"] == "" ? a.Tag3 : Request.Form["Tag3"].ToString();
            a.Tag4 = Request.Form["Tag4"] == "" ? a.Tag4 : Request.Form["Tag4"].ToString();
            a.Tag5 = Request.Form["Tag5"] == "" ? a.Tag5 : Request.Form["Tag5"].ToString();
            a.GiftMainPicture = Request.Form["GiftMainPicture"] == "" ? a.GiftMainPicture : Request.Form["GiftMainPicture"].ToString();
            a.GiftThumbnailPicture = Request.Form["GiftThumbnailPicture"] == "" ? a.GiftThumbnailPicture : Request.Form["GiftThumbnailPicture"].ToString();
            var u = JsonConvert.DeserializeObject<AuthAdmin>(User.Identity.Name);
            var id = Convert.ToInt32(Request.Form["id"].ToString());
            var ap = DB.T_Gifts.Where(x => x.id == id).FirstOrDefault();

            if (ap != null)
            {
                ap.GiftName = a.GiftName;
                ap.GiftType = a.GiftType;
                ap.GiftNo = a.GiftNo;
                ap.GiftIntroductionText = a.GiftIntroductionText;
                ap.GiftIntroductionPT = a.GiftIntroductionPT;
                ap.T_GiftsTagId = a.T_GiftsTagId;
                ap.GiftInventory = a.GiftInventory;
                ap.IsShelf = a.IsShelf;
                ap.IsExchange = a.IsExchange;
                ap.RedeemPoints = a.RedeemPoints;
                ap.Tag1 = a.Tag1;
                ap.Tag2 = a.Tag2;
                ap.Tag3 = a.Tag3;
                ap.Tag4 = a.Tag4;
                ap.Tag5 = a.Tag5;
                ap.GiftMainPicture = a.GiftMainPicture;
                ap.GiftThumbnailPicture = a.GiftThumbnailPicture;
                ap.UpdatedOn = DateTime.Now;
                ap.UpdatedBy = u.RealName;
                ap.IsCoupon = a.IsCoupon;
                ap.SaveType = a.SaveType;
                ap.SaveMoney = a.SaveMoney;
                ap.BeginTime = a.BeginTime;
                ap.StopTime = a.StopTime;
                ap.MoneyLimit = a.MoneyLimit;
                ap.OpenCodeCouponNo = a.OpenCodeCouponNo;
                DB.SaveChanges();
            }

            return RedirectToAction("Index");


        }

        public ActionResult OthersGiftsDetail(int? id)
        {



            return View(DB.T_Gifts.Where(x => x.id == id).FirstOrDefault());
        }
        public ActionResult OthersGiftsDete()
        {
            var id = Convert.ToInt32(Request.Form["id"].ToString());

            var ap = DB.T_Gifts.Where(x => x.id == id).FirstOrDefault();


            DB.T_Gifts.Remove(ap);
            DB.SaveChanges();


            return RedirectToAction("Index");

        }


        #endregion

        #region  自主证券
        public ActionResult PrivateGiftsCreateMuch(int? id)
        {
            var q = DB.T_Gifts.Where(x => x.id == id).FirstOrDefault();

            return View(q);
        }
        [HttpPost]
        public ActionResult PrivateGiftsCreateMuch(int? GiftInventory, int? id)
        {

            var u = JsonConvert.DeserializeObject<AuthAdmin>(User.Identity.Name);

            ////使用异步方法
            var a = Task.Run(
                 () =>
                 {
                     PeContext d = new PeContext();

                     var g = d.T_Gifts.Where(x => x.id == id).First();

                     var a1 = new List<T_GiftsChild>();
                     for (int i = 0; i < GiftInventory; i++)
                     {
                         a1.Add(
                            new T_GiftsChild()
                            {

                                CreatedBy = u.RealName,
                                CreatedOn = DateTime.Now,
                                IsDeleted = false,
                                IsUsed = 0,
                                GenerationTime = DateTime.Now,
                                EffectiveTime = g.BeginTime,
                                FailureTime = g.StopTime,
                                CouponNo = g.OpenCodeCouponNo + 123456,



                            }


                             );
                     }
                     d.T_GiftChild.AddRange(a1);
                     g.T_GiftsChild = a1;
                     if (g.GiftInventory == null)
                     {
                         g.GiftInventory = 0;
                     }
                     g.GiftInventory = g.GiftInventory + GiftInventory;
                     d.SaveChanges();
                 }



                 );
            //a.Start();
            a.Wait();

            var st = a.Status;
            if (a.Status == TaskStatus.RanToCompletion)
            {

                return Json(1000);
            }
            else
            {
                return Json(0);

            }



        }
        public ActionResult PrivateGiftsCreate()
        {

            return View(new T_Gifts());
        }
        [HttpPost]
        public ActionResult PrivateGiftsCreate(string TagName, int? IsUse)
        {

            var a = new T_Gifts();
            a.GiftName = Request.Form["GiftName"] == "" ? a.GiftName : Request.Form["GiftName"].ToString();
            a.GiftType = Request.Form["GiftType"] == "" ? a.GiftType : Convert.ToInt32(Request.Form["GiftType"].ToString());
            a.IsCoupon = Request.Form["IsCoupon"] == "" ? a.IsCoupon : Convert.ToInt32(Request.Form["IsCoupon"].ToString());
            a.SaveType = Request.Form["SaveType"] == "" ? a.SaveType : Convert.ToInt32(Request.Form["SaveType"].ToString());
            a.SaveMoney = Request.Form["SaveMoney"] == "" ? a.SaveMoney : Convert.ToInt32(Request.Form["SaveMoney"].ToString());
            a.BeginTime = Request.Form["BeginTime"] == "" ? a.BeginTime : Convert.ToDateTime(Request.Form["BeginTime"].ToString());
            a.StopTime = Request.Form["StopTime"] == "" ? a.StopTime : Convert.ToDateTime(Request.Form["StopTime"].ToString());
            a.MoneyLimit = Request.Form["MoneyLimit"] == "" ? a.MoneyLimit : Convert.ToInt32(Request.Form["MoneyLimit"].ToString());
            a.OpenCodeCouponNo = Request.Form["OpenCodeCouponNo"] == "" ? a.GiftNo : Request.Form["OpenCodeCouponNo"].ToString();
            a.GiftIntroductionText = Request.Form["GiftIntroductionText"] == "" ? a.GiftIntroductionText : Request.Form["GiftIntroductionText"].ToString();
            a.GiftIntroductionPT = Request.Form["GiftIntroductionPT"] == "" ? a.GiftIntroductionPT : Request.Form["GiftIntroductionPT"].ToString();
            a.T_GiftsTagId = Request.Form["T_GiftsTagId"] == "" ? a.T_GiftsTagId : Convert.ToInt32(Request.Form["T_GiftsTagId"].ToString());
            a.IsShelf = Request.Form["IsShelf"] == "" ? a.IsShelf : Convert.ToInt32(Request.Form["IsShelf"].ToString());
            a.IsExchange = Request.Form["IsExchange"] == "" ? a.IsExchange : Convert.ToInt32(Request.Form["IsExchange"].ToString());
            a.RedeemPoints = Request.Form["RedeemPoints"] == "" ? a.RedeemPoints : Convert.ToInt32(Request.Form["RedeemPoints"].ToString());
            a.Tag1 = Request.Form["Tag1"] == "" ? a.Tag1 : Request.Form["Tag1"].ToString();
            a.Tag2 = Request.Form["Tag2"] == "" ? a.Tag2 : Request.Form["Tag2"].ToString();
            a.Tag3 = Request.Form["Tag3"] == "" ? a.Tag3 : Request.Form["Tag3"].ToString();
            a.Tag4 = Request.Form["Tag4"] == "" ? a.Tag4 : Request.Form["Tag4"].ToString();
            a.Tag5 = Request.Form["Tag5"] == "" ? a.Tag5 : Request.Form["Tag5"].ToString();
            a.GiftMainPicture = Request.Form["GiftMainPicture"] == "" ? a.GiftMainPicture : Request.Form["GiftMainPicture"].ToString();
            a.GiftThumbnailPicture = Request.Form["GiftThumbnailPicture"] == "" ? a.GiftThumbnailPicture : Request.Form["GiftThumbnailPicture"].ToString();

            var u = JsonConvert.DeserializeObject<AuthAdmin>(User.Identity.Name);


            DB.T_Gifts.Add(new T_Gifts()
            {
                GiftName = a.GiftName,
                GiftType = a.GiftType,
                GiftNo = a.GiftNo,
                GiftIntroductionText = a.GiftIntroductionText,
                GiftIntroductionPT = a.GiftIntroductionPT,
                T_GiftsTagId = a.T_GiftsTagId,
                GiftInventory = a.GiftInventory,
                IsShelf = a.IsShelf,
                IsExchange = a.IsExchange,
                RedeemPoints = a.RedeemPoints,
                Tag1 = a.Tag1,
                Tag2 = a.Tag2,
                Tag3 = a.Tag3,
                Tag4 = a.Tag4,
                Tag5 = a.Tag5,
                GiftMainPicture = a.GiftMainPicture,
                GiftThumbnailPicture = a.GiftThumbnailPicture,
                CreatedOn = DateTime.Now,
                CreatedBy = u.RealName,
                IsCoupon = a.IsCoupon,
                SaveType = a.SaveType,
                SaveMoney = a.SaveMoney,
                BeginTime = a.BeginTime,
                StopTime = a.StopTime,
                MoneyLimit = a.MoneyLimit,
                OpenCodeCouponNo = a.OpenCodeCouponNo

            });
            DB.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult PrivateGiftsEdit(int? id)
        {

            return View(DB.T_Gifts.Where(x => x.id == id).FirstOrDefault());

        }
        [ValidateAntiForgeryToken, ValidateInput(false), HttpPost]
        public ActionResult PrivateGiftsEdit()
        {


            var a = new T_Gifts();
            a.GiftName = Request.Form["GiftName"] == "" ? a.GiftName : Request.Form["GiftName"].ToString();
            a.GiftType = Request.Form["GiftType"] == "" ? a.GiftType : Convert.ToInt32(Request.Form["GiftType"].ToString());
            a.IsCoupon = Request.Form["IsCoupon"] == "" ? a.IsCoupon : Convert.ToInt32(Request.Form["IsCoupon"].ToString());
            a.SaveType = Request.Form["SaveType"] == "" ? a.SaveType : Convert.ToInt32(Request.Form["SaveType"].ToString());
            a.SaveMoney = Request.Form["SaveMoney"] == "" ? a.SaveMoney : Convert.ToInt32(Request.Form["SaveMoney"].ToString());
            a.BeginTime = Request.Form["BeginTime"] == "" ? a.BeginTime : Convert.ToDateTime(Request.Form["BeginTime"].ToString());
            a.StopTime = Request.Form["StopTime"] == "" ? a.StopTime : Convert.ToDateTime(Request.Form["StopTime"].ToString());
            a.MoneyLimit = Request.Form["MoneyLimit"] == "" ? a.MoneyLimit : Convert.ToInt32(Request.Form["MoneyLimit"].ToString());
            a.OpenCodeCouponNo = Request.Form["OpenCodeCouponNo"] == "" ? a.GiftNo : Request.Form["OpenCodeCouponNo"].ToString();
            a.GiftIntroductionText = Request.Form["GiftIntroductionText"] == "" ? a.GiftIntroductionText : Request.Form["GiftIntroductionText"].ToString();
            a.GiftIntroductionPT = Request.Form["GiftIntroductionPT"] == "" ? a.GiftIntroductionPT : Request.Form["GiftIntroductionPT"].ToString();
            a.T_GiftsTagId = Request.Form["T_GiftsTagId"] == "" ? a.T_GiftsTagId : Convert.ToInt32(Request.Form["T_GiftsTagId"].ToString());
            a.IsShelf = Request.Form["IsShelf"] == "" ? a.IsShelf : Convert.ToInt32(Request.Form["IsShelf"].ToString());
            a.IsExchange = Request.Form["IsExchange"] == "" ? a.IsExchange : Convert.ToInt32(Request.Form["IsExchange"].ToString());
            a.RedeemPoints = Request.Form["RedeemPoints"] == "" ? a.RedeemPoints : Convert.ToInt32(Request.Form["RedeemPoints"].ToString());
            a.Tag1 = Request.Form["Tag1"] == "" ? a.Tag1 : Request.Form["Tag1"].ToString();
            a.Tag2 = Request.Form["Tag2"] == "" ? a.Tag2 : Request.Form["Tag2"].ToString();
            a.Tag3 = Request.Form["Tag3"] == "" ? a.Tag3 : Request.Form["Tag3"].ToString();
            a.Tag4 = Request.Form["Tag4"] == "" ? a.Tag4 : Request.Form["Tag4"].ToString();
            a.Tag5 = Request.Form["Tag5"] == "" ? a.Tag5 : Request.Form["Tag5"].ToString();
            a.GiftMainPicture = Request.Form["GiftMainPicture"] == "" ? a.GiftMainPicture : Request.Form["GiftMainPicture"].ToString();
            a.GiftThumbnailPicture = Request.Form["GiftThumbnailPicture"] == "" ? a.GiftThumbnailPicture : Request.Form["GiftThumbnailPicture"].ToString();
            var u = JsonConvert.DeserializeObject<AuthAdmin>(User.Identity.Name);
            var id = Convert.ToInt32(Request.Form["id"].ToString());
            var ap = DB.T_Gifts.Where(x => x.id == id).FirstOrDefault();

            if (ap != null)
            {
                ap.GiftName = a.GiftName;
                ap.GiftType = a.GiftType;
                ap.GiftNo = a.GiftNo;
                ap.GiftIntroductionText = a.GiftIntroductionText;
                ap.GiftIntroductionPT = a.GiftIntroductionPT;
                ap.T_GiftsTagId = a.T_GiftsTagId;
                ap.GiftInventory = a.GiftInventory;
                ap.IsShelf = a.IsShelf;
                ap.IsExchange = a.IsExchange;
                ap.RedeemPoints = a.RedeemPoints;
                ap.Tag1 = a.Tag1;
                ap.Tag2 = a.Tag2;
                ap.Tag3 = a.Tag3;
                ap.Tag4 = a.Tag4;
                ap.Tag5 = a.Tag5;
                ap.GiftMainPicture = a.GiftMainPicture;
                ap.GiftThumbnailPicture = a.GiftThumbnailPicture;
                ap.UpdatedOn = DateTime.Now;
                ap.UpdatedBy = u.RealName;
                ap.IsCoupon = a.IsCoupon;
                ap.SaveType = a.SaveType;
                ap.SaveMoney = a.SaveMoney;
                ap.BeginTime = a.BeginTime;
                ap.StopTime = a.StopTime;
                ap.MoneyLimit = a.MoneyLimit;
                ap.OpenCodeCouponNo = a.OpenCodeCouponNo;
                DB.SaveChanges();
            }

            return RedirectToAction("Index");


        }

        public ActionResult PrivateGiftsDetail(int? id)
        {



            return View(DB.T_Gifts.Where(x => x.id == id).FirstOrDefault());
        }
        public ActionResult PrivateGiftsDete()
        {
            var id = Convert.ToInt32(Request.Form["id"].ToString());

            var ap = DB.T_Gifts.Where(x => x.id == id).FirstOrDefault();


            DB.T_Gifts.Remove(ap);
            DB.SaveChanges();


            return RedirectToAction("Index");

        }
        #endregion



        #endregion
    }
}