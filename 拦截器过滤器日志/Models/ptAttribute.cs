using System;

namespace 拦截器过滤器日志.Models
{
    [AttributeUsage(AttributeTargets.All,AllowMultiple =true,Inherited =true)]
    public class ptAttribute : Attribute
    {



        private int length;

        public ptAttribute(int length)
        {
            this.length = length;
        }

        public int Length
        {
            get { return length; }
     
        }

    }
}