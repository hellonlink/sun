using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PositiveEdu.WebApi.ModelsRes;

namespace PositiveEdu.WebApi.Controllers
{
    /// <summary>
    /// 市场合作
    /// </summary>
    [RoutePrefix("api/Market")]
    public class MarketController : BaseApiController
    {
        /// <summary>
        /// 机构合作简介
        /// </summary>
        /// <param name="mobile">是否移动端</param>
        /// <returns>html字符串</returns>
        [Route("Cooperation"), HttpGet]
        public string Cooperation(bool mobile = false)
        {
            var intro = DB.Introduction.AsNoTracking().Where(x => x.Model == "cooperation").FirstOrDefault();
            if (intro != null)
            {
                if (mobile)
                    return ContentToHtml(intro.Character_MB);
                else
                    return ContentToHtml(intro.Character);
            }
            else
                return null;
        }

        /// <summary>
        /// 线上合作
        /// </summary>
        /// <param name="mobile">是否移动端</param>
        /// <returns>html字符串</returns>
        [Route("OnLine"), HttpGet]
        public string OnLine(bool mobile = false)
        {
            var intro = DB.Introduction.AsNoTracking().Where(x => x.Model == "onLine").FirstOrDefault();
            if (intro != null)
            {
                if (mobile)
                    return ContentToHtml(intro.Character_MB);
                else
                    return ContentToHtml(intro.Character);
            }
            else
                return null;
        }

        /// <summary>
        /// 线下合作
        /// </summary>
        /// <param name="mobile">是否移动端</param>
        /// <returns>html字符串</returns>
        [Route("Line"), HttpGet]
        public string Line(bool mobile = false)
        {
            var intro = DB.Introduction.AsNoTracking().Where(x => x.Model == "line").FirstOrDefault();
            if (intro != null)
            {
                if (mobile)
                    return ContentToHtml(intro.Character_MB);
                else
                    return ContentToHtml(intro.Character);
            }
            else
                return null;
        }

        /// <summary>
        /// 合作案例
        /// </summary>
        /// <param name="mobile">是否移动端</param>
        /// <returns>html字符串</returns>
        [Route("Case"), HttpGet]
        public string Case(bool mobile = false)
        {
            var intro = DB.Introduction.AsNoTracking().Where(x => x.Model == "case").FirstOrDefault();
            if (intro != null)
            {
                if (mobile)
                    return ContentToHtml(intro.Character_MB);
                else
                    return ContentToHtml(intro.Character);
            }
            else
                return null;
        }

        /// <summary>
        /// 全国合作联盟
        /// </summary>
        /// <param name="mobile">是否移动端</param>
        /// <returns>html字符串</returns>
        [Route("AllCase"), HttpGet]
        public string AllCase(bool mobile = false)
        {
            var intro = DB.Introduction.AsNoTracking().Where(x => x.Model == "allCase").FirstOrDefault();
            if (intro != null)
            {
                if (mobile)
                    return ContentToHtml(intro.Character_MB);
                else
                    return ContentToHtml(intro.Character);
            }
            else
                return null;
        }
    }
}
