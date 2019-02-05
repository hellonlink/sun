using System;
using System.Collections.Generic;
using System.Web;

namespace PositiveEdu.WebApi.Pay.Wx
{
    public class WxPayException : Exception 
    {
        public WxPayException(string msg) : base(msg) 
        {

        }
     }
}