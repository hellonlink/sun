using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PositiveEdu.WebApi.ModelsRes
{
    /// <summary>
    /// 首页轮动图
    /// </summary>
    public class ModelBanner
    {
        public int id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string model { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string imgUrl { get; set; }

        /// <summary>
        /// 是否首图
        /// </summary>
        public bool Is_First { get; set; }

        /// <summary>
        /// 跳转地址
        /// </summary>
        public string linkUrl { get; set; }
    }
}