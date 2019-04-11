using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using PagedList;
using PositiveEdu.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

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


            try
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
            }
            catch (Exception ex)
            {

              
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
        /// <summary>
        ///活动奖项配置 
        /// </summary>
        /// <returns></returns>
        public ActionResult CreadReward(int? id)
        {
            var result = DB.T_Activity.Where(x => x.Id == id).FirstOrDefault();
            return View(result);
        }
        /// <summary>
        ///获取礼品数据
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false), HttpPost]
        public ActionResult GetGifts()
        {
            //创建数据字典<礼品类型 ,该类型下所有的礼品(实体和虚拟)>
            Dictionary<T_GiftsTag, dynamic> _Date = new Dictionary<T_GiftsTag, dynamic>();
            //初始化礼品类型集合
            List<T_GiftsTag> _T_GiftsTag = new List<T_GiftsTag>();
            //查询所有礼品类型
            _T_GiftsTag = DB.T_GiftsTag.ToList();
            //遍历并且填充数据
            foreach (var item in _T_GiftsTag)
            {
                //根据类型获取所有该类型下的礼品(实体和虚拟) 
                var _T_Gifts = DB.T_Gifts.Where(x => x.T_GiftsTagId == item.id).ToList();
                //初始化虚拟礼品集合
                //List<T_GiftsChild> _T_GiftsChild = new List<T_GiftsChild>();
                //遍历该类型的礼品集合
                _T_Gifts = _T_Gifts.Where(x => x.GiftInventory > 0).ToList();
                if (_T_Gifts.Count > 0)
                {
                    _Date.Add(item, _T_Gifts);
                }


            }




            //DB.Configuration.ProxyCreationEnabled = false;
            ////返回结果集
            var result = JsonConvert.SerializeObject(_Date.ToList());



            return Json(result);

        }
        /// <summary>
        ///获取当前活动奖项
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false), HttpPost]
        public ActionResult GetRewards(string id)
        {
            var d = Convert.ToInt32(id);
            var c = DB.T_Activity.Where(x => x.Id == d).FirstOrDefault();
            var p1 = c.T_Reward;
            List<Dt1> t = new List<Dt1>();
            foreach (var item in p1)
            {
                Dt1 t1 = new Dt1()
                {
                    T_Reward = item,
                    T_RewardChild = item.T_RewardChild.ToList()
                };
                t.Add(t1);
            }
            var a3 = JsonConvert.SerializeObject(t);
            return Json(a3);
        }
        /// <summary>
        ///移除活动奖项
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false), HttpPost]
        public ActionResult RemoveRewards(string date, string id)
        {
            //获取数据  
            var d = Convert.ToInt32(id);
            try
            {
                var t = DB.T_Reward.Where(x => x.Id == d).FirstOrDefault();
                //移除礼品数据
                foreach (var item in t.T_RewardChild)
                {
                    //库存回滚
                    item.T_Gifts.GiftInventory = item.T_Gifts.GiftInventory + item.GiftNumber;

                }
                DB.T_RewardChild.RemoveRange(t.T_RewardChild);
                //移除父项
                DB.T_Reward.Remove(t);
                DB.SaveChanges();
            }
            catch (Exception e)
            {

                throw;
            }

            var t1 = DB.T_Reward.Where(x => x.Id == d).FirstOrDefault();
            if (t1 != null)
            {
                return Json("0");
            }
            else
            {
                return Json("1");
            }

        }

        public ActionResult TakeActivity(int? id, int page = 1)
        {
            var T_Activity = DB.T_Activity.Where(x => x.Id == id).FirstOrDefault();
            ViewBag.ad = id;
            int pageSize = 15;
            ViewBag.Name = T_Activity.ActivityName;
            ViewBag.txt = "";
            var query = DB.T_CustomerActivity.AsNoTracking().AsQueryable();

            query = query.Where(x => x.T_ActivityId == id);
            //报名时间段：
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
            //会员手机号
            var _CustomerPhoneNum = Request.QueryString["CustomerPhoneNum"] == null ? null : Request.QueryString["CustomerPhoneNum"].ToString();
            if (!string.IsNullOrEmpty(_CustomerPhoneNum))
            {
                query = query.Where(x => x.T_Customer.CustomerPhoneNum.Contains(_CustomerPhoneNum));
                ViewBag._CustomerPhoneNum = _CustomerPhoneNum;

            }
            else
            {
                ViewBag._CustomerPhoneNum = null;

            }
            //是否参与成功     
            var ts = Request.QueryString["ts"] == null ? null : Request.QueryString["ts"].ToString();
            if (!string.IsNullOrEmpty(ts))
            {
                if (ts == "0")
                {
                    ViewBag.ts = ts;

                }
                else
                {


                    if (ts == "1")
                    {
                        query = query.Where(x => x.IsSuccessTake == true);

                    }
                    if (ts == "2")
                    {
                        query = query.Where(x => x.IsSuccessTake == false);
                        ViewBag.ts = ts;
                    }
                    ViewBag.ts = ts;
                }

            }
            //所获奖项
            var reward = Request.QueryString["reward"] == null ? null : Request.QueryString["reward"].ToString();
            if (!string.IsNullOrEmpty(reward))
            {
                if (reward == "0")
                {

                }
                else
                {
                    var d = Convert.ToInt32(reward);

                    var a = DB.T_Reward.Where(x => x.Id == d).ToList();
                    query = query.Where(x => x.T_RewardId == d);

                    ViewBag.reward = reward;
                }

            }
            else
            {
                ViewBag.reward = "0";
            }
            var role = Request.QueryString["role"];
            //var result = query.ToList();
            var result = query.OrderBy(x => x.Id).ToPagedList(page, pageSize);
            return View(result);
        }

        /// <summary>
        /// 手工报名
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns> 
        public ActionResult HumanTakeActivity(int? id, int page = 1)
        {
            var T_Activity = DB.T_Activity.Where(x => x.Id == id).FirstOrDefault();
            ViewBag.ad = id;
            int pageSize = 15;
            ViewBag.Name = T_Activity.ActivityName;
            ViewBag.txt = "";
            var query = DB.T_Customer.AsNoTracking().AsQueryable();


            var _CustomerRealName = Request.QueryString["CustomerRealName"] == null ? null : (Request.QueryString["CustomerRealName"].ToString());
            if (!string.IsNullOrEmpty(_CustomerRealName))
            {
                query = query.Where(x => x.CustomerRealName.Contains(_CustomerRealName));
                ViewBag._CustomerRealName = _CustomerRealName;
            }
            else
            {
                ViewBag._CustomerRealName = null;

            }
            var _CustomerPhoneNum = Request.QueryString["CustomerPhoneNum"] == null ? null : Request.QueryString["CustomerPhoneNum"].ToString();
            if (!string.IsNullOrEmpty(_CustomerPhoneNum))
            {
                query = query.Where(x => x.CustomerPhoneNum.Contains(_CustomerPhoneNum));
                ViewBag._CustomerPhoneNum = _CustomerPhoneNum;

            }
            else
            {
                ViewBag._CustomerPhoneNum = null;

            }
            var _CustomerWechatNickName = Request.QueryString["CustomerWechatNickName"] == null ? null : Request.QueryString["CustomerWechatNickName"].ToString();
            if (!string.IsNullOrEmpty(_CustomerWechatNickName))
            {
                query = query.Where(x => x.CustomerWechatNickName.Contains(_CustomerWechatNickName));
                ViewBag._CustomerWechatNickName = _CustomerWechatNickName;

            }
            else
            {
                ViewBag._CustomerWechatNickName = null;

            }
            var _CustomerCertificateNum = Request.QueryString["CustomerCertificateNum"] == null ? null : Request.QueryString["CustomerCertificateNum"].ToString();
            if (!string.IsNullOrEmpty(_CustomerCertificateNum))
            {
                query = query.Where(x => x.CustomerCertificateNum.Contains(_CustomerCertificateNum));
                ViewBag._CustomerCertificateNum = _CustomerCertificateNum;

            }
            else
            {
                ViewBag._CustomerCertificateNum = null;

            }
            var _CustomerId = Request.QueryString["CustomerId"] == null ? null : Request.QueryString["CustomerId"].ToString();
            if (!string.IsNullOrEmpty(_CustomerId))
            {
                query = query.Where(x => x.CustomerId.Contains(_CustomerId));
                ViewBag._CustomerId = _CustomerId;

            }
            else
            {
                ViewBag._CustomerId = null;

            }
            var _CustomerSex = Request.QueryString["payment"] == null ? null : Request.QueryString["payment"].ToString();
            if (!string.IsNullOrEmpty(_CustomerSex))
            {
                if (_CustomerSex == "全部")
                {
                    ViewBag.payment = _CustomerSex;

                }
                else
                {
                    query = query.Where(x => x.CustomerSex.Contains(_CustomerSex));

                    if (_CustomerSex == "未知")
                    {
                        ViewBag.payment = "未知";
                    }
                    if (_CustomerSex == "女")
                    {
                        ViewBag.payment = "女";
                    }
                    if (_CustomerSex == "男")

                    {
                        ViewBag.payment = "男";

                    }
                }

            }
            else
            {
                ViewBag.payment = "全部";
            }
            var role = Request.QueryString["role"];
            //var result = query.ToList();
            var result = query.OrderBy(x => x.Id).ToPagedList(page, pageSize);
            return View(result);
        }
        /// <summary>
        /// 手工报名ajax请求
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ValidateInput(false), HttpPost]
        public ActionResult TakeActivityByHuman(string data, string id)
        {
            int s = 0;//成功行数
            int f = 0;//失败行数
            //获取数组
            var b = data.Split(',');
            //获取当前操作用户
            var u = JsonConvert.DeserializeObject<AuthAdmin>(User.Identity.Name);
            //当前活动ID
            var a1 = Convert.ToInt16(id);
            //遍历添加
            var m = "";
            for (int i = 0; i < b.Length - 1; i++)
            {
                try
                {
                    //当前遍历会员id
                    var a2 = Convert.ToInt32(b[i]);
                    var t1 = DB.T_Activity.Where(x => x.Id == a1).FirstOrDefault();
                    var t2 = DB.T_Customer.Where(x => x.Id == a2).FirstOrDefault();
                    #region 活动规则验证
                    //报名时间验证
                    if (t1.RegistrationStartTime < DateTime.Now && t1.RegistrationStopTime > DateTime.Now)
                    {
                        //报名人数验证
                        if (t1.RegistrationNumber >= t1.T_CustomerActivity.Count())
                        {
                            #region  会员加入活动
                            //初始化
                            T_CustomerActivity t = new T_CustomerActivity()
                            {
                                T_Activity = t1,
                                T_Customer = t2,
                                IsSuccessTake = false,
                                IsCanTake = 0,
                                IsDeleted = false,
                                CreatedBy = u.RealName,
                                CreatedOn = DateTime.Now,
                                RegistrationTime = DateTime.Now
                            };
                            //加入数据库
                            DB.T_CustomerActivity.Add(t);
                            DB.SaveChanges();
                            s++;
                            #endregion
                        }
                        else
                        {
                            m = "报名名额已满！";
                            break;
                        }

                    }
                    else
                    {
                        m = "不在报名时间内！";

                        break;
                    }

                    #endregion





                }
                catch (Exception e)
                {
                    f++;

                    continue;
                }

            }
            dynamic a = new
            {
                s,
                f,
                m
            };
            ViewBag.txt = "<h2>此次加入:" + a.s + "名 失败:" + a.f + "名</h2>"; ;
            return Json(a);
        }


        /// <summary>
        /// 所选会员通过参与资格
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ValidateInput(false), HttpPost]
        public ActionResult TakeActivityEdit(string data)
        {
            //获取数组
            var b = data.Split(',');
            //获取当前操作用户
            var u = JsonConvert.DeserializeObject<AuthAdmin>(User.Identity.Name);
            //错误原因
            var m = "";
            int s = 0;//成功行数
            int f = 0;//失败行数
                      //遍历添加
            for (int i = 0; i < b.Length - 1; i++)
            {
                try
                {
                    //当前遍历会员id
                    var a2 = Convert.ToInt32(b[i]);
                    var t1 = DB.T_CustomerActivity.Where(x => x.Id == a2).FirstOrDefault();

                    #region 参与活动规则验证
                    //参与时间验证
                    //活动未开始
                    if (t1.T_Activity.ActivityStartTime >= DateTime.Now)
                    {

                        //参与人数验证
                        if (t1.T_Activity.JoinNumber > t1.T_Activity.T_CustomerActivity.Count())
                        {
                            //参与人数未满员


                            //获取用户当前积分
                            if (t1.T_Customer.CustomerCurrentIntegral != null)
                            {
                                //当前用户积分是否大于活动参与积分
                                if (t1.T_Activity.NeedIntegral <= t1.T_Customer.CustomerCurrentIntegral)
                                {

                                    //修改 
                                    t1.IsSuccessTake = true;
                                    t1.IsCanTake = 1;
                                    t1.UpdatedBy = u.RealName;
                                    t1.UpdatedOn = DateTime.Now;
                                    //如果大于则减去
                                    t1.T_Customer.CustomerCurrentIntegral = t1.T_Customer.CustomerCurrentIntegral - t1.T_Activity.NeedIntegral;
                                    //记录
                                    DB.T_CustomerIntegralRecord.Add(new T_CustomerIntegralRecord()
                                    {

                                        CreatedBy = u.RealName,
                                        CreatedOn = DateTime.Now,
                                        IsDeleted = false,
                                        T_Customer = t1.T_Customer,
                                        AfterExchangeintegralValue = t1.T_Customer.CustomerCurrentIntegral,
                                        integralExchangeValue = "-" + t1.T_Activity.NeedIntegral
                                    });

                                    //加入数据库
                                    DB.SaveChanges();
                                    s++;

                                }
                                else
                                {
                                    m = "用户积分不足";
                                    break;
                                }

                            }
                            else
                            {
                                m = "用户没有积分";
                                break;
                            }


                        }
                        else
                        {
                            //参与人数满员
                            m = "参与人数满员";
                            break;
                        }
                    }
                    else
                    {
                        //活动已经开始
                        m = "活动已经开始";
                        break;
                    }






                    #endregion

                }
                catch (Exception e)
                {
                    f++;
                    m = "异常:" + e;
                    continue;
                }

            }
            dynamic a = new
            {
                s,
                f,
                m

            };
            ViewBag.txt = "<h2>此次修改:" + a.s + "名 失败:" + a.f + "名</h2>"; ;
            return Json(a);
        }
        /// <summary>
        ///新增活动奖项
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false), HttpPost]
        public ActionResult CreadRewards(string date, string id, string RewardName, string RewardNumber, string OnlyId)
        {


            //获取数据源
            JavaScriptSerializer js = new JavaScriptSerializer();
            List<Dt> a = js.Deserialize<List<Dt>>(date);
            var d = Convert.ToInt32(id);
            var a1 = DB.T_Activity.Where(x => x.Id == d).FirstOrDefault();
            //处理数据
            var u = JsonConvert.DeserializeObject<AuthAdmin>(User.Identity.Name);
            //为活动添加礼品
            T_Reward t = new T_Reward()
            {
                CreatedBy = u.RealName,
                CreatedOn = DateTime.Now,
                IsDeleted = false,
                OnlyId = OnlyId,
                RewardNumber = Convert.ToInt32(RewardNumber),
                RewardRemaining = Convert.ToInt32(RewardNumber),
                RewardName = RewardName,
                RewardUsed = 0,
                T_Activity = a1
            };
            List<T_RewardChild> _T_RewardChild = new List<T_RewardChild>();
            foreach (var item in a)
            {
                //礼品数据修改
                var c1 = DB.T_Gifts.Where(x => x.GiftName == item.GiftName).FirstOrDefault();
                c1.GiftInventory = c1.GiftInventory - item.GiftInventory;
                //添加奖项子项
                _T_RewardChild.Add(new T_RewardChild
                {
                    CreatedBy = u.RealName,
                    CreatedOn = DateTime.Now,
                    GiftNumber = item.GiftInventory,
                    IsDeleted = false,
                    T_Reward = t,
                    T_Gifts = c1
                });
            }

            DB.T_RewardChild.AddRange(_T_RewardChild);
            DB.T_Reward.Add(t);
            DB.SaveChanges();

            var c = DB.T_Activity.Where(x => x.Id == d).FirstOrDefault();
            var p1 = c.T_Reward;

            Dictionary<T_Reward, List<T_RewardChild>> pd = new Dictionary<T_Reward, List<T_RewardChild>>();
            foreach (var item in p1)
            {
                pd.Add(item, item.T_RewardChild.ToList());

            }
            var a3 = JsonConvert.SerializeObject(pd);
            return Json(a3);

        }
        #region 导入报名


        public ActionResult ImportTakeActivity(int id)
        {

            var a = DB.T_Activity.Where(x => x.Id == id).FirstOrDefault();
            ViewBag.Name = a.ActivityName;
            ViewBag.id = a.Id;
            return View();
        }

        [ValidateInput(false), HttpPost]
        public ActionResult ReadFiles(HttpPostedFileBase file, string files, string sc, int? id)
        {
            id = Convert.ToInt32(files);
            //成功行
            var SucessCount = 0;
            //失败行
            var FailCount = 0;
            //获取当前行数
            var ct = 0;
            //返回结果信息
            List<Data3> d2 = new List<Data3>();
            T_OthersGiftsRecord pd = new T_OthersGiftsRecord();
            var tk = Task.Run(() =>
            {

                //创建EF对象
                PeContext db = new PeContext();
                if (file != null)
                {
                    #region 保存并读取导入文件
                    //序列化
                    //var ta = JsonConvert.DeserializeObject<List<Data1>>(files);
                    //var filename = Path.Combine(Request.MapPath("~/App_Data"), file.FileName);
                    //file.SaveAs(filename);
                    var filename = (new FileReadWrite()).SaveFile("T_CustomerActivity", file, Request);
                    var dt = Import(filename, 0);
                    var d1 = dt.CreateDataReader();
                    #endregion
                    #region 操作数据
                    var a1 = (new T_Customer()).GetType();
                    //用户集合
                    var t = db.T_Customer.ToList();
                    //当前活动
                    var a = db.T_Activity.Where(x => x.Id == id).FirstOrDefault();
                    //获取当前操作用户
                    var u = JsonConvert.DeserializeObject<AuthAdmin>(User.Identity.Name);

                    while (d1.Read())
                    {
                        ct++;
                        //记录信息
                        Data3 d3 = new Data3();
                        try
                        {
                            //数据过滤
                            var c = Re(sc, d1[0].ToString());
                            //反射+linq     
                            int cd = 0;
                            foreach (var item in t)
                            {
                                var v = "";
                                //反射获取当前会员对象的选定属性值
                                if (a1.GetProperty(sc).GetValue(item) != null)
                                {
                                    v = a1.GetProperty(sc).GetValue(item).ToString();
                                }
                                else
                                {

                                }


                                //确认相等
                                if (v == c)
                                {
                                    cd = item.Id;
                                    //如果存在
                                    break;
                                }
                            }

                            //var b2 = t.Where(x => x.GetType().GetProperty(sc).GetValue( == c).FirstOrDefault();
                            //存在当前用户
                            if (cd != 0)
                            {

                                var b2 = db.T_Customer.Where(x => x.Id == cd).FirstOrDefault();
                                //查询当前用户的活动
                                var b1 = b2.T_CustomerActivity.Where(x => x.T_ActivityId == id).FirstOrDefault();
                                //当前活动存在
                                if (b1 == null)
                                {
                                    db.T_CustomerActivity.Add(
                                        new T_CustomerActivity()
                                        {
                                            T_Activity = a,
                                            T_Customer = b2,
                                            IsSuccessTake = false,
                                            IsDeleted = false,
                                            CreatedBy = u.RealName,
                                            CreatedOn = DateTime.Now,
                                            RegistrationTime = DateTime.Now



                                        }

                                        );
                                    db.SaveChanges();
                                    //添加成功
                                    d3.row = ct;
                                    d3.message = @"成功导入第" + ct + "行";
                                    SucessCount++;
                                    //加入返回结果集
                                    d2.Add(d3);
                                }
                                else
                                {
                                    //该用户已存在
                                    d3.row = ct;
                                    d3.message = @"导入第" + ct + "行失败 该用户已报名，该行已跳过";
                                    FailCount++;
                                    //加入返回结果集
                                    d2.Add(d3);
                                    continue;
                                }

                            }
                            else
                            {
                                //该用户不存在
                                d3.row = ct;
                                d3.message = @"导入第" + ct + "行失败 该用户不存在，该行已跳过";
                                FailCount++;
                                //加入返回结果集
                                d2.Add(d3);
                                continue;
                            }


                        }
                        catch (Exception ex)
                        {
                            //导入异常
                            d3.row = ct;
                            d3.message = @"导入第" + ct + "行失败 该行已跳过，原因：" + ex;
                            FailCount++;
                            //加入返回结果集
                            d2.Add(d3);
                            continue;
                        }

                    }
                    #endregion
                    #region 添加入数据库
                    //记录操作


                    db.SaveChanges();

                    pd = new T_OthersGiftsRecord()
                    {
                        Count = ct,
                        Time = DateTime.Now,
                        FileName = file.FileName + " | 共计:" + ct + "行" + "成功导入" + SucessCount + "列",
                        Name = "T_CustomerActivity"

                    };
                }
                #endregion
            });
            //等待异步执行完成
            tk.Wait();
            //返回结果
            if (tk.Status == TaskStatus.RanToCompletion)
            {



                #region 导入结果文件生成
                var Message = "";
                Message = Message + "该文件共" + (SucessCount + FailCount) + "行成功导入" + SucessCount + "行，导入失败" + FailCount + "行\n";

                for (var i = 0; i < d2.Count(); i++)
                {
                    Message = Message + d2[i].message + "\n";
                }
                var m = (new FileReadWrite()).WriteFile(file.FileName, "T_CustomerActivity", Message, Request);
                #endregion


                //返回数据结构
                dynamic p1 = new
                {
                    a1 = d2,//返回信息

                    a3 = SucessCount,//成功行
                    a4 = FailCount,  //失败行
                    a5 = m
                };
                pd.FileName = pd.FileName + "-|<a href = '/CustomerManage/DownloadFile?path=" + m + "' > 导入结果下载</a>";
                DB.T_OthersGiftsRecord.Add(pd);
                DB.SaveChanges();
                //序列化数据
                var js = (new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue }.Serialize(p1));

                //返回数据
                return new JsonResult()
                {
                    Data = js,
                    MaxJsonLength = int.MaxValue,
                    ContentType = "application/json"
                };
            }
            else
            {
                return Json(0);
            }



        }
        /// <summary>
        /// 获取列名
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public string getStr(string s)
        {

            var c = "";
            switch (s)
            {

                case "CustomerId": c = "会员编号 "; break;
                case "CustomerPhoneNum": c = "会员手机号 "; break;
                case "CustomerWechatNickName": c = "会员微信昵称  "; break;
                case "CustomerWechatHeadPhoto": c = "会员微信头像  "; break;
                case "CustomerRealName": c = "会员真实姓名  "; break;
                case "CustomerSex": c = "性别  "; break;
                case "CustomerCertificateNum": c = "证件号码 "; break;
                case "CustomerBirthday": c = "生日 "; break;
                case "CustomerAddressProvince": c = "所属省 "; break;
                case "CustomerAddressCity": c = "所属市 "; break;
                case "CustomerAddressDistrict": c = "所属区 "; break;
                case "CustomerTakeTime": c = "会员入会时间 "; break;
                case "CustomerWechatNum": c = "微信号 "; break;
                case "CustomerWeiboNum": c = "微博号 "; break;
                case "CustomerQQNum": c = "QQ号 "; break;
                case "CustomerEmailNum": c = "Email "; break;
                case "CustomizeTag1": c = "客户自定义标签Tag1 "; break;
                case "CustomizeTag2": c = "客户自定义标签Tag2 "; break;
                case "CustomizeTag3": c = "客户自定义标签Tag3 "; break;
                case "CustomizeTag4": c = "客户自定义标签Tag4 "; break;
                case "CustomizeTag5": c = "客户自定义标签Tag5 "; break;
                case "CustomizeTag6": c = "客户自定义标签Tag6 "; break;
                case "CustomizeTag7": c = "客户自定义标签Tag7 "; break;
                case "CustomizeTag8": c = "客户自定义标签Tag8 "; break;
                case "CustomizeTag9": c = "客户自定义标签Tag9 "; break;
                case "CustomizeTag10": c = "客户自定义标签Tag10"; break;
            }
            return c;
        }
        /// <summary>
        ///属性过滤
        /// </summary>
        /// <param name="a1"></param>
        /// <param name="a2"></param>
        public string Re(string a1, string a2)
        {
            var c = "";
            if (!string.IsNullOrEmpty(a2))
            {
                //过滤
                a2 = Regx.ReHtml(a2);
                switch (a1)
                {
                    //电话号码
                    case "CustomerPhoneNum":
                        Regex reg0 = new Regex(Regx.s3);
                        if (reg0.IsMatch(a2.ToLower()))
                        {
                            c = a2;
                        }
                        else
                        {

                        }
                        break;
                    //会员编号
                    case "CustomerId":
                        c = a2;
                        break;
                    //微信昵称
                    case "CustomerWechatNickN":
                        c = a2;
                        break;
                    //会员微信头像
                    case "CustomerWechatHeadPhoto":
                        c = a2;
                        break;
                    //会员真实姓名
                    case "CustomerRealName":
                        c = a2;
                        break;
                    //性别
                    case "CustomerSex":
                        Regex reg6 = new Regex(@"^[男|女|未知]");
                        if (reg6.IsMatch(a2.ToLower()))
                        {
                            c = a2;
                        }
                        else
                        {

                        }

                        break;
                    //证件号码
                    case "CustomerCertificateNum":
                        Regex reg2 = new Regex(Regx.s6);
                        if (reg2.IsMatch(a2.ToLower()))
                        {
                            c = a2;
                        }
                        else
                        {

                        }
                        break;
                    //生日
                    case "CustomerBirthday":
                        try
                        {
                            var q = Convert.ToDateTime(a2);
                            c = a2;
                        }
                        catch (Exception)
                        {
                        }

                        break;
                    //所属省
                    case "CustomerAddressProvince":
                        c = a2;
                        break;
                    //所属市
                    case "CustomerAddressCity":
                        c = a2;
                        break;
                    //所属区
                    case "CustomerAddressDist":
                        c = a2;
                        break;
                    //会员入会时间
                    case "CustomerTakeTime":
                        try
                        {
                            var q = Convert.ToDateTime(a2);
                            c = a2;
                        }
                        catch (Exception)
                        {
                        }
                        break;
                    //微信号
                    case "CustomerWechatNum":
                        Regex reg3 = new Regex(Regx.s8);
                        if (reg3.IsMatch(a2.ToLower()))
                        {
                            c = a2;
                        }
                        else
                        {
                        }
                        break;
                    //微博号
                    case "CustomerWeiboNum":
                        c = a2;
                        break;
                    //QQ号
                    case "CustomerQQNum":
                        Regex reg4 = new Regex(Regx.s7);
                        if (reg4.IsMatch(a2.ToLower()))
                        {
                            c = a2;
                        }
                        else
                        {
                        }
                        break;
                    //邮箱
                    case "CustomerEmailNum":
                        Regex reg5 = new Regex(Regx.s5);
                        if (reg5.IsMatch(a2.ToLower()))
                        {
                            c = a2;
                        }
                        else
                        {
                        }
                        break;
                    // 客户自定义标签Tag1 
                    case "CustomizeTag1":
                        c = a2;
                        break;
                    // 客户自定义标签Tag2
                    case "CustomizeTag2":
                        c = a2;
                        break;
                    // 客户自定义标签Tag3
                    case "CustomizeTag3":
                        c = a2;
                        break;
                    // 客户自定义标签Tag4
                    case "CustomizeTag4":
                        c = a2;
                        break;
                    // 客户自定义标签Tag5
                    case "CustomizeTag5":
                        c = a2;
                        break;
                    // 客户自定义标签Tag6
                    case "CustomizeTag6":
                        c = a2;
                        break;
                    // 客户自定义标签Tag7
                    case "CustomizeTag7":
                        c = a2;
                        break;
                    // 客户自定义标签Tag1 
                    case "CustomizeTag8":
                        c = a2;
                        break;
                    // 客户自定义标签Tag9 
                    case "CustomizeTag9":
                        c = a2;
                        break;
                    //// 客户自定义标签Tag10
                    case "CustomizeTag10":
                        c = a2;
                        break;


                }

                return c;
            }
            else
            {
                return c;
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

        /// <summary>
        /// 修改会员
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult T_CustomerActivityEdit(int Id)
        {
            var result = DB.T_CustomerActivity.Where(x => x.Id == Id).FirstOrDefault();
            return View(result);
        }
        /// <summary>
        /// 修改会员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        [ValidateAntiForgeryToken, ValidateInput(false), HttpPost]
        public ActionResult T_CustomerActivityEdit(int? id)
        {
            var u = JsonConvert.DeserializeObject<AuthAdmin>(User.Identity.Name);
            var a = DB.T_CustomerActivity.Where(x => x.Id == (int)id).FirstOrDefault();
            a.ReciptName = Request.Form["ReciptName"] == "" ? null : Request.Form["ReciptName"].ToString();
            a.AcceptAddressPostCode = Request.Form["AcceptAddressPostCode"] == "" ? null : Request.Form["AcceptAddressPostCode"].ToString();
            a.CourierCompany = Request.Form["CourierCompany"] == "" ? null : Request.Form["CourierCompany"].ToString();
            a.DeliveryNumber = Request.Form["DeliveryNumber"] == "" ? a.DeliveryNumber : Request.Form["DeliveryNumber"].ToString();
            a.ReciptPhoneNum = Request.Form["ReciptPhoneNum"] == "" ? null : Request.Form["ReciptPhoneNum"].ToString();
            a.ReciptAddressProvince = Request.Form["province"] == "" ? a.ReciptAddressProvince : Request.Form["province"].ToString();
            a.ReciptAddressCity = Request.Form["city"] == "" ? a.ReciptAddressCity : Request.Form["city"].ToString();
            a.ReciptAddressDistrict = Request.Form["town"] == "" ? a.ReciptAddressDistrict : Request.Form["town"].ToString();
            a.ReciptDetailedAddress = Request.Form["ReciptDetailedAddress"] == "" ? null : Request.Form["ReciptDetailedAddress"].ToString();
            a.IsCanTake = Request.Form["IsCanTake"] == "" ? a.IsCanTake : Convert.ToInt32(Request.Form["IsCanTake"]);
            a.EntityRewardGettype = Request.Form["EntityRewardGettype"] == "" ? a.EntityRewardGettype : Convert.ToInt32(Request.Form["EntityRewardGettype"]);

            //获取奖项 关联操作
            if (Request.Form["reward"] != "0")
            {

                //用户活动表
                a.T_RewardId = Convert.ToInt32(Request.Form["reward"]);
                //奖项表
                var r = DB.T_Reward.Where(x => x.Id == a.T_RewardId).FirstOrDefault();
                //剩余份数
                if (r.RewardNumber > 0)
                {
                    //剩余份数
                    r.RewardRemaining = r.RewardNumber - 1;
                    //使用份数
                    r.RewardUsed = r.RewardUsed + 1;
                    r.UpdatedBy = u.RealName;
                    r.UpdatedOn = DateTime.Now;

                }

            }
            a.Tag1 = Request.Form["Tag1"] == "" ? null : Request.Form["Tag1"].ToString();
            a.Tag2 = Request.Form["Tag2"] == "" ? null : Request.Form["Tag2"].ToString();
            a.Tag3 = Request.Form["Tag3"] == "" ? null : Request.Form["Tag3"].ToString();
            a.Tag4 = Request.Form["Tag4"] == "" ? null : Request.Form["Tag4"].ToString();
            a.Tag5 = Request.Form["Tag5"] == "" ? null : Request.Form["Tag5"].ToString();

            a.UpdatedOn = DateTime.Now;

            a.UpdatedBy = u.RealName;
            if (a != null)
            {
                DB.SaveChanges();


            }

            return RedirectToAction("Index");


        }
        #endregion

        #endregion
    }
    /// <summary>
    /// 数据填充集合
    /// </summary>
    public class Dt
    {

        public string GiftName { get; set; }
        public int? GiftInventory { get; set; }

    }
    /// <summary>
    /// 数据填充集合
    /// </summary>
    public class Dt1
    {
        public T_Reward T_Reward { get; set; }
        public List<T_RewardChild> T_RewardChild { get; set; }
    }

}