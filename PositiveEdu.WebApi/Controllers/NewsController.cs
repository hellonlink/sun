using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PositiveEdu.WebApi.ModelsRes;
using PagedList;

namespace PositiveEdu.WebApi.Controllers
{
    /// <summary>
    /// 新闻资讯
    /// </summary>
    [RoutePrefix("api/News")]
    public class NewsController : BaseApiController
    {
        /// <summary>
        /// 新闻资讯
        /// </summary>
        /// <param name="page">页码</param>
        /// <returns>新闻</returns>
        [Route("List/{page?}"), HttpGet]
        public ModelPagedList<ModelNews> List([FromUri] int page = 1)
        {
            int pageSize = 5;

            var query = DB.News.AsNoTracking().AsQueryable().OrderBy(x => x.publishDate);
            var result = query.ToPagedList(page, pageSize);

            var model = new ModelPagedList<ModelNews>()
            {
                Pagination = new Pagination(result),
                List = result.Select(x => new ModelNews()
                {
                    id = x.id,
                    title = x.title,
                    publishDate = x.publishDate
                }).ToList()
            };
            return model;
        }

        /// <summary>
        /// 新闻详情
        /// </summary>
        /// <param name="id">新闻编号</param>
        /// <returns>新闻</returns>
        [Route("{id}"), HttpGet]
        public ModelNews Details([FromUri] int id)
        {
            var news = DB.News.Where(x => x.id == id).FirstOrDefault();

            if (news == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var model = new ModelNews()
            {
                id = news.id,
                title = news.title,
                mainContent = ContentToHtml(news.mainContent),
                publishDate = news.publishDate
            };
            return model;
        }
    }
}
