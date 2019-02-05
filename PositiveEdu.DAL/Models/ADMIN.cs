namespace PositiveEdu.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ADMIN")]
    public partial class ADMIN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ADMIN()
        {
            ADMIN_ROLE_RELATION = new HashSet<ADMIN_ROLE_RELATION>();
            Document = new HashSet<Document>();
            SiteMessage = new HashSet<SiteMessage>();
        }

        [Key]
        public int UUID { get; set; }

        public DateTime? CRATE_TIME { get; set; }

        public int? IS_DELETE { get; set; }

        public DateTime? UPDATE_TIME { get; set; }

        public string PASSWORD { get; set; }

        public string USER_NAME { get; set; }

        public string TOKEN { get; set; }

        public string REAL_NAME { get; set; }

        public string PHONE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ADMIN_ROLE_RELATION> ADMIN_ROLE_RELATION { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Document> Document { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SiteMessage> SiteMessage { get; set; }
    }
}
