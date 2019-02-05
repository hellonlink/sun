using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using PositiveEdu.WebApi.ModelsRes;
using PositiveEdu.Common;
using System.IO;
using PagedList;
using PositiveEdu.DAL;

namespace PositiveEdu.WebApi.Controllers
{
    /// <summary>
    /// 会员中心
    /// </summary>
    [RoutePrefix("api/Member"), Authorize]
    public class MemberController : BaseApiController
    {
        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns>用户信息</returns>
        [Route("Customer"), HttpGet]
        public ModelCustomer Customer()
        {
            var item = DB.Customer.Where(x => x.CUSTOMER_ID == AuthCustomer.CustomerId).FirstOrDefault();
            if (item == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var model = new ModelCustomer(item);
            return model;
        }

        /// <summary>
        /// 上传头像
        /// </summary>
        /// <returns>上传结果</returns>
        [Route("UploadPortrait"), HttpPost]
        public HttpResponseMessage UploadPortrait()
        {
            if (HttpContext.Current.Request.Files.Count == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Format("请上传文件。"));
            }

            try
            {
                HttpPostedFile file = HttpContext.Current.Request.Files[0];

                string vPath = string.Format(WebFunctions.ResourceMapRoot + "/Images/Customer/{0}/{1}", AuthCustomer.CustomerId, file.FileName);
                string pPath = HttpContext.Current.Server.MapPath(vPath);

                var directory = Path.GetDirectoryName(pPath);
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                file.SaveAs(pPath);

                var cus = DB.Customer.Where(x => x.CUSTOMER_ID == AuthCustomer.CustomerId).FirstOrDefault();
                cus.HEAD_PORTEAIT = vPath.TrimStart('~');
                DB.SaveChanges();

                var result = new
                {
                    message = "头像修改成功。",
                    AvatarUrl = WebFunctions.ToAbsoluteResourceUrl(cus.HEAD_PORTEAIT),
                };

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        /// <summary>
        /// 保存个人信息
        /// </summary>
        /// <param name="model">个人信息</param>
        /// <returns>保存结果</returns>
        [Route("SaveProfile"), HttpPost]
        public HttpResponseMessage SaveProfile([FromBody] ReqModelProfile model)
        {
            var cus = DB.Customer.Where(x => x.CUSTOMER_ID == AuthCustomer.CustomerId).FirstOrDefault();
            cus.REAL_NAME = model.RealName;
            cus.SEX = model.Sex;
            if (model.Birthday.HasValue)
                cus.BIRTHDAY = DateTime.Parse(model.Birthday.Value.ToString("yyyy-MM-dd"));

            cus.PROVINCE = model.Province;
            cus.MARKET = model.Market;
            cus.intro = model.Intro;
            DB.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK, MakeMessage("保存成功"));
        }

        /// <summary>
        /// 保存职业信息
        /// </summary>
        /// <param name="model">职业信息</param>
        /// <returns>保存结果</returns>
        [Route("SaveCareer"), HttpPost]
        public HttpResponseMessage SaveCareer([FromBody] ReqModelCareer model)
        {
            var cus = DB.Customer.Where(x => x.CUSTOMER_ID == AuthCustomer.CustomerId).FirstOrDefault();
            cus.qualification = model.qualification;
            cus.periods = model.periods;
            DB.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK, MakeMessage("保存成功"));
        }

        /// <summary>
        /// 我的线上课程
        /// </summary>
        /// <param name="page">页码</param>
        /// <returns>已购买的课程</returns>
        [Route("MyCourse/{page?}"), HttpGet]
        public ModelPagedList<ModelCourse> MyCourse(int page = 1)
        {
            int pageSize = 6;

            var paidOrderQuery = DB.MyOrder.AsNoTracking().Where(x => x.CUSTOMER_ID == AuthCustomer.CustomerId && x.PAYMWNT_STATUS == ConstValue.OrderStatus.Paid);
            paidOrderQuery = paidOrderQuery.OrderByDescending(x => x.DATE);
            var result = paidOrderQuery.ToPagedList(page, pageSize);

            var model = new ModelPagedList<ModelCourse>()
            {
                Pagination = new Pagination(result),
                List = result.Select(x => new ModelCourse(x.OnlineCourse)).ToList()
            };

            return model;
        }

        /// <summary>
        /// 我的评论
        /// </summary>
        /// <param name="page">页码</param>
        /// <returns>我的评论</returns>
        [Route("MyReview/{page?}"), HttpGet]
        public ModelPagedList<ModelReview> MyReview(int page = 1)
        {
            int pageSize = 10;

            var query = DB.Review.AsNoTracking().Where(x => x.customerId == AuthCustomer.CustomerId);
            query = query.OrderByDescending(x => x.createdTime);

            var result = query.ToPagedList(page, pageSize);

            var model = new ModelPagedList<ModelReview>()
            {
                Pagination = new Pagination(result),
                List = result.Select(x => new ModelReview(x)).ToList()
            };

            return model;
        }

        /// <summary>
        /// 我的下载
        /// </summary>
        /// <param name="page">页码</param>
        /// <returns>下载过的文档</returns>
        [Route("MyDownload/{page?}"), HttpGet]
        public ModelPagedList<ModelDocument> MyDownload(int page = 1)
        {
            int pageSize = 10;

            var query = DB.MyDocument.AsNoTracking().Where(x => x.customerId == AuthCustomer.CustomerId);
            query = query.OrderByDescending(x => x.createdTime);

            var result = query.ToPagedList(page, pageSize);

            var model = new ModelPagedList<ModelDocument>()
            {
                Pagination = new Pagination(result),
                List = result.Select(x => new ModelDocument(x.Document, AuthCustomer)).ToList()
            };

            return model;
        }

        /// <summary>
        /// 我的消息 - 站内公告
        /// </summary>
        /// <param name="page">页码</param>
        /// <returns>站内公告</returns>
        [Route("SiteMessage/{page?}"), HttpGet]
        public ModelPagedList<ModelSiteMessage> SiteMessage(int page = 1)
        {
            int pageSize = 10;

            var query = DB.SiteMessage.AsNoTracking().AsQueryable();
            query = query.OrderByDescending(x => x.createdTime);

            var result = query.ToPagedList(page, pageSize);

            var model = new ModelPagedList<ModelSiteMessage>()
            {
                Pagination = new Pagination(result),
                List = result.Select(x => new ModelSiteMessage(x)).ToList()
            };

            return model;
        }

        /// <summary>
        /// 我的消息 - 系统消息
        /// </summary>
        /// <param name="page">页码</param>
        /// <returns></returns>
        [Route("MyMessage/{page?}"), HttpGet]
        public ModelPagedList<ModelMessage> MyMessage(int page = 1)
        {
            int pageSize = 10;

            var query = DB.MyMessage.AsNoTracking().Where(x => x.customerId == AuthCustomer.CustomerId);
            query = query.OrderByDescending(x => x.createTime);

            var result = query.ToPagedList(page, pageSize);

            var model = new ModelPagedList<ModelMessage>()
            {
                Pagination = new Pagination(result),
                List = result.Select(x => new ModelMessage(x)).ToList()
            };

            return model;
        }

        /// <summary>
        /// 我的收藏
        /// </summary>
        /// <param name="page">页码</param>
        /// <returns>收藏的课程</returns>
        [Route("MyFavorite/{page?}"), HttpGet]
        public ModelPagedList<ModelCourse> MyFavorite(int page = 1)
        {
            int pageSize = 6;

            var query = DB.Favorite.AsNoTracking().Where(x => x.customerId == AuthCustomer.CustomerId);
            query = query.OrderBy(x => x.OnlineCourse.publishedTime);

            var result = query.ToPagedList(page, pageSize);

            var model = new ModelPagedList<ModelCourse>()
            {
                Pagination = new Pagination(result),
                List = result.Select(x => new ModelCourse(x.OnlineCourse)).ToList()
            };

            return model;
        }

        /// <summary>
        /// 设置密码
        /// </summary>
        /// <param name="model">新旧密码</param>
        /// <returns>设置结果</returns>
        [Route("SetPassword"), HttpPost]
        public HttpResponseMessage SetPassword(ReqModelSetPassword model)
        {
            if (string.IsNullOrWhiteSpace(model.NewPassword))
                return Request.CreateResponse(HttpStatusCode.BadRequest, MakeMessage("新密码不能为空"));

            if (model.NewPassword != model.ConfirmPassword)
                return Request.CreateResponse(HttpStatusCode.BadRequest, MakeMessage("两次密码输入不同"));

            var cus = DB.Customer.Where(x => x.CUSTOMER_ID == AuthCustomer.CustomerId).FirstOrDefault();
            if (cus.USER_PASSWORD != model.OldPassword)
                return Request.CreateResponse(HttpStatusCode.BadRequest, MakeMessage("旧密码错误"));

            cus.USER_PASSWORD = model.NewPassword;            
            DB.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK, MakeMessage("设置成功"));
        }

        /// <summary>
        /// 提交家长评估结果 （A/B/C/D）
        /// </summary>
        /// <param name="result">评估结果（A/B/C/D）</param>
        /// <returns>提交结果</returns>
        [Route("ParentAssessment"), HttpPost]
        public HttpResponseMessage PostParentAssessment(string result)
        {
            var pa = DB.ParentAssessment.Where(x => x.customerId == AuthCustomer.CustomerId).FirstOrDefault();
            if (pa == null)
            {
                pa = new ParentAssessment()
                {
                    customerId = AuthCustomer.CustomerId,
                    result = result,
                    createdTime = DateTime.Now,
                    updatedTime = DateTime.Now,
                };
                DB.ParentAssessment.Add(pa);
            }
            else
            {
                pa.result = result;
                pa.updatedTime = DateTime.Now;
            }
            DB.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK, MakeMessage("提交成功"));
        }

        /// <summary>
        /// 家长评估后的推荐
        /// </summary>
        /// <param name="result">评估结果（A/B/C/D）</param>
        /// <returns>html字符串</returns>
        [Route("ParentAssessment/Recommend/{result}"), HttpGet]
        public string GetParentAssessmentRecommend([FromUri] string result)
        {
            var re = DB.ParentAssessmentRecommend.Where(x => x.resultKey == result).FirstOrDefault();
            if (re != null)
                return ContentToHtml(re.recommendContent);
            else
                return string.Empty;
        }
    }
}
