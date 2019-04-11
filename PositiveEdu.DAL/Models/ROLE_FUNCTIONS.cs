namespace PositiveEdu.DAL
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ROLE_FUNCTIONS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ROLE_FUNCTIONS()
        {
            ROLE_FUNCTION_RELATION = new HashSet<ROLE_FUNCTION_RELATION>();
        }

        [Key]
        public int UUID { get; set; }

        public DateTime? CREATE_TIME { get; set; }

        public int? IS_DELETE { get; set; }

        public DateTime? UPDATE_TIME { get; set; }

        [StringLength(50)]
        public string NAME { get; set; }

        public int? PARENT { get; set; }

        public int? TYPE { get; set; }

        [StringLength(50)]
        public string URL { get; set; }

        public int? SORT { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }
        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ROLE_FUNCTION_RELATION> ROLE_FUNCTION_RELATION { get; set; }
    }
}
