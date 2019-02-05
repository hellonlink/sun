using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PositiveEdu.Common;

namespace PositiveEdu.Admin.Controllers
{
    [RoutePrefix("Resource")]
    public class ResourceController : Controller
    {
        [Route("Picture/Upload"), HttpPost]
        public ActionResult PictureUpload()
        {
            bool isUploaded = false;
            string message = string.Empty;

            string url = string.Empty;

            try
            {
                var files = Request.Files;
                if (files.Count > 0)
                {
                    HttpPostedFileBase file = Request.Files[0];
                    Guid id = Guid.NewGuid();
                    var ext = System.IO.Path.GetExtension(file.FileName);
                    var newFileName = id.ToString().ToUpper().Replace("-", string.Empty) + ext;                      //  4B301ED0A3AF4D9BAE1B269805757C2B.jpg
                    var filePath = string.Format(ImageTemplate, DateTime.Now.ToString("yyyy-MM-dd"), newFileName);  //  /Images/2018-04-25/4B301ED0A3AF4D9BAE1B269805757C2B.jpg
                    var relativelyPath = WebFunctions.ResourceMapRoot + filePath;                                   //  ~/ResourceFolder/Images/2018-04-25/4B301ED0A3AF4D9BAE1B269805757C2B.jpg
                    var serverFilePath = Server.MapPath(relativelyPath);                                            //  D:\WebRoot\PositiveEdu.Resource\Images\2018-04-25\4B301ED0A3AF4D9BAE1B269805757C2B.jpg
                    WebFunctions.CreateDirectory(serverFilePath);
                    file.SaveAs(serverFilePath);

                    //返回给界面相对根路径，数据库存相对根路径。
                    url = relativelyPath.TrimStart('~');                                                            //  /ResourceFolder/Images/2018-04-25/4B301ED0A3AF4D9BAE1B269805757C2B.jpg

                    isUploaded = true;
                    message = "上传完成";
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return Json(new { isUploaded = isUploaded, message = message, content = url }, "application/json");
        }

        private string ImageTemplate
        {
            get
            {
                string template = "/Images/{0}/{1}";
                return template;
            }
        }


        [Route("Attachment/Upload"), HttpPost]
        public ActionResult FileUpload()
        {
            List<dynamic> atts = new List<dynamic>();
            string message = string.Empty;

            try
            {
                var files = Request.Files;

                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase file = Request.Files[i];

                    Guid id = Guid.NewGuid();
                    var ext = System.IO.Path.GetExtension(file.FileName);
                    var newFileName = id.ToString().ToUpper().Replace("-", string.Empty) + ext;
                    var filePath = string.Format(AttachmentTemplate, DateTime.Now.ToString("yyyy-MM-dd"), newFileName);
                    var relativelyPath = WebFunctions.ResourceMapRoot + filePath;
                    var serverFilePath = Server.MapPath(relativelyPath);
                    WebFunctions.CreateDirectory(serverFilePath);
                    file.SaveAs(serverFilePath);

                    var att = new ModelFile
                    {
                        id = id.ToString(),
                        fileName = file.FileName,
                        size = file.ContentLength,
                        url = relativelyPath.TrimStart('~'),
                    };
                    
                    atts.Add(att);
                }

                message = "上传完成";
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return Json(new { isUploaded = atts.Count > 0, message = message, content = atts }, "application/json");
        }

        private string AttachmentTemplate
        {
            get
            {
                string template = "/Document/{0}/{1}";
                return template;
            }
        }

    }
}