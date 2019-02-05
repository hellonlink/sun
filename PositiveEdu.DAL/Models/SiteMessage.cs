namespace PositiveEdu.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SiteMessage")]
    public partial class SiteMessage
    {
        public int id { get; set; }

        public string title { get; set; }

        public string message { get; set; }

        public DateTime? createdTime { get; set; }

        public int? adminId { get; set; }

        public virtual ADMIN ADMIN { get; set; }
    }
}
