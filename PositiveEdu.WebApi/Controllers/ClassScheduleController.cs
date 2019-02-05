using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PositiveEdu.WebApi.Controllers
{
    /// <summary>
    /// 课程表
    /// </summary>
    [RoutePrefix("api/ClassSchedule")]
    public class ClassScheduleController : BaseApiController
    {
        /// <summary>
        /// 家长课程
        /// </summary>
        /// <param name="mobile">是否移动端</param>
        /// <returns>html字符串</returns>
        [Route("Parents"), HttpGet]
        public string Parents(bool mobile = false)
        {
            var intro = DB.Introduction.AsNoTracking().Where(x => x.Model == "ClassScheduleParents").FirstOrDefault();
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
        /// 教师培训
        /// </summary>
        /// <param name="mobile">是否移动端</param>
        /// <returns>html字符串</returns>
        [Route("Teacher"), HttpGet]
        public string Teacher(bool mobile = false)
        {
            var intro = DB.Introduction.AsNoTracking().Where(x => x.Model == "ClassScheduleTeacher").FirstOrDefault();
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
        /// 讲师认证
        /// </summary>
        /// <param name="mobile">是否移动端</param>
        /// <returns>html字符串</returns>
        [Route("Certification"), HttpGet]
        public string Certification(bool mobile = false)
        {
            var intro = DB.Introduction.AsNoTracking().Where(x => x.Model == "ClassScheduleCertification").FirstOrDefault();
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
