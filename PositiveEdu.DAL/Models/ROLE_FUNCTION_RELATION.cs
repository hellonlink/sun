namespace PositiveEdu.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ROLE_FUNCTION_RELATION
    {
        [Key]
        public int UUID { get; set; }

        public DateTime? CREATE_TIME { get; set; }

        public int? IS_DELETE { get; set; }

        public DateTime? UPDATE_TIME { get; set; }

        public int? FUNCTION_ID { get; set; }

        public int? ROLE_ID { get; set; }

        public virtual ROLE ROLE { get; set; }

        public virtual ROLE_FUNCTIONS ROLE_FUNCTIONS { get; set; }
    }
}
