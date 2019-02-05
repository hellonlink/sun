using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PositiveEdu.DAL;
using PositiveEdu.Common;

namespace PositiveEdu.WebApi.ModelsRes
{
    /// <summary>
    /// 会员信息
    /// </summary>
    public class ModelCustomer
    {
        public int id { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string customerImg { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string mobile { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string realName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string sex { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public string birthday { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        public string province { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        public string market { get; set; }

        /// <summary>
        /// 个人简介
        /// </summary>
        public string intro { get; set; }

        /// <summary>
        /// 资质
        /// </summary>
        public string qualification { get; set; }

        /// <summary>
        /// 开课期数
        /// </summary>
        public int? periods { get; set; }

        public ModelCustomer() { }

        public ModelCustomer(Customer item)
        {
            id = item.CUSTOMER_ID;
            mobile = item.MOBILE;
            realName = item.REAL_NAME;
            sex = item.SEX;
            birthday = item.BIRTHDAY.HasValue ? item.BIRTHDAY.Value.ToString("yyyy-MM-dd") : string.Empty;
            province = item.PROVINCE;
            market = item.MARKET;
            intro = item.intro;
            qualification = item.qualification;
            periods = item.periods;
            customerImg = WebFunctions.GetCustomerPortrait(item.HEAD_PORTEAIT);
        }

    }
}