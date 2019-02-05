using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PositiveEdu.WebApi.ModelsRes
{
    /// <summary>
    /// 全国讲师资质查询条件
    /// </summary>
    public class QueryQualifications : QueryPagerBase
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 资质
        /// </summary>
        public string Qualification { get; set; }

        /// <summary>
        /// 所在地
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 开课期数（可为空）
        /// </summary>
        public int? Periods { get; set; }
    }
}