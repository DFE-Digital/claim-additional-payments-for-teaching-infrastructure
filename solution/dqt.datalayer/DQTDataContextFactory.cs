using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace dqt.datalayer
{
    public class DQTDataContextFactory : IDesignTimeDbContextFactory<DQTDataContext>
    {
        public DQTDataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DQTDataContext>();
            optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("DatabaseConnectionString"));

            return new DQTDataContext(optionsBuilder.Options);
        }
    }
}
