using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PositiveEdu.DAL;

namespace PositiveEdu.WebApi.ModelsRes
{
    /// <summary>
    /// 系统消息
    /// </summary>
    public class ModelMessage
    {
        /// <summary>
        /// 消息
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime? createTime { get; set; }

        public ModelMessage(MyMessage item)
        {
            message = item.message;
            createTime = item.createTime;
        }
    }
}