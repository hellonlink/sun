using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList.Mvc;

namespace PositiveEdu.Common
{
    public class PagerUtility
    {
        public static PagedListRenderOptions GetPagedListRenderOptions()
        {
            PagedListRenderOptions options = new PagedListRenderOptions()
            {
                LinkToPreviousPageFormat = "上一页",
                LinkToNextPageFormat = "下一页",
                LinkToFirstPageFormat = "首页",
                LinkToLastPageFormat = "尾页",
                DisplayLinkToNextPage = PagedListDisplayMode.Always,
                DisplayLinkToPreviousPage = PagedListDisplayMode.Always,
                DisplayLinkToFirstPage = PagedListDisplayMode.Always,
                DisplayLinkToLastPage = PagedListDisplayMode.Always
            };
            return options;
        }
    }
}
