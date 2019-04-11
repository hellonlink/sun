namespace PositiveEdu.DAL
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Document")]
    public partial class Document
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Document()
        {
            MyDocument = new HashSet<MyDocument>();
        }

        public int id { get; set; }

        public string title { get; set; }

        public string intro { get; set; }

        public Guid? saveId { get; set; }

        public string filePath { get; set; }

        public string extName { get; set; }

        public string fileName { get; set; }

        public int? fileSize { get; set; }

        public int? downCount { get; set; }

        public DateTime? createdTime { get; set; }

        public int? adminId { get; set; }

        public virtual ADMIN ADMIN { get; set; }

        public bool? Authorize { get; set; }
        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MyDocument> MyDocument { get; set; }
    }
}
