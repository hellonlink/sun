using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PositiveEdu.DAL;

namespace PositiveEdu.WebApi.ModelsRes
{
    public class ModelSiteMessage
    {
        public string title { get; set; }

        public string message { get; set; }

        public DateTime? createdTime { get; set; }

        public ModelSiteMessage(SiteMessage item)
        {
            title = item.title;
            message = item.message;
            createdTime = item.createdTime;
        }
    }
}