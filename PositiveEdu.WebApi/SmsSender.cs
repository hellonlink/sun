using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace PositiveEdu.WebApi
{
    public class SmsSender
    {
        private const string URL = "https://sms.253.com/msg/send";
        private const string un = "N642605_N6025915";
        private const string pw = "SXv7iCFl9Ve987";

        public static string Send(string mobile, string content)
        {
            string URLContent = URL + "?un=" + un + "&pw=" + pw + "&phone=" + mobile + "&msg=" + content;
            HttpWebRequest myReq = (HttpWebRequest)HttpWebRequest.Create(URLContent);
            myReq.Timeout = 12000;
            HttpWebResponse HttpWResp = (HttpWebResponse)myReq.GetResponse();
            Stream myStream = HttpWResp.GetResponseStream();
            StreamReader sr = new StreamReader(myStream, Encoding.Default);
            string result = sr.ReadToEnd();
            return result;
        }
    }
}