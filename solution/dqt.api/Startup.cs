using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using dqt.api.Repository;
using dqt.datalayer.Database;

[assembly: FunctionsStartup(typeof(dqt.api.Startup))]
namespace dqt.api
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddDbContext<DQTDataContext>(options => 
                options.UseNpgsql(Environment.GetEnvironmentVariable("DatabaseConnectionString")));

            builder.Services.AddLogging();
            builder.Services.AddSingleton<IRepository, DQTRepository>();
        }
    }
}
