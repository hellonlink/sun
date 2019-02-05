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
    public class CustomerManageController : BaseController
    {
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
            var result = query.OrderBy(x => x.Id).ToPagedList(page, pageSize);
            return View(result);
        }
        public ActionResult Detail(int Id)
        {

            var result = DB.T_Customer.Where(x => x.Id == Id).FirstOrDefault();



            return View(result);
        }
        public ActionResult Edit(int Id)
        {

            var result = DB.T_Customer.Where(x => x.Id == Id).FirstOrDefault();
            return View(result);
        }
        public ActionResult CustomerCreate()
        {

            return View(new T_Customer());
        }

        [ValidateAntiForgeryToken, ValidateInput(false), HttpPost]

        public ActionResult CustomerCreate(int? id)
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




            return RedirectToAction("Index");
        }



        [ValidateAntiForgeryToken, ValidateInput(false), HttpPost]

        public ActionResult Edit(int? id)
        {

            var a = DB.T_Customer.Where(x => x.Id == (int)id).FirstOrDefault();
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
            a.UpdatedOn = DateTime.Now;
            var u = JsonConvert.DeserializeObject<AuthAdmin>(User.Identity.Name);
            a.UpdatedBy = u.RealName;
            if (a != null)
            {
                DB.SaveChanges();


            }

            return RedirectToAction("Index");


        }
        public ActionResult CustomerTagIndex(int page = 1)
        {
            IPagedList<T_CustomerTag> result = NewMethod1(page);
            return View(result);
        }

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
            var result = query.OrderBy(x => x.id).ToPagedList(page, pageSize);
            return result;
        }

        public ActionResult TagDelete(int id)
        {
            var item = DB.T_CustomerTag.Where(x => x.id == id).FirstOrDefault();
            if (item != null)
            {
                DB.T_CustomerTag.Remove(item);

                DB.SaveChanges();
            }

            return RedirectToAction("CustomerTagIndex");
        }

        public ActionResult CustomerTagCreate()
        {
            return View();
        }
        [ValidateAntiForgeryToken, ValidateInput(false), HttpPost]
        public ActionResult CustomerTagCreate(string Name)
        {
            DB.T_CustomerTag.Add(new T_CustomerTag()
            {
                TagName = Name,

                UpdatedOn = DateTime.Now
            });
            DB.SaveChanges();
            return RedirectToAction("CustomerTagIndex");

        }

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
    }
}