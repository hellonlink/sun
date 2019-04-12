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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PositiveEdu.Admin.Controllers
{
    public class CustomerManageController : BaseController
    {
        #region 会员管理
        /// <summary>
        /// 会员列表
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        // GET: CustomerManage
        public ActionResult Index(int page = 1)
        {
            //NewMethod();

            int pageSize = 15;
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

            if (role != null)
            {
                var _SelectPayment1 = Request.QueryString["SelectPayment1"] == "" ? null : Request.QueryString["SelectPayment1"].ToString();
                var _SelectPayment2 = Request.QueryString["SelectPayment2"] == "" ? null : Request.QueryString["SelectPayment2"].ToString();
                var _SelectPayment3 = Request.QueryString["SelectPayment3"] == "" ? null : Request.QueryString["SelectPayment3"].ToString();
                var _SelectPayment4 = Request.QueryString["SelectPayment4"] == "" ? null : Request.QueryString["SelectPayment4"].ToString();
                var _SelectPayment5 = Request.QueryString["SelectPayment5"] == "" ? null : Request.QueryString["SelectPayment5"].ToString();
                var _SelectPayment6 = Request.QueryString["SelectPayment6"] == "" ? null : Request.QueryString["SelectPayment6"].ToString();
                var _SelectPayment7 = Request.QueryString["SelectPayment7"] == "" ? null : Request.QueryString["SelectPayment7"].ToString();
                var _SelectPayment8 = Request.QueryString["SelectPayment8"] == "" ? null : Request.QueryString["SelectPayment8"].ToString();
                var _SelectPayment9 = Request.QueryString["SelectPayment9"] == "" ? null : Request.QueryString["SelectPayment9"].ToString();
                var _SelectPayment10 = Request.QueryString["SelectPayment10"] == "" ? null : Request.QueryString["SelectPayment10"].ToString();

                var rolesId = role.Split(new char[] { ',' });
                ViewBag.role = rolesId;

                foreach (var item in rolesId)
                {
                    switch (Convert.ToInt32(item))
                    {
                        case 1:

                            query = query.Where(x => x.CustomizeTag1 != null && x.CustomizeTag1.Contains(_SelectPayment1));


                            ViewBag.SelectPayment1 = _SelectPayment1; break;
                        case 2:
                            query = query.Where(x => x.CustomizeTag2 != null && x.CustomizeTag2.Contains(_SelectPayment2));
                            ViewBag.SelectPayment2 = _SelectPayment2; break;
                        case 3:
                            query = query.Where(x => x.CustomizeTag3 != null && x.CustomizeTag3.Contains(_SelectPayment3));
                            ViewBag.SelectPayment3 = _SelectPayment3; break;
                        case 4:
                            query = query.Where(x => x.CustomizeTag4 != null && x.CustomizeTag4.Contains(_SelectPayment4));
                            ViewBag.SelectPayment4 = _SelectPayment4; break;
                        case 5:
                            query = query.Where(x => x.CustomizeTag5 != null && x.CustomizeTag5.Contains(_SelectPayment5));
                            ViewBag.SelectPayment5 = _SelectPayment5; break;
                        case 6:
                            query = query.Where(x => x.CustomizeTag6 != null && x.CustomizeTag6.Contains(_SelectPayment6));
                            ViewBag.SelectPayment6 = _SelectPayment6; break;
                        case 7:
                            query.Where(x => x.CustomizeTag7 != null && x.CustomizeTag7.Contains(_SelectPayment7));
                            ViewBag.SelectPayment7 = _SelectPayment7; break;
                        case 8:
                            query = query.Where(x => x.CustomizeTag8 != null && x.CustomizeTag8.Contains(_SelectPayment8));
                            ViewBag.SelectPayment8 = _SelectPayment8; break;
                        case 9:
                            query = query.Where(x => x.CustomizeTag9 != null && x.CustomizeTag9.Contains(_SelectPayment9));
                            ViewBag.SelectPayment9 = _SelectPayment9; break;
                        case 10:
                            query = query.Where(x => x.CustomizeTag10 != null && x.CustomizeTag10.Contains(_SelectPayment10));
                            ViewBag.SelectPayment10 = _SelectPayment10; break;
                    }
                }
            }
            else
            {
                ViewBag.role = "".Split(new char[] { ',' });
                ViewBag.SelectPayment1 = null;
                ViewBag.SelectPayment2 = null;
                ViewBag.SelectPayment3 = null;
                ViewBag.SelectPayment4 = null;
                ViewBag.SelectPayment5 = null;
                ViewBag.SelectPayment6 = null;
                ViewBag.SelectPayment7 = null;
                ViewBag.SelectPayment8 = null;
                ViewBag.SelectPayment9 = null;
                ViewBag.SelectPayment10 = null;
            }





            //var result = query.ToList();
            var result = query.Where(x => x.IsDeleted == false).OrderBy(x => x.Id).ToPagedList(page, pageSize);
            return View(result);
        }
        /// <summary>
        /// 会员详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult Detail(int Id)
        {

            var result = DB.T_Customer.Where(x => x.Id == Id).FirstOrDefault();



            return View(result);
        }
        /// <summary>
        /// 修改会员
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult Edit(int Id)
        {

            var result = DB.T_Customer.Where(x => x.Id == Id).FirstOrDefault();
            return View(result);
        }
        /// <summary>
        /// 新增会员
        /// </summary>
        /// <returns></returns>
        public ActionResult CustomerCreate()
        {

            return View(new T_Customer());
        }
        /// <summary>
        /// 新增会员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken, ValidateInput(false), HttpPost]
        public ActionResult CustomerCreate(int? id)
        {
            try
            {

                var a = new T_Customer();
                a.CustomerRealName = Request.Form["CustomerRealName"] == null ? a.CustomerRealName : Request.Form["CustomerRealName"].ToString();
                a.CustomerPhoneNum = Request.Form["CustomerPhoneNum"] == null ? a.CustomerPhoneNum : Request.Form["CustomerPhoneNum"].ToString();
                a.CustomerId = Request.Form["CustomerId"] == null ? a.CustomerId : Request.Form["CustomerId"].ToString();
                a.CustomerCertificateType = Request.Form["CustomerCertificateType"] == null ? a.CustomerCertificateType : Request.Form["CustomerCertificateType"].ToString();
                a.CustomerCertificateNum = Request.Form["CustomerCertificateNum"] == null ? a.CustomerCertificateNum : Request.Form["CustomerCertificateNum"].ToString();
                a.CustomerSex = Request.Form["CustomerSex"] == null ? a.CustomerSex : Request.Form["CustomerSex"].ToString();
                a.CustomerAddressProvince = Request.Form["province"] == null ? a.CustomerAddressProvince : Request.Form["province"].ToString();
                a.CustomerAddressCity = Request.Form["city"] == null ? a.CustomerAddressCity : Request.Form["city"].ToString();
                a.CustomerAddressDistrict = Request.Form["town"] == null ? a.CustomerAddressDistrict : Request.Form["town"].ToString();
                a.CustomerWechatNum = Request.Form["CustomerWechatNum"] == null ? a.CustomerWechatNum : Request.Form["CustomerWechatNum"].ToString();
                a.CustomerQQNum = Request.Form["CustomerQQNum"] == null ? a.CustomerQQNum : Request.Form["CustomerQQNum"].ToString();
                a.CustomerEmailNum = Request.Form["CustomerEmailNum"] == null ? a.CustomerEmailNum : Request.Form["CustomerEmailNum"].ToString();
                a.AccountEffect = Request.Form["AccountEffect"] == null ? a.AccountEffect : Convert.ToInt32(Request.Form["AccountEffect"].ToString());
                a.CustomerCurrentIntegral = Request.Form["CustomerCurrentIntegral"] == null ? a.CustomerCurrentIntegral : Convert.ToInt32(Request.Form["CustomerCurrentIntegral"].ToString());
                a.CustomerBirthday = Request.Form["CustomerBirthday"] == null ? a.CustomerBirthday : Convert.ToDateTime(Request.Form["CustomerBirthday"].ToString());
                a.CustomerTakeTime = Request.Form["CustomerTakeTime"] == null ? a.CustomerTakeTime : Convert.ToDateTime(Request.Form["CustomerTakeTime"].ToString());
                a.CustomerTag = Request.Form["CustomerTag"] == null ? a.CustomerTag : Request.Form["CustomerTag"].ToString();

                a.CustomizeTag1 = Request.Form["CustomizeTag1"] == "" ? null : Request.Form["CustomizeTag1"].ToString();
                a.CustomizeTag2 = Request.Form["CustomizeTag2"] == "" ? null : Request.Form["CustomizeTag2"].ToString();
                a.CustomizeTag3 = Request.Form["CustomizeTag3"] == "" ? null : Request.Form["CustomizeTag3"].ToString();
                a.CustomizeTag4 = Request.Form["CustomizeTag4"] == "" ? null : Request.Form["CustomizeTag4"].ToString();
                a.CustomizeTag5 = Request.Form["CustomizeTag5"] == "" ? null : Request.Form["CustomizeTag5"].ToString();
                a.CustomizeTag6 = Request.Form["CustomizeTag6"] == "" ? null : Request.Form["CustomizeTag6"].ToString();
                a.CustomizeTag7 = Request.Form["CustomizeTag7"] == "" ? null : Request.Form["CustomizeTag7"].ToString();
                a.CustomizeTag8 = Request.Form["CustomizeTag8"] == "" ? null : Request.Form["CustomizeTag8"].ToString();
                a.CustomizeTag9 = Request.Form["CustomizeTag9"] == "" ? null : Request.Form["CustomizeTag9"].ToString();
                a.CustomizeTag10 = Request.Form["CustomizeTag10"] == "" ? null : Request.Form["CustomizeTag10"].ToString();
                a.IsDeleted = false;
                a.CreatedOn = DateTime.Now;
                var u = JsonConvert.DeserializeObject<AuthAdmin>(User.Identity.Name);
                a.CreatedBy = u.RealName;

                DB.T_Customer.Add(a);

                DB.SaveChanges();
            }
            catch (Exception ex)
            {


            }





            return RedirectToAction("Index");
        }
        /// <summary>
        /// 修改会员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken, ValidateInput(false), HttpPost]
        public ActionResult Edit(int? id)
        {
            try
            {


                var a = DB.T_Customer.Where(x => x.Id == (int)id).FirstOrDefault();
                a.CustomerRealName = Request.Form["CustomerRealName"] == "" ? null : Request.Form["CustomerRealName"].ToString();
                a.CustomerPhoneNum = Request.Form["CustomerPhoneNum"] == "" ? null : Request.Form["CustomerPhoneNum"].ToString();
                a.CustomerId = Request.Form["CustomerId"] == "" ? null : Request.Form["CustomerId"].ToString();
                a.CustomerCertificateType = Request.Form["CustomerCertificateType"] == "" ? a.CustomerCertificateType : Request.Form["CustomerCertificateType"].ToString();
                a.CustomerCertificateNum = Request.Form["CustomerCertificateNum"] == "" ? null : Request.Form["CustomerCertificateNum"].ToString();
                a.CustomerSex = Request.Form["CustomerSex"] == "" ? null : Request.Form["CustomerSex"].ToString();
                a.CustomerAddressProvince = Request.Form["province"] == "" ? a.CustomerAddressProvince : Request.Form["province"].ToString();
                a.CustomerAddressCity = Request.Form["city"] == "" ? a.CustomerAddressCity : Request.Form["city"].ToString();
                a.CustomerAddressDistrict = Request.Form["town"] == "" ? a.CustomerAddressDistrict : Request.Form["town"].ToString();
                a.CustomerWechatNum = Request.Form["CustomerWechatNum"] == "" ? null : Request.Form["CustomerWechatNum"].ToString();
                a.CustomerQQNum = Request.Form["CustomerQQNum"] == "" ? null : Request.Form["CustomerQQNum"].ToString();
                a.CustomerEmailNum = Request.Form["CustomerEmailNum"] == "" ? null : Request.Form["CustomerEmailNum"].ToString();
                a.AccountEffect = Request.Form["AccountEffect"] == "" ? a.AccountEffect : Convert.ToInt32(Request.Form["AccountEffect"].ToString());
                a.CustomerCurrentIntegral = Request.Form["CustomerCurrentIntegral"] == "" ? a.CustomerCurrentIntegral : Convert.ToInt32(Request.Form["CustomerCurrentIntegral"].ToString());
                a.CustomerBirthday = Request.Form["CustomerBirthday"] == "" ? a.CustomerBirthday : Convert.ToDateTime(Request.Form["CustomerBirthday"].ToString());
                a.CustomerTakeTime = Request.Form["CustomerTakeTime"] == "" ? a.CustomerTakeTime : Convert.ToDateTime(Request.Form["CustomerTakeTime"].ToString());
                a.CustomerTag = Request.Form["CustomerTag"] == null ? null : Request.Form["CustomerTag"].ToString();

                a.CustomizeTag1 = Request.Form["CustomizeTag1"] == "" ? null : Request.Form["CustomizeTag1"].ToString();
                a.CustomizeTag2 = Request.Form["CustomizeTag2"] == "" ? null : Request.Form["CustomizeTag2"].ToString();
                a.CustomizeTag3 = Request.Form["CustomizeTag3"] == "" ? null : Request.Form["CustomizeTag3"].ToString();
                a.CustomizeTag4 = Request.Form["CustomizeTag4"] == "" ? null : Request.Form["CustomizeTag4"].ToString();
                a.CustomizeTag5 = Request.Form["CustomizeTag5"] == "" ? null : Request.Form["CustomizeTag5"].ToString();
                a.CustomizeTag6 = Request.Form["CustomizeTag6"] == "" ? null : Request.Form["CustomizeTag6"].ToString();
                a.CustomizeTag7 = Request.Form["CustomizeTag7"] == "" ? null : Request.Form["CustomizeTag7"].ToString();
                a.CustomizeTag8 = Request.Form["CustomizeTag8"] == "" ? null : Request.Form["CustomizeTag8"].ToString();
                a.CustomizeTag9 = Request.Form["CustomizeTag9"] == "" ? null : Request.Form["CustomizeTag9"].ToString();
                a.CustomizeTag10 = Request.Form["CustomizeTag10"] == "" ? null : Request.Form["CustomizeTag10"].ToString();
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
        #region 批量导入会员        
        public ActionResult ImportCustomes()
        {
            return View();
        }
        [ValidateInput(false), HttpPost]
        public ActionResult ReadFile(HttpPostedFileBase file)
        {
            List<string> t = new List<string>();
            if (file != null)
            {
                var filename = (new FileReadWrite()).SaveFile("T_Customer", file, Request);
                var dt = Import(filename, 0);
                var d1 = dt.CreateDataReader();
                var a = 0;
                while (d1.Read())
                {
                    if (a == 0)
                    {
                        for (int i = 0; i < d1.FieldCount; i++)
                        {

                            t.Add(d1[dt.Columns[i].ColumnName].ToString());
                        }
                        a = 1;
                    }

                }



            }


            return Json(t);




        }
        [ValidateInput(false), HttpPost]
        public ActionResult ReadFiles(HttpPostedFileBase file, string files, string Rfiles)
        {
            //初始化赋值集合
            List<T_Customer> s = new List<T_Customer>();
            //初始化返回值
            List<Data2> d = new List<Data2>();
            var TC = DB.T_Customer.ToList();
            var SucessCount = 0;
            var FailCount = 0;
            T_OthersGiftsRecord pd = new T_OthersGiftsRecord();
            var u = JsonConvert.DeserializeObject<AuthAdmin>(User.Identity.Name);
            var tk = Task.Run(() =>
              {
                  //创建EF对象
                  PeContext db = new PeContext();
                  if (file != null && files != null)
                  {
                      #region 保存并读取导入文件
                      //序列化
                      var ta = JsonConvert.DeserializeObject<List<Data1>>(files);
                      //var filename = Path.Combine(Request.MapPath("~/App_Data"), file.FileName);
                      //file.SaveAs(filename);
                      var filename = (new FileReadWrite()).SaveFile("T_Customer", file, Request);
                      var dt = Import(filename, 0);
                      var d1 = dt.CreateDataReader();
                      #endregion
                      #region 操作数据
                      var a1 = (new T_Customer()).GetType();
                      var ct = 0;
                      while (d1.Read())
                      {
                          Data2 d2 = new Data2();
                          d2.row = ct;
                          d2.col = new List<Data3>();
                          //创建赋值对象
                          var a2 = new T_Customer();
                          a2.IsDeleted = false;
                          a2.CreatedBy = u.RealName;
                          a2.CreatedOn = DateTime.Now;
                          //遍历被选中的列
                          for (int j = 0; j < ta.Count(); j++)
                          {
                              //初始化当前列数据
                              Data3 d3 = new Data3();
                              //正则过滤
                              try
                              {
                                  var a = Re(ta[j].name, d1[dt.Columns[ta[j].index].ColumnName].ToString());
                                  if (a == "")
                                  {
                                      d3.row = j;
                                      d3.message = @"第" + (ct + 2) + "行第" + (j + 1) + "列 '" + getStr(ta[j].name) + "' 数据为: " + d1[dt.Columns[ta[j].index].ColumnName].ToString() + "有误！！".Replace("<", "").Replace(">", "");
                                      d2.col.Add(d3);
                                      a2 = null;
                                      continue;
                                  }
                                  else
                                  {
                                      if (a2 != null)
                                      {
                                          //标记重复行
                                          var bj = 0;
                                          //过滤数据库重复
                                          //TC = db.T_Customer.ToList();
                                          //选择的
                                          if (ta[j].name == Rfiles)
                                          {


                                              //遍历集合
                                              foreach (var item in TC)
                                              {
                                                  //获取当前对象的指定属性的值
                                                  var v = DateTime.Now.ToLongDateString();
                                                  if (a1.GetProperty(Rfiles).GetValue(item) != null)
                                                  {
                                                      v = a1.GetProperty(Rfiles).GetValue(item).ToString();
                                                  }

                                                  //日期
                                                  if (Rfiles == "CustomerBirthday" || Rfiles == "CustomerTakeTime")
                                                  {

                                                      var date1 = Convert.ToDateTime(a).ToLongDateString();
                                                      var date2 = Convert.ToDateTime(v).ToLongDateString();

                                                      if (date1 == date2)
                                                      {
                                                          d3.row = j;
                                                          d3.message = @"第" + (ct + 2) + "行第" + (j + 1) + "列 '" + getStr(ta[j].name) + "' 数据为: " + d1[dt.Columns[ta[j].index].ColumnName].ToString() + "已存在！！".Replace("<", "").Replace(">", "");
                                                          d2.col.Add(d3);
                                                          bj++;
                                                          a2 = null;
                                                          break;
                                                      }
                                                  }
                                                  else if (Rfiles == "CustomerCurrentIntegral")
                                                  {

                                                      if (Convert.ToInt32(a) == Convert.ToInt32(v))
                                                      {
                                                          d3.row = j;
                                                          d3.message = @"第" + (ct + 2) + "行第" + (j + 1) + "列 '" + getStr(ta[j].name) + "' 数据为: " + d1[dt.Columns[ta[j].index].ColumnName].ToString() + "已存在！！".Replace("<", "").Replace(">", "");
                                                          d2.col.Add(d3);
                                                          bj++;
                                                          a2 = null;

                                                          break;
                                                      }
                                                  }
                                                  else
                                                  {
                                                      if (a == v)
                                                      {
                                                          d3.row = j;
                                                          d3.message = @"第" + (ct + 2) + "行第" + (j + 1) + "列 '" + getStr(ta[j].name) + "' 数据为: " + d1[dt.Columns[ta[j].index].ColumnName].ToString() + "已存在！！".Replace("<", "").Replace(">", "");
                                                          d2.col.Add(d3);
                                                          bj++;
                                                          a2 = null;

                                                          break;
                                                      }
                                                  }

                                              }

                                          }

                                          if (bj == 0)
                                          {  //日期
                                              if (ta[j].name == "CustomerBirthday" || ta[j].name == "CustomerTakeTime")
                                              {
                                                  a1.GetProperty(ta[j].name).SetValue(a2, Convert.ToDateTime(a));
                                              }
                                              else if (ta[j].name == "CustomerCurrentIntegral")
                                              {

                                                  a1.GetProperty(ta[j].name).SetValue(a2, Convert.ToInt32(a));

                                              }
                                              else
                                              {
                                                  a1.GetProperty(ta[j].name).SetValue(a2, a);
                                              }
                                          }



                                      }
                                  }

                              }
                              catch (Exception e)
                              {
                                  d3.row = j;
                                  d3.message = @"第" + (ct + 2) + "行第" + (j + 1) + "列 '" + getStr(ta[j].name) + "' 数据为" + d1[dt.Columns[ta[j].index].ColumnName].ToString() + e + "有误！！".Replace("<", "").Replace(">", "");
                                  d2.col.Add(d3);
                                  a2 = null;
                                  continue;
                              }
                          }
                          //填充对象不为空
                          if (a2 != null)
                          {
                              s.Add(a2);
                          }
                          ct++;

                          d.Add(d2);
                      }
                      #endregion
                      #region 添加入数据库
                      var t1 = 0;
                      foreach (var item in s.Where(x => x != null).ToList())
                      {
                          try
                          {
                              db.T_Customer.Add(item);

                              //成功
                              SucessCount++;
                              t1++;
                          }
                          catch (Exception e)
                          {
                              //失败
                              FailCount++;
                          }
                      }
                      //记录操作
                      pd = new T_OthersGiftsRecord()
                      {
                          Count = t1,
                          Time = DateTime.Now,
                          FileName = file.FileName + " | 共计:" + s.Count() + "行" + "," + d1.FieldCount + "列",
                          Name = "T_Customer"

                      };

                      db.SaveChanges();
                  }
                  #endregion
              });
            //等待异步执行完成
            tk.Wait();
            //返回结果
            if (tk.Status == TaskStatus.RanToCompletion)
            {
                dynamic p2 = new
                {
                    a1 = d,//返回信息
                    a2 = s,//添加几何
                    a3 = SucessCount,//成功行
                    a4 = FailCount,  //失败行
                };
                var Messsage = "";
                for (var i = 0; i < d.Count(); i++)
                {
                    var st = "";
                    for (var j = 0; j < d[i].col.Count(); j++)
                    {


                        var p = d[i].col[j].message;

                        st = st + p + "\n";


                    }

                    if (i == 0)
                    {
                        //添加结果
                        Messsage = Messsage + "此次共计添加" + SucessCount + "行添加成功！该文件共" + d.Count() + "行\n";

                    }
                    if (st == "")
                    {
                        //如果添加成功
                        Messsage = Messsage + "第" + (i + 2) + "行，此行添加成功!\n";
                    }
                    else
                    {
                        //如果添加失败

                        Messsage = Messsage + "第" + (i + 2) + "行，此行添加失败！，原因如下：\n" + st + "\n";

                    }
                    st = "";


                }


                var m = (new FileReadWrite()).WriteFile(file.FileName, "T_Customer", Messsage, Request);
                dynamic p1 = new
                {
                    a1 = d,//返回信息
                    a2 = s,//添加几何
                    a3 = SucessCount,//成功行
                    a4 = FailCount,  //失败行
                    a5 = m//文件路径
                };


                //返回数据结构
                pd.FileName = pd.FileName + "-|<a href = '/CustomerManage/DownloadFile?path=" + m + "' > 导入结果下载</a>";
                DB.T_OthersGiftsRecord.Add(pd);
                DB.SaveChanges();

                //序列化数据
                var js = (new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue }.Serialize(p1));
                #region 生成文件保存
                //生成文件


                //存储文件




                #endregion
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
                    //会员入会时间
                    case "CustomerCurrentIntegral":
                        try
                        {
                            var q = Convert.ToInt32(a2);
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
        /// 文件下载
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DownloadFile(string path)
        {

            if (System.IO.File.Exists(Server.MapPath(path)))
            {
                return File(Server.MapPath(path), "application/octet-stream", DateTime.Now.ToLongDateString() + "_" + DateTime.Now.ToLongTimeString() + ".txt");
            }
            else
            {
                return View("Index");
            }


        }
        #endregion

        #region 会员标签管理
        /// <summary>
        /// 标签列表
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult CustomerTagIndex(int page = 1)
        {
            IPagedList<T_CustomerTag> result = NewMethod1(page);
            return View(result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        private IPagedList<T_CustomerTag> NewMethod1(int page)
        {
            int pageSize = 15;
            var query = DB.T_CustomerTag.AsNoTracking().AsQueryable();
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
            var result = query.Where(x => x.IsDeleted == false).OrderBy(x => x.id).ToPagedList(page, pageSize);
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        private void NewMethod()
        {
            List<T_Customer> a = new List<T_Customer>();
            for (int i = 10; i < 100; i++)
            {
                a.Add(

                    new T_Customer()
                    {
                        CustomerRealName = "张" + i + "山",
                        CustomerId = "CN" + i,
                        CustomerCertificateNum = "ID" + i,
                        CustomerSex = (i % 2) == 0 ? "男" : "女"





                    }
                    );
            }

            DB.T_Customer.AddRange(a);

            DB.SaveChanges();
        }
        /// <summary>
        /// 移除标签
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult TagDelete(int id)
        {
            var item = DB.T_CustomerTag.Where(x => x.id == id).FirstOrDefault();
            if (item != null)
            {
                var u = JsonConvert.DeserializeObject<AuthAdmin>(User.Identity.Name);
                item.IsDeleted = true;
                item.UpdatedBy = u.RealName;
                item.UpdatedOn = DateTime.Now;

                DB.SaveChanges();
            }

            return RedirectToAction("CustomerTagIndex");
        }
        /// <summary>
        /// 新增会员标签
        /// </summary>
        /// <returns></returns>
        public ActionResult CustomerTagCreate()
        {
            return View();
        }
        /// <summary>
        /// 新增会员标签
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken, ValidateInput(false), HttpPost]
        public ActionResult CustomerTagCreate(string Name)
        {
            var u = JsonConvert.DeserializeObject<AuthAdmin>(User.Identity.Name);
            DB.T_CustomerTag.Add(new T_CustomerTag()
            {
                TagName = Name,
                CreatedOn = DateTime.Now,
                CreatedBy = u.RealName,
                IsDeleted = false

            });
            DB.SaveChanges();
            return RedirectToAction("CustomerTagIndex");

        }
        public ActionResult TagEdit(int id)
        {
            var a = DB.T_CustomerTag.Where(x => x.id == id).FirstOrDefault();
            return View(a);
        }
        [ValidateAntiForgeryToken, ValidateInput(false), HttpPost]
        public ActionResult TagEdit(string TagName, int? id)
        {
            var u = JsonConvert.DeserializeObject<AuthAdmin>(User.Identity.Name);
            var a = DB.T_CustomerTag.Where(x => x.id == id).FirstOrDefault();
            if (a != null)
            {
                a.TagName = TagName;
                a.UpdatedOn = DateTime.Now;
                a.UpdatedBy = u.RealName;
                DB.SaveChanges();
            }
            return RedirectToAction("CustomerTagIndex");

        }



        #endregion

        #region 会员积分管理
        /// <summary>
        /// 会员积分记录
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult T_CustomerIntegralRecords(int? id, int page = 1)
        {
            if (id != null)
            {
                ViewBag.id = id;
            }
            else
            {
                var a = Request.QueryString["id"];
            }

            int pageSize = 15;
            var t = DB.T_Customer.Where(x => x.Id == id).FirstOrDefault();
            ViewBag.a = t;
            var query = DB.T_CustomerIntegralRecord.AsNoTracking().AsQueryable();
            query = query.Where(x => x.T_CustomerId == t.Id);
            var result = query.OrderBy(x => x.Id).ToPagedList(page, pageSize);

            return View(result);
        }


        /// <summary>
        /// 会员积分记录
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [ValidateInput(false), HttpPost]

        public ActionResult T_CustomerIntegralRecords(int page = 1)
        {
            int? id = 0;
            try
            {
                //获取数据
                id = Convert.ToInt32(Request.Form["id"]);
                var integralExchangeValue = Convert.ToInt32(Request.Form["integralExchangeValue"]);
                var ExchangeReason = Request.Form["ExchangeReason"].ToString();


                var u = JsonConvert.DeserializeObject<AuthAdmin>(User.Identity.Name);
                var t1 = DB.T_Customer.Where(x => x.Id == id).FirstOrDefault();
                var ct = t1.CustomerCurrentIntegral;
                t1.CustomerCurrentIntegral = t1.CustomerCurrentIntegral + integralExchangeValue;
                if (t1.CustomerCurrentIntegral < 0)
                {
                    t1.CustomerCurrentIntegral = ct;
                }
                //记录
                DB.T_CustomerIntegralRecord.Add(new T_CustomerIntegralRecord()
                {

                    CreatedBy = u.RealName,
                    CreatedOn = DateTime.Now,
                    IsDeleted = false,
                    T_Customer = t1,
                    AfterExchangeintegralValue = t1.CustomerCurrentIntegral,
                    integralExchangeValue = integralExchangeValue + "",
                    ExchangeReason = ExchangeReason,
                    ExchangeTime = DateTime.Now
                });


                DB.SaveChanges();

            }

            catch (Exception ex)
            {

                //throw;
            }


            return RedirectToAction("T_CustomerIntegralRecords");
        }
        #endregion

        #region 会员礼品管理
        /// <summary>
        /// 会员礼品管理
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult T_GiftsChildRecords(int? id, int page = 1)
        {
            if (id != null)
            {
                ViewBag.id = id;
            }
            else
            {
                id = Convert.ToInt32(Request.QueryString["id"]);
            }
            int pageSize = 15;
            var t = DB.T_Customer.Where(x => x.Id == id).FirstOrDefault();
            ViewBag.a = t;
            var query = DB.T_GiftChild.AsNoTracking().AsQueryable();
            query = query.Where(x => x.T_CustomerId == t.Id);
            var result = query.OrderBy(x => x.id).ToPagedList(page, pageSize);

            return View(result);
        }


        /// <summary>
        /// 会员礼品管理
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [ValidateInput(false), HttpPost]

        public ActionResult T_GiftsChildRecords(int page = 1)
        {
            int? id = 0;
            try
            {
                //获取数据
                id = Convert.ToInt32(Request.Form["id"]);
                var GetReason = Request.Form["GetReason"];
                //var  s0= Request.Form["s2"].ToString();
                //礼品id
                var s2 = Convert.ToInt32(Request.Form["gid"]);
                //礼品数量
                var s3 = Convert.ToInt32(Request.Form["s3"]);




                var u = JsonConvert.DeserializeObject<AuthAdmin>(User.Identity.Name);

                //获取用户
                var t1 = DB.T_Customer.Where(x => x.Id == id).FirstOrDefault();
                //获取礼品
                var t2 = DB.T_Gifts.Where(x => x.id == s2).FirstOrDefault();


                #region 数据操作

                //礼品表  


                //虚拟礼品                   第三方Coupon   
                if (t2.GiftType == 0 && t2.IsCoupon == 1)
                {
                    //从卷池中取出需要赠送的卷
                    var j = t2.T_GiftsChild.Where(x => x.T_CustomerId == null).Take(s3).ToList();
                    //修改每张卷
                    foreach (var item in j)
                    {

                        //获赠人
                        item.T_Customer = t1;
                        //赠送
                        item.ExchangeTime = DateTime.Now;
                        item.IsDeleted = false;
                        item.GetReason = GetReason;
                        //修改人
                        item.UpdatedBy = u.RealName;
                        //修改时间
                        item.UpdatedOn = DateTime.Now;



                    }

                }
                else if (t2.GiftType == 0 && t2.IsCoupon == 0)
                {
                    //自主Coupon
                    //从卷池中取出需要赠送的卷 获赠时间在 自主卷生效期内 未被删除
                    var j = t2.T_GiftsChild.Where(x => x.IsDeleted == false && x.T_CustomerId == null && x.EffectiveTime < DateTime.Now && x.FailureTime > DateTime.Now).Take(s3).ToList();
                    //修改每张卷
                    foreach (var item in j)
                    {

                        //获赠人
                        item.T_Customer = t1;
                        //赠送
                        item.ExchangeTime = DateTime.Now;
                        item.IsDeleted = false;
                        item.GetReason = GetReason;
                        item.ExchangeType = 2;
                        //修改人
                        item.UpdatedBy = u.RealName;
                        //修改时间
                        item.UpdatedOn = DateTime.Now;



                    }


                }
                else if (t2.GiftType == 1)
                {
                    //实体礼品
                    DB.T_GiftChild.Add(new T_GiftsChild()
                    {
                        T_Gifts = t2,
                        //获赠人
                        T_Customer = t1,
                        //赠送
                        ExchangeTime = DateTime.Now,
                        ExchangeType = 2,
                        GetReason = GetReason,
                        //修改人
                        CreatedBy = u.RealName,
                        //修改时间
                        CreatedOn = DateTime.Now,
                        IsDeleted = false
                    });

                }
                t2.GiftInventory = t2.GiftInventory - s3;



                #endregion


                DB.SaveChanges();

            }

            catch (Exception ex)
            {


            }

            return RedirectToAction("T_GiftsChildRecords");



        }
        #endregion
        #endregion

    }
    #region 辅助类
    /// <summary>
    /// 序列化实体
    /// </summary>
    public class Data1
    {
        public int index { get; set; }
        public String name { get; set; }
    }
    /// <summary>
    ///数据返回实体
    /// </summary>
    public class Data2
    {
        /// <summary>
        /// 行
        /// </summary>
        public int row { get; set; }
        /// <summary>
        /// 列<列名 导入返回结果>
        /// </summary>
        public List<Data3> col { get; set; }
    }
    /// <summary>
    ///数据返回实体
    /// </summary>
    public class Data3
    {
        /// <summary>
        /// 列名
        /// </summary>
        public int row { get; set; }
        /// <summary>
        /// 列返回结果
        /// </summary>
        public string message { get; set; }
    }
    /// <summary>
    /// 验证数据
    /// </summary>
    public static class Regx
    {
        /// <summary>
        /// 验证日期
        /// </summary>
        public static string s1 = @"^\d{4}-\d{1,2}-\d{1,2}";
        /// <summary>
        /// 验证固定电话
        /// </summary>
        public static string s2 = @"\d{3}-\d{8}|\d{4}-\d{7}";
        /// <summary>
        /// 验证手机
        /// </summary>
        public static string s3 = @"^(13[0-9]|14[5|7]|15[0|1|2|3|5|6|7|8|9]|18[0|1|2|3|5|6|7|8|9])\d{8}$";
        /// <summary>
        /// 验证邮编
        /// </summary>
        public static string s4 = @"[1-9]\d{5}(?!\d) ";
        /// <summary>
        /// 验证Email地址
        /// </summary>
        public static string s5 = @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
        /// <summary>
        /// 验证身份证号
        /// </summary>
        public static string s6 = @"^\d{15}|\d{18}$";
        /// <summary>
        /// 验证QQ号
        /// </summary>
        public static string s7 = @"[1-9][0-9]{4,}";
        /// <summary>
        /// 验证微信号
        /// </summary>
        public static string s8 = @"^[a-zA-Z]{1}[-_a-zA-Z0-9]{5,19}$";

        /// <summary>
        /// 去除html标记，装换成一般文本
        /// </summary>
        /// <param name="htmlStr"></param>
        /// <returns></returns>
        public static string ReHtml(string htmlStr)
        {

            if (String.IsNullOrEmpty(htmlStr))

            {

                return "";

            }

            string regEx_style = "<style[^>]*?>[\\s\\S]*?<\\/style>"; //定义style的正则表达式 

            string regEx_script = "<script[^>]*?>[\\s\\S]*?<\\/script>"; //定义script的正则表达式   

            string regEx_html = "<[^>]+>"; //定义HTML标签的正则表达式   

            htmlStr = Regex.Replace(htmlStr, regEx_style, "");//删除css

            htmlStr = Regex.Replace(htmlStr, regEx_script, "");//删除js

            htmlStr = Regex.Replace(htmlStr, regEx_html, "");//删除html标记

            htmlStr = Regex.Replace(htmlStr, "\\s*|\t|\r|\n", "");//去除tab、空格、空行

            htmlStr = htmlStr.Replace(" ", "");

            htmlStr = htmlStr.Replace(@" "", "");   //去除异常的引号 
    

        htmlStr = htmlStr.Replace(@"" ", "");

            return htmlStr.Trim();

        }
    }


    public class FileReadWrite
    {

        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="FName">文件名称</param>
        /// <param name="TName">表明</param>
        /// <param name="Message">内容</param>
        /// <param name="Request"></param>
        /// <returns></returns>
        public string WriteFile(string FName, string TName, string Message, HttpRequestBase Request)
        {
            var a = "";
            try
            {
                //路径名称 时间+文件名+表名
                var p = FName.Replace('.', '_') + "_Input_" + TName + "_" + DateTime.Now.ToLongDateString() + "_" + DateTime.Now.ToLongTimeString();
                p = p.Replace(':', '_').ToString();
                //判断该文件夹是否存在
                var mp = "~/App_Data/" + TName + "/" + DateTime.Now.ToLongDateString() + "/";
                var p1 = Request.MapPath(mp);
                if (false == System.IO.Directory.Exists(p1))
                {
                    //不存在就创建
                    System.IO.Directory.CreateDirectory(p1);
                }

                File.WriteAllText(p1 + "/" + p + ".text", Message);
                a = mp + "/" + p + ".text";

            }
            catch (Exception ex)
            {
                a = "失败,原因：" + ex;
            }

            return a;

        }

        /// <summary>
        /// 文件保存
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="TName">表名</param> 
        /// <param name="Request"></param>
        /// <returns></returns>
        public string SaveFile(string TName, HttpPostedFileBase file, HttpRequestBase Request)
        {
            var a = "";
            try
            {

                //判断该文件夹是否存在
                var mp = "~/App_Data/" + TName + "/" + DateTime.Now.ToLongDateString() + "/";
                var p1 = Request.MapPath(mp);
                if (false == System.IO.Directory.Exists(p1))
                {
                    //不存在就创建
                    System.IO.Directory.CreateDirectory(p1);
                }
                var filename = Path.Combine(p1, file.FileName);
                file.SaveAs(filename);
                a = filename;

            }
            catch (Exception ex)
            {
                a = "失败,原因：" + ex;
            }

            return a;

        }

    }
    #endregion
}