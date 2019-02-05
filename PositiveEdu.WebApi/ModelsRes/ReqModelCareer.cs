using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PositiveEdu.WebApi.ModelsRes
{
    /// <summary>
    /// 修改职业信息
    /// </summary>
    public class ReqModelCareer
    {
        /// <summary>
        /// 资质 （填写文字，非Id）
        /// </summary>
        public string qualification { get; set; }

        /// <summary>
        /// 开课期数
        /// </summary>
        public int? periods { get; set; }
    }
}