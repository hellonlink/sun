using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PositiveEdu.DAL;
using PositiveEdu.Common;

namespace PositiveEdu.WebApi.ModelsRes
{
    /// <summary>
    /// 公司信息
    /// </summary>
    public class ModelCompanyInfo
    {
        /// <summary>
        /// 电话
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 博客
        /// </summary>
        public string Blog { get; set; }

        /// <summary>
        /// 微信
        /// </summary>
        public string Chat { get; set; }

        /// <summary>
        /// 二维码1
        /// </summary>
        public string QR1 { get; set; }

        /// <summary>
        /// 二维码2
        /// </summary>
        public string QR2 { get; set; }

        public ModelCompanyInfo() { }

        public ModelCompanyInfo(CompanyInfo item)
        {
            Tel = item.Tel;
            Email = item.Email;
            Address = item.Address;
            Blog = item.Blog;
            Chat = item.Chat;
            QR1 = WebFunctions.ToAbsoluteResourceUrl(item.QR1);
            QR2 = WebFunctions.ToAbsoluteResourceUrl(item.QR2);
        }
    }
}