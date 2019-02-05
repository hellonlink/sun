using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PositiveEdu.DAL;
using PositiveEdu.Common;

namespace PositiveEdu.WebApi.ModelsRes
{
    /// <summary>
    /// 课程评论
    /// </summary>
    public class ModelReview
    {
        public int id { get; set; }

        /// <summary>
        /// 评论
        /// </summary>
        public string comment { get; set; }

        /// <summary>
        /// 点赞（有用）数
        /// </summary>
        public int? up { get; set; }

        /// <summary>
        /// 发布日期
        /// </summary>
        public DateTime? createdTime { get; set; }

        /// <summary>
        /// 发布人手机号
        /// </summary>
        public string customerMobile { get; set; }

        /// <summary>
        /// 发布人头像
        /// </summary>
        public string customerImg { get; set; }

        public ModelReview(Review item)
        {
            id = item.id;
            comment = item.comment;
            up = item.up;
            createdTime = item.createdTime;
            if (item.Customer != null)
            {
                customerMobile = item.Customer.MOBILE;
                customerImg = WebFunctions.GetCustomerPortrait(item.Customer.HEAD_PORTEAIT);
            }
        }
    }
}