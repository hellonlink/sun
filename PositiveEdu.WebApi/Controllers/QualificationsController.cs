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
    /// 全国讲师资质查询
    /// </summary>
    [RoutePrefix("api/Qualifications")]
    public class QualificationsController : BaseApiController
    {
        /// <summary>
        /// 全国讲师资质查询
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>讲师资质</returns>
        [Route("Query"), HttpPost]
        public ModelPagedList<ModelQualifications> Query([FromBody] QueryQualifications param)
        {
            int page = 1;
            int pageSize = 5;

            var query = DB.Qualifications.AsNoTracking().AsQueryable();

            if (param != null)
            {
                page = param.PageIndex;
                pageSize = param.PageSize;

                if (!string.IsNullOrWhiteSpace(param.Name))
                    query = query.Where(x => x.name.Contains(param.Name));

                if (!string.IsNullOrWhiteSpace(param.Qualification))
                    query = query.Where(x => x.auth.Contains(param.Qualification));

                if (!string.IsNullOrWhiteSpace(param.Location))
                    query = query.Where(x => x.location.Contains(param.Location));

                //if (param.Periods.HasValue)
                //    query = query.Where(x=>x.)
            }

            query = query.OrderBy(x => x.date);

            var result = query.ToPagedList(page, pageSize);

            var model = new ModelPagedList<ModelQualifications>()
            {
                Pagination = new Pagination(result),
                List = result.Select(x => new ModelQualifications()
                {
                    id = x.id,
                    name = x.name,
                    auth = x.auth,
                    date = x.date,
                    institution = x.institution,
                    location = x.location,
                    Periods = 3,    //temp
                }).ToList()
            };
            return model;
        }


        /// <summary>
        /// 全国讲师资质图
        /// </summary>
        /// <returns></returns>
        [Route("Images"), HttpGet]
        public IEnumerable<ModelQualificationsImage> Images()
        {
            var query = DB.PlaceholderImage.AsNoTracking().Where(x => x.holderType == "QualificationsImage").OrderBy(x => x.sort);
            var result = query.ToList();
            var model = result.Select(x => new ModelQualificationsImage(x)).ToList();
            return model;
        }
    }
}
