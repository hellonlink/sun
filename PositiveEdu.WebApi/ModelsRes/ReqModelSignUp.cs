using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PositiveEdu.WebApi.ModelsRes
{
    /// <summary>
    /// 用户注册信息
    /// </summary>
    public class ReqModelSignUp
    {
        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string ValidateCode { get; set; }
    }
}