using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PositiveEdu.DAL;

namespace PositiveEdu.WebApi.ModelsRes
{
    /// <summary>
    /// 课程详情
    /// </summary>
    public class ModelCourseDetails : ModelCourse
    {
        /// <summary>
        /// 课程介绍
        /// </summary>
        public string intro { get; set; }

        /// <summary>
        /// 相关课程
        /// </summary>
        public IEnumerable<ModelCourse> relatedCourses { get; set; }

        /// <summary>
        /// 是否已购买
        /// </summary>
        public bool Bought { get; set; }

        public ModelCourseDetails() { }

        public ModelCourseDetails(OnlineCourse item) : base(item)
        {
            intro = item.intro;
        }

        public ModelCourseDetails(OnlineCourse item, List<OnlineCourse> relatedItems) : base(item)
        {
            intro = item.intro;
            if (relatedItems != null)
                relatedCourses = relatedItems.Select(x => new ModelCourse(x)).ToList();
        }
    }
}