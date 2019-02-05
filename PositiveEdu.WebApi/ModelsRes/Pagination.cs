using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace PositiveEdu.WebApi.ModelsRes
{
    /// <summary>
    /// 分页信息
    /// </summary>
    public class Pagination
    {
        public Pagination()
        { }

        public Pagination(IPagedList ip)
        {
            this.TotalItemCount = ip.TotalItemCount;
            this.PageCount = ip.PageCount;
            this.PageNumber = ip.PageNumber;
            this.PageSize = ip.PageSize;
        }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalItemCount { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// 每页记录数
        /// </summary>
        public int PageSize { get; set; }
    }
}