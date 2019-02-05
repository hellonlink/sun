using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PositiveEdu.Common
{
    public class ConstValue
    {
        public static class CourseDataType
        {
            public const string Video = "video";
            public const string TryVideo = "tryVideo";
            public const string Doc = "doc";
        }

        public static class OrderPayWay
        {
            public const string AliPay = "支付宝";
            public const string WeChat = "微信";
            public const string Offline = "线下";
        }

        public static class OrderStatus
        {
            public const string Unpaid = "未付款";
            public const string Paid = "已付款";

        }

        public static class SmsType
        {
            public const string SignUp = "SignUp";

            public const string Login = "Login";

            public const string Forget = "Forget";
        }

        public static class SmsStatus
        {
            public const string Pending = "Pending";

            public const string Used = "Used";

            public const string Invalid = "Invalid";
        }

        public const int SmsEffectiveTime = 15;
    }
}
