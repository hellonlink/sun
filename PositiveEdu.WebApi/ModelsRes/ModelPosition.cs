using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PositiveEdu.WebApi.ModelsRes
{
    /// <summary>
    /// 招聘职位
    /// </summary>
    public class ModelPosition
    {
        public int id { get; set; }

        /// <summary>
        /// 职位名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 职位要求
        /// </summary>
        public string requirements { get; set; }

        /// <summary>
        /// 工作内容
        /// </summary>
        public string jobContent { get; set; }

        /// <summary>
        /// 汇报对象
        /// </summary>
        public string objects { get; set; }

        /// <summary>
        /// 联系方式
        /// </summary>
        public string contact { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? createTime { get; set; }

        /// <summary>
        /// 更新日期
        /// </summary>
        public DateTime? updateTime { get; set; }
    }
}