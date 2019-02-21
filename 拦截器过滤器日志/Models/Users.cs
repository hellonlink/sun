using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace 拦截器过滤器日志.Models
{
    [Serializable]
    public class Users
    {
        [pt(20)]
        [DisplayName("姓名")]
        public string Name { get; set; }
        [DisplayName("年龄")]
        public int Age { get; set; }
        [DisplayName("性别")]
        public string Sex { get; set; }
        [DisplayName("密码  ")]
        public string Password { get; set; }
    }
}