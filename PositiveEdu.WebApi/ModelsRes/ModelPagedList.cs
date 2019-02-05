using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PositiveEdu.WebApi.ModelsRes
{
    /// <summary>
    /// 分页列表
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    public class ModelPagedList<T>
    {
        /// <summary>
        /// 数据列表
        /// </summary>
        public IEnumerable<T> List { get; set; }

        /// <summary>
        /// 分页信息
        /// </summary>
        public Pagination Pagination { get; set; }
    }
}