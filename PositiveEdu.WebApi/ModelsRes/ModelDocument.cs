using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PositiveEdu.DAL;

namespace PositiveEdu.WebApi.ModelsRes
{
    public class ModelDocument
    {
        public int id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        public string intro { get; set; }

        /// <summary>
        /// 扩展名
        /// </summary>
        public string extName { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public string sizeMB { get; set; }

        /// <summary>
        /// 根据当前用户身份判断是否可以下载
        /// </summary>
        public bool CanDownload { get; set; }

        public ModelDocument() { }

        public ModelDocument(Document doc, AuthCustomer authCustomer)
        {
            id = doc.id;
            title = doc.title;
            intro = doc.intro;
            extName = doc.extName;
            sizeMB = ((double)doc.fileSize / 1024 / 1024).ToString("f2");

            if (doc.Authorize == true && authCustomer == null)
                CanDownload = false;
            else
                CanDownload = true;
        }
    }
}