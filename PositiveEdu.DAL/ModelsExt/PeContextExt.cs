using System.Data.Entity;

namespace PositiveEdu.DAL
{
    public partial class PeContext : DbContext
    {
        public virtual DbSet<DictData> DictData { get; set; }
        public virtual DbSet<CourseCategoryA> CourseCategoryA { get; set; }
        public virtual DbSet<CourseCategoryB> CourseCategoryB { get; set; }
        public virtual DbSet<QualificationDict> QualificationDict { get; set; }
    }
}
