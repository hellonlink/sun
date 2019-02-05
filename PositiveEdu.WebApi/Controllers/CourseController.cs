using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PagedList;
using PositiveEdu.WebApi.ModelsRes;
using PositiveEdu.Common;
using PositiveEdu.DAL;

namespace PositiveEdu.WebApi.Controllers
{
    /// <summary>
    /// 线上课程
    /// </summary>
    [RoutePrefix("api/Course")]
    public class CourseController : BaseApiController
    {
        /// <summary>
        /// 课程检索
        /// </summary>
        /// <param name="param">检索条件</param>
        /// <returns>课程</returns>
        [Route("Query"), HttpPost]
        public ModelPagedList<ModelCourse> Query([FromBody] QueryCourse param)
        {
            int page = 1;
            int pageSize = 6;

            var query = DB.OnlineCourse.AsNoTracking().AsQueryable();

            if (param != null)
            {
                page = param.PageIndex;
                pageSize = param.PageSize;

                if (!string.IsNullOrWhiteSpace(param.categoryA))
                    query = query.Where(x => x.categoryA == param.categoryA);

                if (!string.IsNullOrWhiteSpace(param.categoryB))
                    query = query.Where(x => x.categoryB == param.categoryB);

                if (!string.IsNullOrWhiteSpace(param.key))
                    query = query.Where(x => x.name.Contains(param.key));

                if (param.minPrice.HasValue)
                    query = query.Where(x => x.price >= param.minPrice);

                if (param.maxPrice.HasValue)
                    query = query.Where(x => x.price <= param.maxPrice);

                if (param.age.HasValue)
                    query = query.Where(x => x.fromAge <= param.age && x.toAge >= param.age);
            }

            int order = 1;
            int orderAsc = 2;
            if (param != null)
            {
                if (param.order.HasValue)
                    order = param.order.Value;
                if (param.orderAsc.HasValue)
                    orderAsc = param.orderAsc.Value;
            }

            if (orderAsc == 1)
            {
                if (order == 1)
                    query = query.OrderBy(x => x.viewCount);
                else if (order == 2)
                    query = query.OrderBy(x => x.publishedTime);
                else
                    query = query.OrderBy(x => x.price);
            }
            else
            {
                if (order == 1)
                    query = query.OrderByDescending(x => x.viewCount);
                else if (order == 2)
                    query = query.OrderByDescending(x => x.publishedTime);
                else
                    query = query.OrderByDescending(x => x.price);
            }

            var result = query.ToPagedList(page, pageSize);

            List<Favorite> favs = new List<DAL.Favorite>();
            if (User.Identity.IsAuthenticated)
            {
                if (AuthCustomer != null)
                {
                    var ids = result.Select(x => x.courseId).ToList();
                    favs = DB.Favorite.AsNoTracking().Where(x => x.customerId == AuthCustomer.CustomerId && ids.Contains(x.courseId)).ToList();                    
                }
            }

            var model = new ModelPagedList<ModelCourse>()
            {
                Pagination = new Pagination(result),
                List = result.Select(x => new ModelCourse(x)).ToList()
            };

            foreach(var m in model.List)
            {
                if (favs.Any(x => x.courseId == m.id))
                    m.IsFavorite = true;
            }

            return model;
        }

        /// <summary>
        /// 课程详情
        /// </summary>
        /// <param name="id">课程id</param>
        /// <returns>课程信息</returns>
        [Route("{id}"), HttpGet]
        public ModelCourseDetails Details([FromUri] int id)
        {
            var item = DB.OnlineCourse.AsNoTracking().Where(x => x.id == id).FirstOrDefault();
            if (item == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var related = item.OnlineCourse2.OrderByDescending(x => x.viewCount).ThenByDescending(x => x.publishedTime).ToList();

            var model = new ModelCourseDetails(item, related);

            if (User.Identity.IsAuthenticated)
            {
                if (AuthCustomer != null)
                {
                    var fav = DB.Favorite.AsNoTracking().Any(x => x.customerId == AuthCustomer.CustomerId && x.courseId == id);
                    model.IsFavorite = fav;

                    model.Bought = DB.MyOrder.Any(x => x.CUSTOMER_ID == AuthCustomer.CustomerId
                        && x.OnLineCourseId == id
                        && x.PAYMWNT_STATUS == ConstValue.OrderStatus.Paid);
                }
            }

            return model;
        }

        /// <summary>
        /// 课程评论
        /// </summary>
        /// <param name="courseId">课程id</param>
        /// <param name="page">评论页码</param>
        /// <returns>课程评论</returns>
        [Route("{courseId}/Review/{page?}"), HttpGet]
        public ModelPagedList<ModelReview> GetReview([FromUri]int courseId, int page = 1)
        {
            int pageSize = 5;

            var query = DB.Review.AsNoTracking().AsQueryable();
            query = query.Where(x => x.courseId == courseId);
            query = query.OrderByDescending(x => x.up).ThenByDescending(x => x.createdTime);
            var result = query.ToPagedList(page, pageSize);

            var model = new ModelPagedList<ModelReview>()
            {
                Pagination = new Pagination(result),
                List = result.Select(x => new ModelReview(x)).ToList()
            };
            return model;
        }

        /// <summary>
        /// 试听/看 获取试看课程的地址
        /// </summary>
        /// <param name="id">课程id</param>
        /// <returns>播放地址</returns>
        [Route("Trial/{id}"), HttpGet]
        public IEnumerable<string> Trial([FromUri] int id)
        {
            var items = DB.OnlineCourseData.AsNoTracking()
                .Where(x => x.courseId == id && x.dataType == ConstValue.CourseDataType.TryVideo)
                .OrderBy(x => x.sort).ToList();
            var model = items.Select(x => x.dataLink).ToList();

            if (User.Identity.IsAuthenticated)
            {
                var trial = new Trial()
                {
                    courseId = id,
                    customerId = AuthCustomer.CustomerId,
                    createdTime = DateTime.Now,
                    sentMail = false,
                };
                DB.Trial.Add(trial);
                DB.SaveChanges();
            }

            return model;
        }

        /// <summary>
        /// 开始学习 获取完整课程的地址
        /// </summary>
        /// <param name="id">课程id</param>
        /// <returns>播放地址</returns>
        [Route("Full/{id}"), HttpGet, Authorize]
        public IEnumerable<ModelCourseData> Full([FromUri] int id)
        {
            var bought = DB.MyOrder.Any(x => x.CUSTOMER_ID == AuthCustomer.CustomerId && x.OnLineCourseId == id && x.PAYMWNT_STATUS == ConstValue.OrderStatus.Paid);
            if (bought)
            {
                var items = DB.OnlineCourseData.AsNoTracking()
                    .Where(x => x.courseId == id && x.dataType == ConstValue.CourseDataType.Video)
                    .OrderBy(x => x.sort).ToList();
                var model = items.Select(x => new ModelCourseData(x)).ToList();
                return model;
            }
            else
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.Unauthorized, MakeMessage("未购买该课程")));
            }
        }

        /// <summary>
        /// 下单
        /// </summary>
        /// <param name="courseId">课程id</param>
        /// <returns>订单编号</returns>
        [Route("Buy"), HttpPost, Authorize]
        public string Buy([FromBody] int courseId)
        {
            //var bought = DB.MyOrder.Any(x => x.CUSTOMER_ID == AuthCustomer.CustomerId && x.OnLineCourseId == courseId && x.PAYMWNT_STATUS == ConstValue.OrderStatus.Paid);
            //if (bought)
            //    throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest, MakeMessage("请勿重复购买")));

            var course = DB.OnlineCourse.AsNoTracking().Where(x => x.id == courseId).FirstOrDefault();
            if (course == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);


            string orderNumber = "O" + courseId + DateTime.Now.ToString("yyyyMMddHHmmss") + WebFunctions.GetRandomWords(3);

            MyOrder newOrder = new MyOrder()
            {
                ORDER_NUMBER = orderNumber,
                OnLineCourseId = courseId,
                CUSTOMER_ID = AuthCustomer.CustomerId,
                DATE = DateTime.Now,
                NAME = course.name,
                PRICE_PRODUCT = (double)course.price,
                PRICE_PAY = (double)course.price,
                TOTAL = (double)course.price,                
                PAYMWNT_STATUS = ConstValue.OrderStatus.Unpaid,
            };

            DB.MyOrder.Add(newOrder);
            DB.SaveChanges();

            return orderNumber;
        }

        /// <summary>
        /// 获取订单 待支付
        /// </summary>
        /// <param name="orderNumber">订单编号</param>
        /// <returns>订单信息</returns>
        [Route("Order/{orderNumber}"), HttpGet]
        public ModelOrder Order([FromUri] string orderNumber)
        {
            var item = DB.MyOrder.AsNoTracking().Where(x => x.ORDER_NUMBER == orderNumber).FirstOrDefault();
            if (item == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            ModelOrder model = new ModelOrder(item);
            return model;
        }

        /// <summary>
        /// 订单支付 生成支付二维码 （支付宝）
        /// </summary>
        /// <param name="id">订单id</param>
        /// <returns>二维码地址</returns>
        [Route("PayCode/AliPay/{id}"), HttpGet]
        public string PayCodeAliPay([FromUri] int id)
        {
            var item = DB.MyOrder.AsNoTracking().Where(x => x.MYORDER_ID == id).FirstOrDefault();

            if (item != null)
            {
                if (item.PAYMWNT_STATUS == ConstValue.OrderStatus.Unpaid)
                {
                    Pay.PayBase pay = new Pay.Ali.AliPay();
                    bool result = pay.TradePrecreate(item);
                    if (result)
                        return pay.QrCodeUrl;
                    else
                        return pay.PrecreateMessage;
                }
                return "该订单已经支付";
            }

            throw new HttpResponseException(HttpStatusCode.NotFound);
        }


        /// <summary>
        /// 订单支付 生成支付二维码 （微信）
        /// </summary>
        /// <param name="id">订单id</param>
        /// <returns>二维码地址</returns>
        [Route("PayCode/WxPay/{id}"), HttpGet]
        public string PayCodeWxPay([FromUri] int id)
        {
            var item = DB.MyOrder.AsNoTracking().Where(x => x.MYORDER_ID == id).FirstOrDefault();

            if (item != null)
            {
                if (item.PAYMWNT_STATUS == ConstValue.OrderStatus.Unpaid)
                {
                    Pay.PayBase pay = new Pay.Wx.WxPay();
                    bool result = pay.TradePrecreate(item);
                    if (result)
                        return pay.QrCodeUrl;
                    else
                        return pay.PrecreateMessage;
                }
                return "该订单已经支付";
            }

            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        /// <summary>
        /// 订单支付 （微信客户端H5支付）
        /// </summary>
        /// <param name="id">订单id</param>
        /// <param name="wxCode">微信授权code</param>
        /// <returns>wxJsApiParam</returns>
        [Route("PayWxJsApi/{id}"), HttpGet]
        public string PayWxJsApi([FromUri] int id, string wxCode)
        {
            var item = DB.MyOrder.AsNoTracking().Where(x => x.MYORDER_ID == id).FirstOrDefault();

            if (item != null)
            {
                if (item.PAYMWNT_STATUS == ConstValue.OrderStatus.Unpaid)
                {
                    Pay.Wx.JsApiPay jsApiPay = new Pay.Wx.JsApiPay(null);
                    jsApiPay.GetOpenidAndAccessTokenFromCode(wxCode);
                    jsApiPay.total_fee = (int)item.PRICE_PAY * 100;
                    var unifiedOrderResult = jsApiPay.GetUnifiedOrderResult();
                    var wxJsApiParam = jsApiPay.GetJsApiParameters();//获取H5调起JS API参数                    
                    Pay.Wx.Log.Debug(this.GetType().ToString(), "wxJsApiParam : " + wxJsApiParam);
                    return wxJsApiParam;
                }
                return "该订单已经支付";
            }

            throw new HttpResponseException(HttpStatusCode.NotFound);

        }

        /// <summary>
        /// 监测支付状态
        /// </summary>
        /// <param name="id">订单id</param>
        /// <returns>支付结果</returns>
        [Route("MonitorPaid/{id}"), HttpGet]
        public HttpResponseMessage MonitorPaid([FromUri] int id)
        {
            var pass = DB.MyOrder.Any(x => x.MYORDER_ID == id && x.PAYMWNT_STATUS == ConstValue.OrderStatus.Paid);
            if (pass)
                return Request.CreateResponse(HttpStatusCode.OK, MakeMessage("支付成功"));
            else
                return Request.CreateResponse(HttpStatusCode.NotFound, MakeMessage("尚未支付"));
        }

        /// <summary>
        /// 评价课程
        /// </summary>
        /// <param name="id">课程id</param>
        /// <param name="text">评论内容</param>
        /// <returns>评价结果</returns>
        [Route("Review/{id}"), HttpPost, Authorize]
        public HttpResponseMessage Review([FromUri] int id, [FromBody] string text)
        {
            if (text == null)
                text = string.Empty;

            Review item = new Review()
            {
                courseId = id,
                comment = text.Trim(),
                createdTime = DateTime.Now,
                up = 0,
                customerId = AuthCustomer.CustomerId,
            };
            DB.Review.Add(item);
            DB.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK, MakeMessage("评价成功"));
        }

        /// <summary>
        /// 对评论点赞
        /// </summary>
        /// <param name="id">评论id</param>
        /// <returns></returns>
        [Route("Review/Up/{id}"), HttpPost, Authorize]
        public HttpResponseMessage UpReview([FromUri] int id)
        {
            var item = DB.Review.Where(x => x.id == id).FirstOrDefault();
            if (item != null)
            {
                item.up++;
                DB.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, MakeMessage("点赞成功"));
            }
            else
                return Request.CreateResponse(HttpStatusCode.NotFound, MakeMessage("评论不存在"));
        }

        /// <summary>
        /// 收藏课程（已收藏的再次调用变成取消）
        /// </summary>
        /// <param name="id">课程id</param>
        /// <returns>收藏结果</returns>
        [Route("Favorite/{id}"), HttpPost, Authorize]
        public HttpResponseMessage Favorite([FromUri] int id)
        {
            var item = DB.Favorite.Where(x => x.customerId == AuthCustomer.CustomerId && x.courseId == id).ToList();
            if (item.Count > 0)
            {
                DB.Favorite.RemoveRange(item);
                DB.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, MakeMessage("取消收藏"));
            }
            else
            {
                Favorite newItem = new Favorite()
                {
                    courseId = id,
                    createdTime = DateTime.Now,
                    customerId = AuthCustomer.CustomerId,
                };
                DB.Favorite.Add(newItem);
                DB.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, MakeMessage("收藏成功"));
            }
        }
    }
}
