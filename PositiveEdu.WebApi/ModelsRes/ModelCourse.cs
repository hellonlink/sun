using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PositiveEdu.DAL;
using PositiveEdu.Common;

namespace PositiveEdu.WebApi.ModelsRes
{
    /// <summary>
    /// 课程
    /// </summary>
    public class ModelCourse
    {
        public int id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 讲师
        /// </summary>
        public string teacher { get; set; }

        /// <summary>
        /// 课程简介
        /// </summary>
        public string simpleIntro { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal price { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string imgUrl { get; set; }

        /// <summary>
        /// 是否收藏
        /// </summary>
        public bool IsFavorite { get; set; }

        public ModelCourse() { }

        public ModelCourse(OnlineCourse item)
        {
            id = item.id;
            title = item.name;
            simpleIntro = item.simpleIntro;
            price = item.price.HasValue ? item.price.Value : 0;
            teacher = item.lecturer;
            imgUrl = WebFunctions.ToAbsoluteResourceUrl(item.img);
        }
    }
}