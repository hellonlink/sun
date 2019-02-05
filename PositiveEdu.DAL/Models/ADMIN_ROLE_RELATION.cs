namespace PositiveEdu.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ADMIN_ROLE_RELATION
    {
        [Key]
        public int UUID { get; set; }

        public DateTime? CREATE_TIME { get; set; }

        public int? IS_DELETE { get; set; }

        public DateTime? UPDATE_TIME { get; set; }

        public int? ADMIN_ID { get; set; }

        public int? ROLE_ID { get; set; }

        public virtual ADMIN ADMIN { get; set; }

        public virtual ROLE ROLE { get; set; }
    }
}
