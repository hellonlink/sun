using System.Web;
using System.Web.Mvc;

namespace 拦截器过滤器日志
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
