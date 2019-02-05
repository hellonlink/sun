using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace PositiveEdu.Admin
{
    public static class HMTLHelperExtensions
    {
        public static string IsSelected(this HtmlHelper html, string controller = null, string action = null, string cssClass = null)
        {

            if (String.IsNullOrEmpty(cssClass)) 
                cssClass = "active";

            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            string currentController = (string)html.ViewContext.RouteData.Values["controller"];

            if (String.IsNullOrEmpty(controller))
                controller = currentController;

            if (String.IsNullOrEmpty(action))
                action = currentAction;

            return controller == currentController && action == currentAction ?
                cssClass : String.Empty;
        }

        public static string PageClass(this HtmlHelper html)
        {
            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            return currentAction;
        }

        public static string BoolString(this HtmlHelper html, bool boolean, string result)
        {
            return BoolString(html, boolean, result, string.Empty);
        }

        public static string BoolString(this HtmlHelper html, bool boolean, string result, string result2)
        {
            if (boolean)
                return result;
            else
                return result2;
        }

        public static MvcHtmlString ConfirmLink(this HtmlHelper helper, string linkText, string actionName, object routeValues)
        {
            return ConfirmLink(helper, linkText, actionName, routeValues, null);
        }

        public static MvcHtmlString ConfirmLink(this HtmlHelper helper, string linkText, string actionName, object routeValues, object htmlAttributes)
        {
            string confirmTitle = linkText;
            string confirmContent = string.Format("确定要{0}吗？", linkText);
            return ConfirmLink(helper, linkText, actionName, confirmTitle, confirmContent, routeValues, htmlAttributes);
        }

        public static MvcHtmlString ConfirmLink(this HtmlHelper helper, string linkText, string actionName, string controllerName, object routeValues, object htmlAttributes)
        {
            string confirmTitle = linkText;
            string confirmContent = string.Format("确定要{0}吗？", linkText);
            return ConfirmLink(helper, linkText, actionName, controllerName, confirmTitle, confirmContent, routeValues, htmlAttributes);
        }

        public static MvcHtmlString ConfirmLink(this HtmlHelper helper, string linkText, string actionName, string confirmTitle, string confirmContent, object routeValues)
        {
            return ConfirmLink(helper, linkText, actionName, confirmTitle, confirmContent, routeValues, null);
        }


        public static MvcHtmlString ConfirmLink(this HtmlHelper helper, string linkText, string actionName, string confirmTitle, string confirmContent, object routeValues, object htmlAttributes)
        {
            return ConfirmLink(helper, linkText, actionName, null, confirmTitle, confirmContent, routeValues, htmlAttributes);
        }

        public static MvcHtmlString ConfirmLink(this HtmlHelper helper, string linkText, string actionName, string controllerName, string confirmTitle, string confirmContent, object routeValues, object htmlAttributes)
        {
            string json = Json.Encode(new
            {

                url = new UrlHelper(helper.ViewContext.RequestContext).Action(actionName, controllerName, routeValues),
                title = confirmTitle,
                content = confirmContent,
            });

            TagBuilder builder = new TagBuilder("a");
            builder.MergeAttribute("href", "javascript:;");
            builder.InnerHtml = linkText;
            if (htmlAttributes != null)
            {
                foreach (System.Reflection.PropertyInfo p in htmlAttributes.GetType().GetProperties())
                {
                    builder.MergeAttribute(p.Name, p.GetValue(htmlAttributes).ToString());
                }
            }
            builder.AddCssClass("confirm-btn");

            MvcHtmlString hidden = helper.Hidden("ConfirmHidden", json);

            string html = builder.ToString(TagRenderMode.Normal) + hidden.ToHtmlString();
            return new MvcHtmlString(html);
        }
    }
}
