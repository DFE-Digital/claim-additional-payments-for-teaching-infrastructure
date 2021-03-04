using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace dqt.datalayer
{
    public class DQTDataContext : DbContext
    {
        public DQTDataContext(DbContextOptions<DQTDataContext> options)
            : base(options)
        { }

        public DbSet<QualifiedTeacher> QualifiedTeachers { get; set; }
    }
}
