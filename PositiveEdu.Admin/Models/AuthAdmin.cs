using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PositiveEdu.DAL;

namespace PositiveEdu.Admin
{
    public class AuthAdmin
    {
        public int AdminId { get; set; }

        public string UserName { get; set; }

        public string Mobile { get; set; }

        public string RealName { get; set; }

        public string FirstRoleName { get; set; }

        public List<int> FunctionsId { get; set; }
    }
}