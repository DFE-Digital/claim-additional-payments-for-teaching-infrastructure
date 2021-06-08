using dqt.datalayer.Model;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Reflection;

namespace dqt.datalayer.Database
{
    public class DQTDataContext : DbContext
    {
        internal DbSet<QualifiedTeacher> QualifiedTeachers { get; set; }
        internal DbSet<QualifiedTeacherBackup> QualifiedTeachersBackup { get; set; }
        internal DbSet<DQTFileTransfer> DQTFileTransfer { get; set; }

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

                var server = jObject["Values"]["DatabaseServerName"].ToString();
                var database = jObject["Values"]["DatabaseName"].ToString();
                var username = jObject["Values"]["DatabaseUsername"].ToString();
                var password = jObject["Values"]["DatabasePassword"].ToString();

                var connectionstring = @$"Server={server};Database={database};Port=5432;User Id={username};Password={password};Ssl Mode=Require;";
                optionsBuilder.UseNpgsql(connectionstring,
                    buider =>
                    {
                        buider.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                    });
            }
#endif
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
        }
    }
}
