namespace PositiveEdu.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class News
    {
 

        public int id { get; set; }

        public string title { get; set; }

        public string mainContent { get; set; }

        public DateTime? publishDate { get; set; }

        public DateTime? createdTime { get; set; }

        public DateTime? updatedTime { get; set; }

        public int? createdUserId { get; set; }

        public int? updatedUserId { get; set; }
    }
}
