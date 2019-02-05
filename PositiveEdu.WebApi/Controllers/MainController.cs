using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PositiveEdu.WebApi.ModelsRes;

namespace PositiveEdu.WebApi.Controllers
{
    /// <summary>
    /// 首页
    /// </summary>
    [RoutePrefix("api/Main")]
    public class MainController : BaseApiController
    {
        /// <summary>
        /// 获取首页轮播图
        /// </summary>
        /// <returns>轮播图</returns>
        public IEnumerable<ModelBanner> Get()
        {
            var banner = DB.Banner.AsNoTracking().OrderBy(x => x.id).ToList();

            var model = banner.Select(x => new ModelBanner()
            {
                id = x.id,
                model = x.model,
                imgUrl = ToAbsoluteResourceUrl(x.imgUrl),
                Is_First = x.Is_First == "是",
                linkUrl = x.linkUrl,
            });

            return model;
        }

        /// <summary>
        /// 首页中段文字
        /// </summary>
        /// <returns>文字内容</returns>
        [Route("HomePageText"), HttpGet]
        public string HomePageText()
        {
            var intro = DB.Introduction.AsNoTracking().Where(x => x.Model == "HomePageText").FirstOrDefault();
            if (intro != null)
                return ContentToHtml(intro.Character);
            else
                return null;
        }

        /// <summary>
        /// 首页下方占位图
        /// </summary>
        /// <returns>占位图</returns>
        [Route("HomePageImage"), HttpGet]
        public IEnumerable<ModelPlaceholderImage> HomePageImage()
        {
            var phImg = DB.PlaceholderImage.AsNoTracking().Where(x => x.model.Contains("HomePage")).OrderBy(x => x.sort).ToList();

            var model = phImg.Select(x => new ModelPlaceholderImage(x)).ToList();
            return model;
        }

        /// <summary>
        /// 公司信息
        /// </summary>
        /// <returns>公司信息</returns>
        [Route("CompanyInfo"), HttpGet]
        public ModelCompanyInfo CompanyInfo()
        {
            var comInfo = DB.CompanyInfo.AsNoTracking().OrderBy(x => x.id).FirstOrDefault();
            if (comInfo != null)
            {
                var model = new ModelCompanyInfo(comInfo);
                return model;
            }
            else
                return new ModelCompanyInfo();
        }
    }
}
