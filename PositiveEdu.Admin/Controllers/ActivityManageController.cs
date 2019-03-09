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
    public class ActivityManageController : BaseController
    {
        // GET: ActivityManage



        #region 活动管理
        /// <summary>
        /// 活动列表
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult Index(int page = 1)
        {
            int pageSize = 15;
            var query = DB.T_Activity.AsNoTracking().AsQueryable();
            var ActivityName = Request.QueryString["ActivityName"] == null ? null : (Request.QueryString["ActivityName"].ToString());
            if (!string.IsNullOrEmpty(ActivityName))
            {
                query = query.Where(x => x.ActivityName.Contains(ActivityName));
                ViewBag.ActivityName = ActivityName;
            }
            else
            {
                ViewBag.ActivityName = null;

            }
            var IsActivityEffective = Request.QueryString["IsActivityEffective"] == null ? null : (Request.QueryString["IsActivityEffective"].ToString());
            if (!string.IsNullOrEmpty(IsActivityEffective))
            {
                int? af = ((int?)Convert.ToInt32(IsActivityEffective));
                query = query.Where(x => x.IsActivityEffective == af);
                ViewBag.IsActivityEffective = IsActivityEffective;
            }
            else
            {
                ViewBag.IsActivityEffective = null;

            }
            var result = query.OrderBy(x => x.Id).ToPagedList(page, pageSize);

            return View(result);
        }

        /// <summary>
        /// 活动详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult Detail(int Id)
        {

            var result = DB.T_Activity.Where(x => x.Id == Id).FirstOrDefault();


            return View(result);
        }

        /// <summary>
        /// 修改活动
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult Edit(int Id)
        {

            var result = DB.T_Activity.Where(x => x.Id == Id).FirstOrDefault();
            return View(result);
        }
        [ValidateAntiForgeryToken, ValidateInput(false), HttpPost]

        public ActionResult Edit(int? id)
        {

            var a = DB.T_Activity.Where(x => x.Id == (int)id).FirstOrDefault();
            a.ActivityName = Request.Form["ActivityName"] == "" ? a.ActivityName : Request.Form["ActivityName"].ToString();
            a.GUID = Request.Form["GUID"] == null ? a.GUID : Request.Form["GUID"].ToString();
            a.RegistrationStartTime = Request.Form["RegistrationStartTime"] == "" ? (a.RegistrationStartTime = null) : Convert.ToDateTime(Request.Form["RegistrationStartTime"].ToString());
            a.RegistrationStopTime = Request.Form["RegistrationStopTime"] == "" ? (a.RegistrationStopTime = null) : Convert.ToDateTime(Request.Form["RegistrationStopTime"].ToString());
            a.ActivityStartTime = Request.Form["ActivityStartTime"] == "" ? (a.ActivityStartTime = null) : Convert.ToDateTime(Request.Form["ActivityStartTime"].ToString());
            a.ActivityStopTime = Request.Form["ActivityStopTime"] == "" ? (a.ActivityStopTime = null) : Convert.ToDateTime(Request.Form["ActivityStopTime"].ToString());

            a.RegistrationNumber = Request.Form["RegistrationNumber"] == "" ? a.RegistrationNumber : Convert.ToInt32(Request.Form["RegistrationNumber"].ToString());
            a.JoinNumber = Request.Form["JoinNumber"] == "" ? a.JoinNumber : Convert.ToInt32(Request.Form["JoinNumber"].ToString());
            a.IsAllTake = Request.Form["IsAllTake"] == "" ? a.IsAllTake : Convert.ToInt32(Request.Form["IsAllTake"].ToString());
            a.NeedIntegral = Request.Form["NeedIntegral"] == "" ? (a.NeedIntegral = null) : Convert.ToInt32(Request.Form["NeedIntegral"].ToString());
            a.IsAllSameReward = Request.Form["IsAllSameReward"] == null ? (a.IsAllSameReward = null) : Convert.ToInt32(Request.Form["IsAllSameReward"].ToString());
            a.IsActivityEffective = Request.Form["IsActivityEffective"] == null ? (a.IsActivityEffective = null) : Convert.ToInt32(Request.Form["IsActivityEffective"].ToString());





            a.UpdatedOn = DateTime.Now;
            var u = JsonConvert.DeserializeObject<AuthAdmin>(User.Identity.Name);
            a.UpdatedBy = u.RealName;
            if (a != null)
            {
                DB.SaveChanges();


            }

            return RedirectToAction("Index");


        }

        /// <summary>
        ///新增活动 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var result = new T_Activity();
            return View(result);
        }
        [ValidateAntiForgeryToken, ValidateInput(false), HttpPost]

        public ActionResult Create(int? id)
        {

            var a = new T_Activity();
            a.ActivityName = Request.Form["ActivityName"] == "" ? null : Request.Form["ActivityName"].ToString();
            a.GUID = Request.Form["GUID"] == "" ? a.GUID : Request.Form["GUID"].ToString();
            a.RegistrationStartTime = Request.Form["RegistrationStartTime"] == "" ? a.RegistrationStartTime : Convert.ToDateTime(Request.Form["RegistrationStartTime"].ToString());
            a.RegistrationStopTime = Request.Form["RegistrationStopTime"] == "" ? a.RegistrationStopTime : Convert.ToDateTime(Request.Form["RegistrationStopTime"].ToString());
            a.ActivityStartTime = Request.Form["ActivityStartTime"] == "" ? a.ActivityStartTime : Convert.ToDateTime(Request.Form["ActivityStartTime"].ToString());
            a.ActivityStopTime = Request.Form["ActivityStopTime"] == "" ? a.ActivityStopTime : Convert.ToDateTime(Request.Form["ActivityStopTime"].ToString());

            a.RegistrationNumber = Request.Form["RegistrationNumber"] == "" ? a.RegistrationNumber : Convert.ToInt32(Request.Form["RegistrationNumber"].ToString());
            a.JoinNumber = Request.Form["JoinNumber"] == "" ? a.JoinNumber : Convert.ToInt32(Request.Form["JoinNumber"].ToString());
            a.IsAllTake = Request.Form["IsAllTake"] == null ? a.IsAllTake : Convert.ToInt32(Request.Form["IsAllTake"].ToString());
            a.NeedIntegral = Request.Form["NeedIntegral"] == "" ? a.NeedIntegral : Convert.ToInt32(Request.Form["NeedIntegral"].ToString());
            a.IsAllSameReward = Request.Form["IsAllSameReward"] == null ? a.IsAllSameReward : Convert.ToInt32(Request.Form["IsAllSameReward"].ToString());
            a.IsActivityEffective = Request.Form["IsActivityEffective"] == null ? a.IsActivityEffective : Convert.ToInt32(Request.Form["IsActivityEffective"].ToString());





            a.CreatedOn = DateTime.Now;
            var u = JsonConvert.DeserializeObject<AuthAdmin>(User.Identity.Name);
            a.CreatedBy = u.RealName;


            DB.T_Activity.Add(a);
            DB.SaveChanges();



            return RedirectToAction("Index");


        }
        /// <summary>
        ///移除活动 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult Delete(int Id)
        {

            var result = DB.T_Activity.Where(x => x.Id == Id).FirstOrDefault();
            if (result.T_CustomerActivity.Count() > 0)
            {

            }
            else
            {
                DB.T_Activity.Remove(result);
                DB.SaveChanges();
            }


            return RedirectToAction("Index");


        }


        /// <summary>
        ///活动礼品配置
        /// </summary>
        /// <returns></returns>
        public ActionResult ActivityGiftsConfig()
        {
            var result = new T_Activity();
            return View(result);
        }

        #endregion


        #region 活动报名管理

        #endregion


    }
}