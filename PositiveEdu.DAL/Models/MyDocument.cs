namespace PositiveEdu.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MyDocument")]
    public partial class MyDocument
    {
        public int id { get; set; }

        public int? documentId { get; set; }

        public int? customerId { get; set; }

        public DateTime? createdTime { get; set; }



        public virtual Document Document { get; set; }
    }
}
