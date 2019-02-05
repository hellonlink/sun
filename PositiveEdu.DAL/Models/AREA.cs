namespace PositiveEdu.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AREA")]
    public partial class AREA
    {
        [Key]
        public int AREA_ID { get; set; }

        [StringLength(100)]
        public string AREA_NAME { get; set; }

        public int? PARENT_ID { get; set; }

        public int? LEVEL_ID { get; set; }

        public DateTime? UPDATE_TIME { get; set; }

        public int? SORT_ID { get; set; }

        public int? DELIVRY_RATE { get; set; }
    }
}
