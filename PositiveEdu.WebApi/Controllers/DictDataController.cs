using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PositiveEdu.WebApi.ModelsRes;

namespace PositiveEdu.WebApi.Controllers
{
    [RoutePrefix("api/DictData")]
    public class DictDataController : BaseApiController
    {
        /// <summary>
        /// 课程大类（全龄适用 - CatA01 / 讲师认证 - CatA02）
        /// </summary>
        /// <returns></returns>
        [Route("CourseCategoryA"), HttpGet]
        public IEnumerable<ModelDictData> CourseCategoryA()
        {
            var dict = DB.CourseCategoryA.AsNoTracking().ToList();
            var model = dict.Select(x => new ModelDictData()
            {
                id = x.id,
                name = x.name,
                value = x.value,
                weight = x.weight,
            }).OrderBy(x => x.weight).ToList();
            return model;
        }

        /// <summary>
        /// 课程分类
        /// </summary>
        /// <returns>课程分类</returns>
        [Route("CourseCategoryB"), HttpGet]
        public IEnumerable<ModelDictData> CourseCategoryB()
        {
            var dict = DB.CourseCategoryB.AsNoTracking().ToList();
            var model = dict.Select(x => new ModelDictData()
            {
                id = x.id,
                name = x.name,
                value = x.value,
                weight = x.weight,
            }).OrderBy(x => x.weight).ToList();
            return model;
        }

        /// <summary>
        /// 地区
        /// </summary>
        /// <returns>地区列表</returns>
        [Route("Area"), HttpGet]
        public IEnumerable<ModelArea> Area()
        {
            var areas = DB.AREA.AsNoTracking().OrderBy(x => x.LEVEL_ID).ThenBy(x => x.SORT_ID).ToList();

            var model = areas.Select(x => new ModelArea(x)).ToList();
            return model;
        }

        /// <summary>
        /// 职业资质
        /// </summary>
        /// <returns>职业资质</returns>
        [Route("Qualification"), HttpGet]
        public IEnumerable<ModelDictData> Qualification()
        {
            var dict = DB.QualificationDict.AsNoTracking().ToList();
            var model = dict.Select(x => new ModelDictData()
            {
                id = x.id,
                name = x.name,
                value = x.value,
                weight = x.weight,
            }).OrderBy(x => x.weight).ToList();
            return model;
        }
    }
}
