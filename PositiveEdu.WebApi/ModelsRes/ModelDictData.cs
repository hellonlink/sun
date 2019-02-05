using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PositiveEdu.WebApi.ModelsRes
{
    /// <summary>
    /// 字典
    /// </summary>
    public class ModelDictData
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 辅助值
        /// </summary>
        public string value { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int weight { get; set; }
    }
}