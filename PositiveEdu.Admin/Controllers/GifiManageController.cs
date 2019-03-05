using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using PagedList;
using PositiveEdu.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
        /// 第三方卷导入记录表列表
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult T_OthersGiftsRecordIndex(int page = 1)
        {

            int pageSize = 15;
            var query = DB.T_OthersGiftsRecord.AsNoTracking().AsQueryable();

            var result = query.OrderBy(x => x.Id).ToPagedList(page, pageSize);

            return View(result);



        }

        /// <summary>
        /// 第三方卷导入记录表列表
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult T_PrivateGiftsRecordIndex(int page = 1)
        {

            int pageSize = 15;
            var query = DB.T_PrivateGiftsRecord.AsNoTracking().AsQueryable();

            var result = query.OrderBy(x => x.Id).ToPagedList(page, pageSize);

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
            var query = DB.T_GiftChild.AsNoTracking().AsQueryable();

            //券证名称：
            var GiftName = Request.QueryString["GiftName"] == null ? null : (Request.QueryString["GiftName"].ToString());
            if (!string.IsNullOrEmpty(GiftName))
            {

                query = query.Where(x => x.T_Gifts.GiftName.Contains(GiftName));
                ViewBag.GiftName = GiftName;



            }
            else
            {
                ViewBag.GiftName = null;

            }

            //是否已发送：：
            var ExchangeTime = Request.QueryString["ExchangeTime"] == null ? null : (Request.QueryString["ExchangeTime"].ToString());
            if (!string.IsNullOrEmpty(ExchangeTime))
            {
                var c = Convert.ToInt32(ExchangeTime);
                if (c == 0)
                {
                    query = query.Where(x => x.ExchangeTime != null);
                    ViewBag.ExchangeTime = 0;

                }
                else
                {
                    query = query.Where(x => x.ExchangeTime == null);

                    ViewBag.ExchangeTime = 1;

                }

            }
            else
            {
                ViewBag.ExchangeTime = null;

            }


            //是否已使用：
            var IsUsed = Request.QueryString["IsUsed"] == null ? null : (Request.QueryString["IsUsed"].ToString());
            if (!string.IsNullOrEmpty(IsUsed))
            {
                var c = Convert.ToInt32(IsUsed);

                query = query.Where(x => x.IsUsed == c);
                ViewBag.IsUsed = c;
            }
            else
            {
                ViewBag.IsUsed = null;

            }


            //卷证类型：
            var IsCoupon = Request.QueryString["IsCoupon"] == null ? null : (Request.QueryString["IsCoupon"].ToString());
            if (!string.IsNullOrEmpty(IsCoupon))
            {
                var c = Convert.ToInt32(IsCoupon);

                query = query.Where(x => x.T_Gifts.IsCoupon == c);
                ViewBag.IsCoupon = c;
            }
            else
            {
                ViewBag.IsCoupon = null;

            }


            //券证编号：
            var CouponNo = Request.QueryString["CouponNo"] == null ? null : (Request.QueryString["CouponNo"].ToString());
            if (!string.IsNullOrEmpty(CouponNo))
            {

                query = query.Where(x => x.CouponNo.Contains(CouponNo));
                ViewBag.CouponNo = CouponNo;



            }
            else
            {
                ViewBag.CouponNo = null;

            }
            //会员姓名：
            var CustomerRealName = Request.QueryString["CustomerRealName"] == null ? null : (Request.QueryString["CustomerRealName"].ToString());
            if (!string.IsNullOrEmpty(CustomerRealName))
            {

                query = query.Where(x => x.T_Customer.CustomerRealName.Contains(CustomerRealName));
                ViewBag.CustomerRealName = CustomerRealName;



            }
            else
            {
                ViewBag.CustomerRealName = null;

            }
            //会员编号：
            var CustomerId = Request.QueryString["CustomerId"] == null ? null : (Request.QueryString["CustomerId"].ToString());
            if (!string.IsNullOrEmpty(CustomerId))
            {

                query = query.Where(x => x.T_Customer.CustomerId.Contains(CustomerId));
                ViewBag.CustomerId = CustomerId;



            }
            else
            {
                ViewBag.CustomerId = null;

            }
            //会员手机号：
            var CustomerPhoneNum = Request.QueryString["CustomerPhoneNum"] == null ? null : (Request.QueryString["CustomerPhoneNum"].ToString());
            if (!string.IsNullOrEmpty(CustomerPhoneNum))
            {

                query = query.Where(x => x.T_Customer.CustomerPhoneNum.Contains(CustomerPhoneNum));
                ViewBag.CustomerPhoneNum = CustomerPhoneNum;



            }
            else
            {
                ViewBag.CustomerPhoneNum = null;

            }
            //券证实际发送时间段：

            var ExchangeStartTime = Request.QueryString["ExchangeStartTime"] == null ? null : (Request.QueryString["ExchangeStartTime"].ToString());
            if (!string.IsNullOrEmpty(ExchangeStartTime))
            {

                var s1 = Convert.ToDateTime(ExchangeStartTime);
                query = query.Where(x => x.ExchangeTime >= s1);
                ViewBag.ExchangeStartTime = ExchangeStartTime;



            }
            else
            {
                ViewBag.ExchangeStartTime = null;

            }

            var ExchangeStopTime = Request.QueryString["ExchangeStopTime"] == null ? null : (Request.QueryString["ExchangeStopTime"].ToString());
            if (!string.IsNullOrEmpty(ExchangeStopTime))
            {

                var s1 = Convert.ToDateTime(ExchangeStopTime);
                query = query.Where(x => x.ExchangeTime <= s1);
                ViewBag.ExchangeStopTime = ExchangeStopTime;



            }
            else
            {
                ViewBag.ExchangeStopTime = null;

            }

            //券证实际使用时间：



            var UseStartTime = Request.QueryString["UseStartTime"] == null ? null : (Request.QueryString["UseStartTime"].ToString());
            if (!string.IsNullOrEmpty(UseStartTime))
            {

                var s1 = Convert.ToDateTime(UseStartTime);
                query = query.Where(x => x.UseTime >= s1);
                ViewBag.UseStartTime = UseStartTime;



            }
            else
            {
                ViewBag.UseStartTime = null;

            }

            var UseStoptime = Request.QueryString["UseStoptime"] == null ? null : (Request.QueryString["UseStoptime"].ToString());
            if (!string.IsNullOrEmpty(UseStoptime))
            {

                var s1 = Convert.ToDateTime(UseStoptime);
                query = query.Where(x => x.UseTime <= s1);
                ViewBag.UseStoptime = UseStoptime;



            }
            else
            {
                ViewBag.UseStoptime = null;

            }





            var result = query.OrderBy(x => x.id).ToPagedList(page, pageSize);

            return View(result);



        }


        public ActionResult PoolDetail(int? id)
        {



            return View(DB.T_GiftChild.Where(x => x.id == id).FirstOrDefault());
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

        public ActionResult OthersCreateMuch(int? id)
        {
            var q = DB.T_Gifts.Where(x => x.id == id).FirstOrDefault();
            ViewBag.t = null;
            return View(q);
        }

        [ValidateInput(false), HttpPost]
        public dynamic OthersCreateMuch(HttpPostedFileBase file, int? id)
        {


            var u = JsonConvert.DeserializeObject<AuthAdmin>(User.Identity.Name);

            List<int> a3 = new List<int> { 1, 2 };
            a3.Clear();
            //记录行数
            int ct = 0;
            //记录重复行数
            int cf = 0;
            int b = 0;
            //判断列数
            int b1 = 0;
            ////使用异步方法
            var a = Task.Run(
                 () =>
                 {





                     PeContext d = new PeContext();

                     var g = d.T_Gifts.Where(x => x.id == id).First();

                     var a1 = new List<T_GiftsChild>();


                     var r = Request.Form["GiftInventory"];



                     if (file != null)
                     {



                         var filename = Path.Combine(Request.MapPath("~/App_Data"), file.FileName);
                         file.SaveAs(filename);

                         var dt = Import(filename, 0);
                         var d1 = dt.CreateDataReader();



                         if (d1.FieldCount == 1)
                         {

                             //记录
                             T_OthersGiftsRecord pd = new T_OthersGiftsRecord();

                             for (int j = 0; j < dt.Columns.Count; j++)
                             {
                                 if (dt.Columns[j].ColumnName != "CouponNo")
                                 {
                                     b1 = 1;
                                 }
                             }

                             if (b1 == 0)
                             {
                                 while (d1.Read())
                                 {

                                     ct++;

                                     var p1 = d1["CouponNo"].ToString();
                                     var t1 = d.T_GiftChild.Where(x => x.CouponNo.Contains(p1)).ToList();

                                     if (
                                     t1.Count > 0


                                         )
                                     {


                                         cf++;
                                         a3.Add(cf);
                                         continue;

                                     }

                                     a1.Add(
                                   new T_GiftsChild()
                                   {

                                       CreatedBy = u.RealName,
                                       CreatedOn = DateTime.Now,
                                       IsDeleted = false,
                                       IsUsed = 0,
                                       GenerationTime = DateTime.Now,
                                       CouponNo = d1["CouponNo"].ToString()

                                   });



                                 }



                                 d.T_GiftChild.AddRange(a1);
                                 g.T_GiftsChild = a1;
                                 if (g.GiftInventory == null)
                                 {
                                     g.GiftInventory = 0;
                                 }
                                 g.GiftInventory = g.GiftInventory + ct;

                                 //记录详情
                                 pd.FileName = file.FileName;
                                 pd.Count = ct - a3.Count();
                                 pd.Name = u.RealName;
                                 pd.Time = DateTime.Now;
                                 d.T_OthersGiftsRecord.Add(pd);
                                 d.SaveChanges();

                             }



                         }
                         else
                         {
                             b1 = 1;
                         }

                     }
                     else
                     {
                         b = 1;
                     }








                 }



                 );
            //a.Start();
            a.Wait();


            var st = a.Status;
            if (a.Status == TaskStatus.RanToCompletion)
            {
                if (b1 == 1)
                {


                    return Json(new { col = b1 });
                }




                if (b == 0)
                {


                    dynamic q = new
                    {
                        //文件行数
                        Count = ct,
                        //重复行数
                        c = a3,

                        t = ct - a3.Count()

                    };

                    var q1 = new { q, c1 = 1000 };

                    return Json(q1);
                }
                else
                {
                    return Json(0);

                }






            }
            else
            {
                return Json(0);

            }



        }


        /// <summary>读取excel
        /// 默认第一行为表头
        /// </summary>
        /// <param name="strFileName">excel文档绝对路径</param>
        /// <param name="rowIndex">内容行偏移量，第一行为表头，内容行从第二行开始则为1</param>
        /// <returns></returns>
        public static DataTable Import(string strFileName, int rowIndex)
        {
            DataTable dt = new DataTable();

            IWorkbook hssfworkbook;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                hssfworkbook = WorkbookFactory.Create(file);
            }
            ISheet sheet = hssfworkbook.GetSheetAt(0);
            //
            IRow headRow = sheet.GetRow(0);
            if (headRow != null)
            {
                int colCount = headRow.LastCellNum;
                for (int i = 0; i < colCount; i++)
                {
                    dt.Columns.Add("COL_" + i);
                }
            }

            for (int i = (sheet.FirstRowNum + rowIndex); i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                bool emptyRow = true;
                object[] itemArray = null;

                if (row != null)
                {
                    itemArray = new object[row.LastCellNum];

                    for (int j = row.FirstCellNum; j < row.LastCellNum; j++)
                    {

                        if (row.GetCell(j) != null)
                        {

                            switch (row.GetCell(j).CellType)
                            {
                                case CellType.Numeric:
                                    if (HSSFDateUtil.IsCellDateFormatted(row.GetCell(j)))//日期类型
                                    {
                                        itemArray[j] = row.GetCell(j).DateCellValue.ToString("yyyy-MM-dd");
                                    }
                                    else//其他数字类型
                                    {
                                        itemArray[j] = row.GetCell(j).NumericCellValue;
                                    }
                                    break;
                                case CellType.Blank:
                                    itemArray[j] = string.Empty;
                                    break;
                                case CellType.Formula:
                                    if (Path.GetExtension(strFileName).ToLower().Trim() == ".xlsx")
                                    {
                                        XSSFFormulaEvaluator eva = new XSSFFormulaEvaluator(hssfworkbook);
                                        if (eva.Evaluate(row.GetCell(j)).CellType == CellType.Numeric)
                                        {
                                            if (HSSFDateUtil.IsCellDateFormatted(row.GetCell(j)))//日期类型
                                            {
                                                itemArray[j] = row.GetCell(j).DateCellValue.ToString("yyyy-MM-dd");
                                            }
                                            else//其他数字类型
                                            {
                                                itemArray[j] = row.GetCell(j).NumericCellValue;
                                            }
                                        }
                                        else
                                        {
                                            itemArray[j] = eva.Evaluate(row.GetCell(j)).StringValue;
                                        }
                                    }
                                    else
                                    {
                                        HSSFFormulaEvaluator eva = new HSSFFormulaEvaluator(hssfworkbook);
                                        if (eva.Evaluate(row.GetCell(j)).CellType == CellType.Numeric)
                                        {
                                            if (HSSFDateUtil.IsCellDateFormatted(row.GetCell(j)))//日期类型
                                            {
                                                itemArray[j] = row.GetCell(j).DateCellValue.ToString("yyyy-MM-dd");
                                            }
                                            else//其他数字类型
                                            {
                                                itemArray[j] = row.GetCell(j).NumericCellValue;
                                            }
                                        }
                                        else
                                        {
                                            itemArray[j] = eva.Evaluate(row.GetCell(j)).StringValue;
                                        }
                                    }
                                    break;
                                default:
                                    itemArray[j] = row.GetCell(j).StringCellValue;
                                    break;

                            }

                            if (itemArray[j] != null && !string.IsNullOrEmpty(itemArray[j].ToString().Trim()))
                            {
                                emptyRow = false;
                            }
                        }
                    }





                }

                //非空数据行数据添加到DataTable
                if (!emptyRow)
                {
                    dt.Rows.Add(itemArray);


                }


            }

            if (dt.Columns.Count > 0)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    dt.Columns[j].ColumnName = dt.Rows[0][j].ToString();
                }

                dt.Rows.Remove(dt.Rows[0]);
            }

            return dt;
        }

        #endregion

        #region  自主证券
        public ActionResult PrivateGiftsCreateMuch(int? id)
        {
            var q = DB.T_Gifts.Where(x => x.id == id).FirstOrDefault();

            return View(q);
        }
        [ValidateInput(false), HttpPost]

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
                                CouponNo = g.OpenCodeCouponNo + i + "_" + Guid.NewGuid().ToString() + i



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
                     T_PrivateGiftsRecord re = new T_PrivateGiftsRecord();
                     re.Count = GiftInventory;
                     re.Time = DateTime.Now;
                     re.Name = u.RealName;
                     d.T_PrivateGiftsRecord.Add(re);
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
        [ValidateInput(false), HttpPost]

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