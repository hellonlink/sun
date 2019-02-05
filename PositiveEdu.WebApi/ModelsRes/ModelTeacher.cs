using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PositiveEdu.DAL;
using PositiveEdu.Common;

namespace PositiveEdu.WebApi.ModelsRes
{
    /// <summary>
    /// 老师信息
    /// </summary>
    public class ModelTeacher
    {
        public int id { get; set; }

        /// <summary>
        /// 照片
        /// </summary>
        public string photo { get; set; }

        /// <summary>
        /// 名字
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 资质
        /// </summary>
        public string lever { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 英文名
        /// </summary>
        public string name_en { get; set; }

        /// <summary>
        /// 顺序
        /// </summary>
        public int? sort { get; set; }

        public ModelTeacher() { }

        public ModelTeacher(Teacher item)
        {
            id = item.id;
            photo = WebFunctions.ToAbsoluteResourceUrl(item.photo);
            name = item.name;
            description = item.description;
            lever = item.lever;
            type = item.type;
            name_en = item.name_en;
            sort = item.sort;
        }

    }
}