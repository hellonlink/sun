using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PositiveEdu.WebApi.ModelsRes
{
    /// <summary>
    /// 线上课程查询条件
    /// </summary>
    public class QueryCourse : QueryPagerBase
    {
        /// <summary>
        /// 全龄适用 - CatA01 / 讲师认证 - CatA02
        /// </summary>
        public string categoryA { get; set; }
   
        /// <summary>
        /// 课程分类
        /// </summary>
        public string categoryB { get; set; }

        /// <summary>
        /// 最小价格
        /// </summary>
        public decimal? minPrice { get; set; }

        /// <summary>
        /// 最大价格
        /// </summary>
        public decimal? maxPrice { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        public string key { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int? age { get; set; }

        /// <summary>
        /// 排序 (1-热门度；2-上架时间；3-价格，不填按热门度)
        /// </summary>
        public int? order { get; set; }

        /// <summary>
        /// 顺序 / 逆序 （1-顺序；2-逆序，不填按逆序）
        /// </summary>
        public int? orderAsc { get; set; }
    }
}