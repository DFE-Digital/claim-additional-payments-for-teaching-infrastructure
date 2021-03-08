using dqt.datalayer.Model;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Reflection;

namespace dqt.datalayer.Database
{
    public class DQTDataContext : DbContext
    {
        internal DbSet<QualifiedTeacher> QualifiedTeachers { get; set; }

        public DQTDataContext() { }
        public DQTDataContext(DbContextOptions options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#if DEBUG
            if (!optionsBuilder.IsConfigured)
            {
                var localSettingsFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\local.settings.json";
                var text = File.ReadAllText(localSettingsFilePath);
                var jObject = JObject.Parse(text);
                var connectionstring = jObject["Values"]["DatabaseConnectionString"].ToString();
                optionsBuilder.UseNpgsql(connectionstring);
            }
#endif
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns(); 
        }
    }
}
