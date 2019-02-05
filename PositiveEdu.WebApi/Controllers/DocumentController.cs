using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PositiveEdu.WebApi.ModelsRes;
using PagedList;
using System.IO;
using System.Net.Http.Headers;
using System.Web;
using System.Text;
using PositiveEdu.DAL;

namespace PositiveEdu.WebApi.Controllers
{
    /// <summary>
    /// 资料下载
    /// </summary>
    [RoutePrefix("api/Document")]
    public class DocumentController : BaseApiController
    {
        /// <summary>
        /// 资料列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="page">页码</param>
        /// <returns>资料</returns>
        [Route("List/{page?}"), HttpGet]
        public ModelPagedList<ModelDocument> List([FromUri] string key = null, int page = 1)
        {
            int pageSize = 5;

            var query = DB.Document.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(key))
                query = query.Where(x => x.title.Contains(key) || x.intro.Contains(key));

            query = query.OrderBy(x => x.id);

            var result = query.ToPagedList(page, pageSize);

            var model = new ModelPagedList<ModelDocument>()
            {
                Pagination = new Pagination(result),
                List = result.Select(x => new ModelDocument(x, AuthCustomer)).ToList()
            };
            return model;
        }

        /// <summary>
        /// 资料下载
        /// </summary>
        /// <param name="id"></param>
        /// <returns>文件</returns>
        [Route("Download/{id}"), HttpGet]
        public HttpResponseMessage Download([FromUri] int id)
        {
            var doc = DB.Document.Where(x => x.id == id).FirstOrDefault();
            if (doc == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            if (doc.Authorize == true && AuthCustomer == null)
            {
                var res = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    Content = new StringContent("游客没有下载权限"),
                    ReasonPhrase = "Unauthorized"
                };
                throw new HttpResponseException(res);
            }

            //downCount++
            doc.downCount++;

            //MyDocument
            if (User.Identity.IsAuthenticated)
            {
                if (!DB.MyDocument.Any(x => x.customerId == AuthCustomer.CustomerId))
                {
                    MyDocument myDoc = new MyDocument()
                    {
                        customerId = AuthCustomer.CustomerId,
                        documentId = doc.id,
                        createdTime = DateTime.Now,
                    };
                    DB.MyDocument.Add(myDoc);
                }
            }
            DB.SaveChanges();


            string filePath = HttpContext.Current.Server.MapPath(doc.filePath);
            var fileStream = new MemoryStream(File.ReadAllBytes(filePath));
            var resp = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(fileStream)
            };
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            resp.Content.Headers.Add("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(doc.fileName, Encoding.UTF8).Replace("+", " "));
            return resp;
        }
    }
}
