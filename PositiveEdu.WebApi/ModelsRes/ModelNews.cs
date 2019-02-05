using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PositiveEdu.WebApi.ModelsRes
{
    /// <summary>
    /// 新闻资讯
    /// </summary>
    public class ModelNews
    {
        public int id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 正文
        /// </summary>
        public string mainContent { get; set; }

        /// <summary>
        /// 发布日期
        /// </summary>
        public DateTime? publishDate { get; set; }
    }
}