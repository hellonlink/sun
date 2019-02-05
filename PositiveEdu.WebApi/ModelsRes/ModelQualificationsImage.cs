using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PositiveEdu.DAL;
using PositiveEdu.Common;

namespace PositiveEdu.WebApi.ModelsRes
{
    /// <summary>
    /// 讲师资质图
    /// </summary>
    public class ModelQualificationsImage
    {
        /// <summary>
        /// 图片
        /// </summary>
        public string ImgUrl { get; set; }

        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 资质
        /// </summary>
        public string Qualification { get; set; }

        /// <summary>
        /// 顺序
        /// </summary>
        public int? Sort { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        public string LinkUrl { get; set; }

        public ModelQualificationsImage() { }

        public ModelQualificationsImage(PlaceholderImage item)
        {
            ImgUrl = WebFunctions.ToAbsoluteResourceUrl(item.imgUrl);
            Name = item.text1;
            Qualification = item.text2;
            Sort = item.sort;
            LinkUrl = item.linkUrl;
        }
    }
}