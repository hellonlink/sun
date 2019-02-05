using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace PositiveEdu.Common
{
    public class WebFunctions
    {
        private static string resourceRoot = "http://115.159.153.91:801";
        private static string ResourceRoot
        {
            get
            {
                string cfgVal = System.Configuration.ConfigurationManager.AppSettings["ResourceRoot"];
                if (!string.IsNullOrEmpty(cfgVal))
                    return cfgVal;
                else
                    return resourceRoot;
            }
        }

        private static string resourceMapRoot = "~/ResourceFolder";
        public static string ResourceMapRoot
        { get { return resourceMapRoot; } }

        public static string ToAbsoluteResourceUrl(string url)
        {               
            return ResourceRoot + url;
        }

        public static string WrapToHtmlNewLine(string content)
        {
            return content.Replace("\r\n", "<br />").Replace("\n", "<br />");
        }

        public static string ContentToHtml(string content)
        {
            string imgPath = resourceMapRoot.TrimStart('~') + "/Images/";
            string html = Regex.Replace(content, imgPath, ResourceRoot + imgPath, RegexOptions.IgnoreCase);
            //html = WrapToHtmlNewLine(html);
            return html;
        }

        public static string GetCustomerPortrait(string head_porteait)
        {
            if (string.IsNullOrWhiteSpace(head_porteait))
                head_porteait = ResourceMapRoot.TrimStart('~') + "/Images/Default/head_porteait.png";

            return ToAbsoluteResourceUrl(head_porteait);
        }

        public static string GetRandomWords(int length)
        {
            string resaultnumber = "";
            string[] vList = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            Random vRandom = new Random();
            for (int i = 0; i < length; i++)
            {
                string vData = vList[vRandom.Next(vList.Length)];
                resaultnumber = resaultnumber + vData.ToString();
            }
            return resaultnumber;
        }

        public static string GetRandomNumber(int length)
        {
            string resaultnumber = "";
            string[] vList = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            Random vRandom = new Random();
            for (int i = 0; i < length; i++)
            {
                string vData = vList[vRandom.Next(vList.Length)];
                resaultnumber = resaultnumber + vData.ToString();
            }
            return resaultnumber;
        }

        public static string MapServerResourcePath(string path)
        {
            return HttpContext.Current.Server.MapPath(resourceMapRoot + path);
        }

        /// <summary>
        /// 根据给出的相对地址获取网站绝对地址
        /// </summary>
        /// <param name="localPath">相对地址</param>
        /// <returns>绝对地址</returns>
        public static string GetWebPath(string localPath)
        {
            string path = HttpContext.Current.Request.ApplicationPath;
            string thisPath;
            string thisLocalPath;
            //如果不是根目录就加上"/" 根目录自己会加"/"
            if (path != "/")
            {
                thisPath = path + "/";
            }
            else
            {
                thisPath = path;
            }
            if (localPath.StartsWith("~/"))
            {
                thisLocalPath = localPath.Substring(2);
            }
            else
            {
                return localPath;
            }
            return thisPath + thisLocalPath;
        }

        public static void CreateDirectory(string fullPath)
        {
            var directory = Path.GetDirectoryName(fullPath);

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
        }

        public static int StringToInt(string text)
        {
            int num;
            if (int.TryParse(text, out num))
                return num;
            else
                return 0;
        }

        public static int? StringToIntNullable(string text)
        {
            int num;
            if (int.TryParse(text, out num))
                return num;
            else
                return null;
        }

        public static decimal StringToDecimal(string text)
        {
            decimal num;
            if (decimal.TryParse(text, out num))
                return num;
            else
                return 0;
        }

        public static decimal? StringToDecimalNullable(string text)
        {
            decimal num;
            if (decimal.TryParse(text, out num))
                return num;
            else
                return null;
        }

        public static DateTime? StringToDateTimeNullable(string text)
        {
            DateTime dt;
            if (DateTime.TryParse(text, out dt))
                return dt;
            else
                return null;
        }
    }
}
