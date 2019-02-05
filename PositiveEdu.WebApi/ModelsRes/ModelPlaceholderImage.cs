using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PositiveEdu.DAL;
using PositiveEdu.Common;

namespace PositiveEdu.WebApi.ModelsRes
{
    /// <summary>
    /// 占位图
    /// </summary>
    public class ModelPlaceholderImage
    {
        public int id { get; set; }

        /// <summary>
        /// 标记
        /// </summary>
        public string model { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string imgUrl { get; set; }

        /// <summary>
        /// 跳转链接
        /// </summary>
        public string linkUrl { get; set; }

        /// <summary>
        /// 跳转链接（移动端）
        /// </summary>
        public string linkUrlMB { get; set; }

        /// <summary>
        /// 宽度
        /// </summary>
        public int? width { get; set; }

        /// <summary>
        /// 高度
        /// </summary>
        public int? height { get; set; }

        /// <summary>
        /// 顺序
        /// </summary>
        public int? sort { get; set; }

        public ModelPlaceholderImage() { }

        public ModelPlaceholderImage(PlaceholderImage item)
        {
            id = item.id;
            model = item.model;
            imgUrl = WebFunctions.ToAbsoluteResourceUrl(item.imgUrl);
            linkUrl = item.linkUrl;
            width = item.width;
            height = item.height;
            sort = item.sort;
            linkUrlMB = item.linkUrlMB;
        }
    }
}