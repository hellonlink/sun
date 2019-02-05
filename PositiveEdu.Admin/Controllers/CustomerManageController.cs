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

                    if (_CustomerSex == "女")
                    {
                        ViewBag.payment = "女";
                    }
                    else
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
                var rolesId = role.Split(new char[] { ',' });
                ViewBag.role = rolesId;

                foreach (var item in rolesId)
                {
                    switch (Convert.ToInt32(item))
                    {
                        case 1:
                            query = query.Where(x => x.CustomizeTag1 != null);
                            break;
                        case 2:
                            query = query.Where(x => x.CustomizeTag2 != null);
                            break;
                        case 3:
                            query = query.Where(x => x.CustomizeTag3 != null);
                            break;
                        case 4:
                            query = query.Where(x => x.CustomizeTag4 != null);
                            break;
                        case 5:
                            query = query.Where(x => x.CustomizeTag5 != null);
                            break;
                        case 6:
                            query = query.Where(x => x.CustomizeTag6 != null);
                            break;
                        case 7:
                            query.Where(x => x.CustomizeTag7 != null);
                            break;
                        case 8:
                            query = query.Where(x => x.CustomizeTag8 != null);
                            break;
                        case 9:
                            query = query.Where(x => x.CustomizeTag9 != null);
                            break;
                        case 10:
                            query = query.Where(x => x.CustomizeTag10 != null);
                            break;
                    }
                }
            }
            else
            {
                ViewBag.role = "".Split(new char[] { ',' });

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
        [ValidateAntiForgeryToken, ValidateInput(false), HttpPost]

        public ActionResult Edit(int? id)
        {

            return View("Index");
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