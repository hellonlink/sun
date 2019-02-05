using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace PositiveEdu.WebApi.Pay.Ali
{
    public class AliPayConfig
    {
        public static string alipay_public_key = @"MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAtxRK6rsZ/ecvPuUv/8vBdgxOQnVjICwi/Pu+mRxpeSG/im2ME5QYu4fJPMPDwtLoM28qKRV8dEhMdIq0g9N01K4ukXKsVlOGmOFv+Ixk/dR7wG+Nc1uY+O4DzbA0hwFUBejT/svZKDQTeskXZPc0fJ8Xc+dluA/5MzZO/p1QOVrKw5yiHnk+sWNTaeTY0EoalK3Brw1/mzfdgE9mL+eaDnG7nGeML+iXiNEyCgwwRuVikHTTMT9tnrAsXzJ0MKbhOS6dQniKJvEkkDHtq1wO9QupJc1ASjeYB108IxVUNaFyG5F93Qb3dbxGC93U0JL1yaHsNnfHcOiDEQjJnSvXgQIDAQAB";


        //这里要配置没有经过的原始私钥

        //开发者私钥
        public static string merchant_private_key = @"MIIEogIBAAKCAQEA1bQqEd6iWhN7fFH/6EmMTVljaG9p+RBBvlJj4DsHSeTtzTO4ZfemCE/b56C/uzWMsFk4DHl861G8AK7GZFjxH8PgwikScvZJmAQ1BWiprgIusIIoOjSO1kkjYrNkT7okHljVF6R3ZEYjfAx4/Y+jIxghmtKIif6tUGo8AZP7ROs+1+EyGAH4whRvNBcAln2T1asc+iBQMhMe4VTisPHXOsfuPyQ1nVJ1erqHx680B5VBFXPHCGgd9om5lo9UG3sE17Pi0TMunA9NpCyT7n4QznE9debvO6OwVpxlMcJpxecSfdXEfl1j3zyDXstXHBxUFJUBut2zFLg6IajsQUopUQIDAQABAoIBAEoNkn56vZulIKdl6t3djqbHEPGBmiFahs2i00VCbzYmBFmVUJLVDE3ZDoTnaUlANy5s3EVGXAlXW2SJK+pd/8BYKixd+yynN71bx9vF9vZ7fx5fG/1/dPfAhGiRbXNLN3Tkp4L31P+EZot37v0+pvNkS1DRH8th20EAbybE1s98avvAvJGbqlDE0YE8Sf9oSpR0m5CjP84XqFgsgqghiG/AtVhfoeM4zltd35xkyYoCa9REIwpXUdGHFXanqZ8QkrLWGYn8pkacyQRmLp0F9zAaIDmOCOTcSOwOGAJzNA79c7lWnJt8UW5FLLcM+SbCTEYFAUUM/mbZoSz6AM0Vd6ECgYEA9+W7YYDXVacEe6kbWrhpb08V5YCeJKpjzabrCoDcqbbLX4tFGmleYoMm1zorozZJ+b4xp8ywZivbS/XO7EBsOhfd7EerWb9d45VzwjmbJ6I5wmkdQSjUwmpBRTqj0QtUuXi2PYlj0ue9O73FCuKe3VeyjQah/v2lODrthsHeBf0CgYEA3LBRrxtp5k7QKwafcmxEgIvppeL87F2wdlEbVT0LRipbUyYS+svDuDLM5e/In0hVM0pc5BHHloFsC0/Bu5SDCZEXOhqhAu4bw4xgAKHIu8nh8NRPL+xgzIHudOL+3q8llPstTYiLfZgIxX72YOyMvN9GKJGIhVsXsk9lUlg8ZuUCgYBPMQyTMV2Y9ynvdy5N5OuJq3ILRvM+M1B+ufwL9u6HKOX/N7ZzoYWNYuhdiS+3i/hF2AR03mbbvNbphTT9M/de95Dlkl+i5AdSVMQOH+j42FIrUPkPf8O398myG/GJLl16gp11OxawoWdKVm1D9PFbNFDNzHijxNOrCugmTHho4QKBgBBrXeh1Va+49Wv+FibhP0eBIWUiOkcrwVFMP4hb/la+GAVbs9XNC0bNIMmKyZZwP0aNpdOiLwNXpb5BtNxL9GPl06CTEp+xL4ehDhN99f1iy9irMykRoTIWCLPRn7zZsyYeegRmwFob0atAAvy7HM2AnyutiEbDvEOb/KHfxr6xAoGAJs2uSshR9h6iiO0rpY+x2M8/7HTv/QOs4GjCrmY3ZqurUpbUO8NIGQn/A/VtMFgUmto2WKEpD7Pc6VP/L1mkj//gb6T6b1OjMR8Nn/HX4AiFGTEL7g5xyKAbe+lWbqlHLGn5FGeDHdalIHN6kOGiEWzz+yoMzI0dbCMYeYZa1G8=";

        //开发者公钥
        public static string merchant_public_key = @"MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA1bQqEd6iWhN7fFH/6EmMTVljaG9p+RBBvlJj4DsHSeTtzTO4ZfemCE/b56C/uzWMsFk4DHl861G8AK7GZFjxH8PgwikScvZJmAQ1BWiprgIusIIoOjSO1kkjYrNkT7okHljVF6R3ZEYjfAx4/Y+jIxghmtKIif6tUGo8AZP7ROs+1+EyGAH4whRvNBcAln2T1asc+iBQMhMe4VTisPHXOsfuPyQ1nVJ1erqHx680B5VBFXPHCGgd9om5lo9UG3sE17Pi0TMunA9NpCyT7n4QznE9debvO6OwVpxlMcJpxecSfdXEfl1j3zyDXstXHBxUFJUBut2zFLg6IajsQUopUQIDAQAB";

        //应用ID
        public static string appId = "2018042360071007";

        //合作伙伴ID：partnerID
        public static string pid = "2088221732518696";


        //支付宝网关
        public static string serverUrl = "https://openapi.alipay.com/gateway.do";
        public static string mapiUrl = "https://mapi.alipay.com/gateway.do";
        public static string monitorUrl = "http://mcloudmonitor.com/gateway.do";

        //编码，无需修改
        public static string charset = "utf-8";
        //签名类型，支持RSA2（推荐！）、RSA
        //public static string sign_type = "RSA2";
        public static string sign_type = "RSA2";
        //版本号，无需修改
        public static string version = "1.0";

        //回调通知地址
        public static string notifyUrl = "http://115.159.153.91:801/PayNotify/AliPay";

        /// <summary>
        /// 公钥文件类型转换成纯文本类型
        /// </summary>
        /// <returns>过滤后的字符串类型公钥</returns>
        public static string getMerchantPublicKeyStr()
        {
            StreamReader sr = new StreamReader(merchant_public_key);
            string pubkey = sr.ReadToEnd();
            sr.Close();
            if (pubkey != null)
            {
                pubkey = pubkey.Replace("-----BEGIN PUBLIC KEY-----", "");
                pubkey = pubkey.Replace("-----END PUBLIC KEY-----", "");
                pubkey = pubkey.Replace("\r", "");
                pubkey = pubkey.Replace("\n", "");
            }
            return pubkey;
        }

        /// <summary>
        /// 私钥文件类型转换成纯文本类型
        /// </summary>
        /// <returns>过滤后的字符串类型私钥</returns>
        public static string getMerchantPriveteKeyStr()
        {
            StreamReader sr = new StreamReader(merchant_private_key);
            string pubkey = sr.ReadToEnd();
            sr.Close();
            if (pubkey != null)
            {
                pubkey = pubkey.Replace("-----BEGIN PUBLIC KEY-----", "");
                pubkey = pubkey.Replace("-----END PUBLIC KEY-----", "");
                pubkey = pubkey.Replace("\r", "");
                pubkey = pubkey.Replace("\n", "");
            }
            return pubkey;
        }
    }
}