using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PositiveEdu.WebApi.Controllers
{
    /// <summary>
    /// 接口样例
    /// </summary>
    [RoutePrefix("api/Sample")]
    public class SampleController : BaseApiController
    {
        /// <summary>
        /// 取得数组
        /// </summary>
        /// <returns></returns>
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// 取得字符串 带参数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// 提交数组 POST
        /// </summary>
        /// <param name="value"></param>
        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        /// <summary>
        /// 删除 带参数
        /// </summary>
        /// <param name="id"></param>
        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        /// <summary>
        /// 测试认证 需要认证才能取得返回值
        /// </summary>
        /// <returns>当前用户基本信息</returns>
        [Authorize, HttpGet, Route("AuthValue")]
        public AuthCustomer GetAuthValue()
        {
            return AuthCustomer;
        }
    }
}