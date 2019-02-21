using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Web.Mvc;
using 拦截器过滤器日志.Models;

namespace 拦截器过滤器日志.Controllers
{
    public class wjController : Controller
    {
        // GET: wj
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string name, string password)
        {
            Users u = new Users();
            u.Name = name;
            u.Password = password;
            //序列化
    


            return View();
        }



        private static void NewMethod4()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(@"App_Data\MyFile.bin", FileMode.Open,
            FileAccess.Read, FileShare.Read);
            Users obj = (Users)formatter.Deserialize(stream);


            stream.Close();
        }

        private static void NewMethod3(Users s)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(@"App_Data\MyFile.bin", FileMode.Create,
            FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, s);
            stream.Close();
        }

    }
}