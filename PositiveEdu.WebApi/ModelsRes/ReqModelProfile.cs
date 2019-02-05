using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PositiveEdu.WebApi.ModelsRes
{
    /// <summary>
    /// 修改用户资料
    /// </summary>
    public class ReqModelProfile
    {
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 性别 男/女
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 居住地 - 省
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 居住地 - 市
        /// </summary>
        public string Market { get; set; }

        /// <summary>
        /// 个人简介
        /// </summary>
        public string Intro { get; set; }
    }
}