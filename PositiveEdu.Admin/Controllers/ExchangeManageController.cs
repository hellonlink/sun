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
    public class ExchangeManageController : BaseController
    {
        // GET: ExchangeManage


        #region 兑换管理
        public ActionResult Index(int page = 1)
        {
            int pageSize = 15;
            var query = DB.T_ExchangeGift.AsNoTracking().AsQueryable();
            //礼品名称
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
            //礼品编号
            var GiftNo = Request.QueryString["GiftNo"] == null ? null : (Request.QueryString["GiftNo"].ToString());
            if (!string.IsNullOrEmpty(GiftNo))
            {
                query = query.Where(x => x.T_Gifts.GiftNo.Contains(GiftNo));
                ViewBag.GiftNo = GiftNo;
            }
            else
            {
                ViewBag.GiftNo = null;

            }
            //会员编号
            var _CustomerId = Request.QueryString["CustomerId"] == null ? null : Request.QueryString["CustomerId"].ToString();
            if (!string.IsNullOrEmpty(_CustomerId))
            {
                query = query.Where(x => x.T_Customer.CustomerId.Contains(_CustomerId));
                ViewBag._CustomerId = _CustomerId;

            }
            else
            {
                ViewBag._CustomerId = null;

            }
            //会员名称

            var _CustomerRealName = Request.QueryString["CustomerRealName"] == null ? null : (Request.QueryString["CustomerRealName"].ToString());
            if (!string.IsNullOrEmpty(_CustomerRealName))
            {
                query = query.Where(x => x.T_Customer.CustomerRealName.Contains(_CustomerRealName));
                ViewBag._CustomerRealName = _CustomerRealName;
            }
            else
            {
                ViewBag._CustomerRealName = null;

            }
            //所属分类
            var T_GiftsTagId = Request.QueryString["T_GiftsTagId"] == null ? null : (Request.QueryString["T_GiftsTagId"].ToString());
            if (!string.IsNullOrEmpty(T_GiftsTagId))
            {
                if (T_GiftsTagId == "全部")
                {

                }
                else
                {
                    int? af = ((int?)Convert.ToInt32(T_GiftsTagId));
                    query = query.Where(x => x.T_Gifts.T_GiftsTagId == af);
                    ViewBag.T_GiftsTagId = T_GiftsTagId;
                }

            }
            else
            {
                ViewBag.T_GiftsTagId = "全部";

            }
            //礼品类型

            var GiftType = Request.QueryString["GiftType"] == null ? null : (Request.QueryString["GiftType"].ToString());
            if (!string.IsNullOrEmpty(GiftType))
            {

                int? af = ((int?)Convert.ToInt32(GiftType));
                query = query.Where(x => x.T_Gifts.GiftType == af);
                ViewBag.GiftType = GiftType;


            }
            else
            {
                ViewBag.GiftType = null;

            }

            //发送时间
            var ExchangeStartTime = Request.QueryString["ExchangeStartTime"] == null ? null : (Request.QueryString["ExchangeStartTime"].ToString());
            if (!string.IsNullOrEmpty(ExchangeStartTime))
            {

                var s1 = Convert.ToDateTime(ExchangeStartTime);
                query = query.Where(x => x.CreatedOn >= s1);
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
                query = query.Where(x => x.CreatedOn <= s1);
                ViewBag.ExchangeStopTime = ExchangeStopTime;


            }
            else
            {
                ViewBag.ExchangeStopTime = null;

            }
            var result = query.OrderBy(x => x.Id).ToPagedList(page, pageSize);

            return View(result);
        }
        /// <summary>
        /// 实体礼品作废
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult PoolDetail(int? id)
        {


            try
            {
                #region 数据作废


                //数据修改
                var a1 = DB.T_ExchangeGift.Where(x => x.Id == id).FirstOrDefault();
                var u = JsonConvert.DeserializeObject<AuthAdmin>(User.Identity.Name);

                //用户兑换表
                a1.IsDeleted = true;
                a1.UpdatedBy = u.RealName;
                a1.UpdatedOn = DateTime.Now;
                //用户表
                a1.T_Customer.CustomerCurrentIntegral = a1.T_Customer.CustomerCurrentIntegral + a1.UserIntegral;
                a1.T_Customer.UpdatedOn = DateTime.Now;
                a1.T_Customer.UpdatedBy = u.RealName;


                //积分记录表
                T_CustomerIntegralRecord a2 = new T_CustomerIntegralRecord()
                {
                    CreatedBy = u.RealName,
                    CreatedOn = DateTime.Now,
                    ExchangeType = 2,
                    ExchangeReason = "礼品作废",
                    IsDeleted = false,
                    AfterExchangeintegralValue = a1.T_Customer.CustomerCurrentIntegral,
                    integralExchangeValue = "+" + a1.UserIntegral,
                    T_Customer = a1.T_Customer,
                    ExchangeTime = DateTime.Now
                };


                //提交
                DB.SaveChanges();

                #endregion

            }
            catch (Exception ex)
            {

            }


            return View("Index");
        }

        /// <summary>
        /// 实体兑换修改
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult T_ExchangeGiftEdit(int? id)
        {

            return View(DB.T_ExchangeGift.Where(x => x.Id == id).FirstOrDefault());

        }
        /// <summary>
        /// 实体兑换修改
        /// </summary>
        /// <returns></returns>
        [ValidateAntiForgeryToken, ValidateInput(false), HttpPost]
        public ActionResult T_ExchangeGiftEdit()
        {


            try
            {
                var u = JsonConvert.DeserializeObject<AuthAdmin>(User.Identity.Name);
                var id = Convert.ToInt32(Request.Form["id"].ToString());
                var a = DB.T_ExchangeGift.Where(x => x.Id == id).FirstOrDefault();
                //联系人
                a.AcceptName = Request.Form["AcceptName"] == "" ? a.AcceptName : Request.Form["AcceptName"].ToString();
                a.AcceptPhoneNum = Request.Form["AcceptPhoneNum"] == "" ? a.AcceptPhoneNum : Request.Form["AcceptPhoneNum"].ToString();
                //快递
                a.CourierCompany = Request.Form["CourierCompany"] == "" ? a.CourierCompany : Request.Form["CourierCompany"].ToString();
                a.CourierNum = Request.Form["CourierNum"] == "" ? a.CourierNum : Request.Form["CourierNum"].ToString();
                a.AcceptPostNum = Request.Form["AcceptPostNum"] == "" ? a.AcceptPostNum : Request.Form["AcceptPostNum"].ToString();
                //地址
                a.AcceptAddressProvince = Request.Form["province"] == "" ? a.AcceptAddressProvince : Request.Form["province"].ToString();
                a.AcceptAddressCity = Request.Form["city"] == "" ? a.AcceptAddressCity : Request.Form["city"].ToString();
                a.AcceptAddressDistrict = Request.Form["town"] == "" ? a.AcceptAddressDistrict : Request.Form["town"].ToString();
                a.AcceptAddressDetail = Request.Form["AcceptAddressDetail"] == "" ? a.AcceptAddressDetail : Request.Form["AcceptAddressDetail"].ToString();
                //时间
                a.CompleteTime = Request.Form["CompleteTime"] == "" ? a.CompleteTime : Convert.ToDateTime(Request.Form["CompleteTime"].ToString());
                a.ObtainTime = Request.Form["ObtainTime"] == "" ? a.ObtainTime : Convert.ToDateTime(Request.Form["ObtainTime"].ToString());
                //状态
                a.CourierStatus = Convert.ToInt32(Request.Form["CourierStatus"].ToString());
                a.PickType = Convert.ToInt32(Request.Form["PickType"].ToString());
                a.UpdatedBy = u.RealName;
                a.UpdatedOn = DateTime.Now;
                DB.SaveChanges();
            }
            catch (Exception ex)
            {

            }




            return RedirectToAction("Index");


        }

        #endregion


    }
}