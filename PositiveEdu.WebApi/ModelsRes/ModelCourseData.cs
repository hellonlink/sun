using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PositiveEdu.DAL;
using PositiveEdu.Common;

namespace PositiveEdu.WebApi.ModelsRes
{
    /// <summary>
    /// 课程资源
    /// </summary>
    public class ModelCourseData
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string linkUrl { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string imgUrl { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? sort { get; set; }

        public ModelCourseData() { }

        public ModelCourseData(OnlineCourseData item)
        {
            name = item.name;
            linkUrl = item.dataLink;
            imgUrl = WebFunctions.ToAbsoluteResourceUrl(item.img);
            sort = item.sort;
        }
    }
}