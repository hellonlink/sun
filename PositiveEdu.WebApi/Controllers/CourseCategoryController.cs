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
    /// 课程分类
    /// </summary>
    [RoutePrefix("api/CourseCategory")]
    public class CourseCategoryController : BaseApiController
    {
        /// <summary>
        /// 学校讲师班
        /// </summary>
        /// <param name="mobile">是否移动端</param>
        /// <returns>html字符串</returns>
        [Route("Parents"), HttpGet]
        public string Parents(bool mobile = false)
        {
            var intro = DB.Introduction.AsNoTracking().Where(x => x.Model == "Parents").FirstOrDefault();
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
        /// 家长讲师班
        /// </summary>
        /// <param name="mobile">是否移动端</param>
        /// <returns>html字符串</returns>
        [Route("CertifiedParents"), HttpGet]
        public string CertifiedParents(bool mobile = false)
        {
            var intro = DB.Introduction.AsNoTracking().Where(x => x.Model == "CertifiedParents").FirstOrDefault();
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
        /// 指导课家长课堂
        /// </summary>
        /// <param name="mobile">是否移动端</param>
        /// <returns>html字符串</returns>
        [Route("Tutorial"), HttpGet]
        public string Tutorial(bool mobile = false)
        {
            var intro = DB.Introduction.AsNoTracking().Where(x => x.Model == "Tutorial").FirstOrDefault();
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
        /// 我是家长
        /// </summary>
        /// <param name="mobile">是否移动端</param>
        /// <returns>html字符串</returns>
        [Route("ImParents"), HttpGet]
        public string ImParents(bool mobile = false)
        {
            var intro = DB.Introduction.AsNoTracking().Where(x => x.Model == "ImParents").FirstOrDefault();
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
        /// 我是讲师
        /// </summary>
        /// <param name="mobile">是否移动端</param>
        /// <returns>html字符串</returns>
        [Route("ImLecturer"), HttpGet]
        public string ImLecturer(bool mobile = false)
        {
            var intro = DB.Introduction.AsNoTracking().Where(x => x.Model == "ImLecturer").FirstOrDefault();
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
        /// 我是老师
        /// </summary>
        /// <param name="mobile">是否移动端</param>
        /// <returns>html字符串</returns>
        [Route("ImTeacher"), HttpGet]
        public string ImTeacher(bool mobile = false)
        {
            var intro = DB.Introduction.AsNoTracking().Where(x => x.Model == "ImTeacher").FirstOrDefault();
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
        /// 经典普及班
        /// </summary>
        /// <param name="mobile">是否移动端</param>
        /// <returns>html字符串</returns>
        [Route("ClassicalClass"), HttpGet]
        public string ClassicalClass(bool mobile = false)
        {
            var intro = DB.Introduction.AsNoTracking().Where(x => x.Model == "ClassicalClass").FirstOrDefault();
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