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
    /// 关于我们
    /// </summary>
    [RoutePrefix("api/AboutUs")]
    public class AboutUsController : BaseApiController
    {
        /// <summary>
        /// 讲师团队
        /// </summary>
        /// <param name="mobile">是否移动端</param>
        /// <returns>html字符串</returns>
        [Route("Team"), HttpGet]
        public string Team(bool mobile = false)
        {
            var intro = DB.Introduction.AsNoTracking().Where(x => x.Model == "team").FirstOrDefault();
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
        /// 讲师团队（导师）
        /// </summary>
        /// <returns>老师信息</returns>
        [Route("Team/Teacher1"), HttpGet]
        public IEnumerable<ModelTeacher> TeamTeacher1()
        {
            return GetTeachers("导师");
        }

        /// <summary>
        /// 讲师团队（讲师）
        /// </summary>
        /// <returns>老师信息</returns>
        [Route("Team/Teacher2"), HttpGet]
        public IEnumerable<ModelTeacher> TeamTeacher2()
        {
            return GetTeachers("讲师");
        }

        /// <summary>
        /// 讲师团队（合作讲师）
        /// </summary>
        /// <returns>老师信息</returns>
        [Route("Team/Teacher3"), HttpGet]
        public IEnumerable<ModelTeacher> TeamTeacher3()
        {
            return GetTeachers("合作讲师");
        }

        /// <summary>
        /// 讲师团队（国外导师）
        /// </summary>
        /// <returns>老师信息</returns>
        [Route("Team/Teacher4"), HttpGet]
        public IEnumerable<ModelTeacher> TeamTeacher4()
        {
            return GetTeachers("国外导师");
        }

        private List<ModelTeacher> GetTeachers(string type)
        {
            var teacher = DB.Teacher.AsNoTracking().Where(x => x.type == type && x.show == 1).OrderBy(x => x.sort).ToList();
            var model = teacher.Select(x => new ModelTeacher(x)).ToList();
            return model;
        }

        /// <summary>
        /// 历史沿革
        /// </summary>
        /// <param name="mobile">是否移动端</param>
        /// <returns>html字符串</returns>
        [Route("History"), HttpGet]
        public string History(bool mobile = false)
        {
            var intro = DB.Introduction.AsNoTracking().Where(x => x.Model == "history").FirstOrDefault();
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
        /// 使命愿景
        /// </summary>
        /// <param name="mobile">是否移动端</param>
        /// <returns>html字符串</returns>
        [Route("Wish"), HttpGet]
        public string Wish(bool mobile = false)
        {
            var intro = DB.Introduction.AsNoTracking().Where(x => x.Model == "wish").FirstOrDefault();
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
        /// 加入我们
        /// </summary>
        /// <returns>招聘职位</returns>
        [Route("JoinUs"), HttpGet]
        public IEnumerable<ModelPosition> JoinUs()
        {
            var positions = DB.position.AsNoTracking().OrderBy(x => x.createTime).ToList();
            var model = positions.Select(x => new ModelPosition()
            {
                id = x.id,
                name = x.name,
                jobContent = WrapToHtmlNewLine(x.jobContent),
                objects = x.objects,
                contact = x.contact,
                requirements = WrapToHtmlNewLine(x.requirements),
                createTime = x.createTime,
                updateTime = x.updateTime,
            });

            return model;
        }
    }
}