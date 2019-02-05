using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PositiveEdu.DAL;

namespace PositiveEdu.WebApi.ModelsRes
{
    /// <summary>
    /// 地区
    /// </summary>
    public class ModelArea
    {        
        public int AREA_ID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string AREA_NAME { get; set; }

        /// <summary>
        /// 上级地区
        /// </summary>
        public int? PARENT_ID { get; set; }

        /// <summary>
        /// 层级
        /// </summary>
        public int? LEVEL_ID { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? SORT_ID { get; set; }

        public ModelArea(AREA item)
        {
            AREA_ID = item.AREA_ID;
            AREA_NAME = item.AREA_NAME;
            PARENT_ID = item.PARENT_ID;
            LEVEL_ID = item.LEVEL_ID;
            SORT_ID = item.SORT_ID;
        }
    }
}