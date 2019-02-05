using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PositiveEdu.WebApi.ModelsRes
{
    /// <summary>
    /// 全国讲师资质
    /// </summary>
    public class ModelQualifications
    {
        public int id { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 资质
        /// </summary>
        public string auth { get; set; }

        /// <summary>
        /// 认证日期
        /// </summary>
        public string date { get; set; }

        /// <summary>
        /// 发证机构
        /// </summary>
        public string institution { get; set; }

        /// <summary>
        /// 所在地
        /// </summary>
        public string location { get; set; }

        /// <summary>
        /// 开课期数
        /// </summary>
        public int Periods { get; set; }
    }
}