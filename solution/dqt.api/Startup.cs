using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using dqt.datalayer.Database;
using dqt.domain;
using dqt.datalayer.Repository;
using dqt.datalayer.Model;

[assembly: FunctionsStartup(typeof(dqt.api.Startup))]
namespace dqt.api
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddDbContext<DQTDataContext>(options => 
                options.UseNpgsql(Environment.GetEnvironmentVariable("DatabaseConnectionString")));

            builder.Services.AddTransient<IRollbarService, RollbarService>();
            builder.Services.AddTransient<IQualifiedTeachersService, QualifiedTeachersService>();
            builder.Services.AddTransient<IRepository<QualifiedTeacher>, QualifiedTeachersRepository>();
        }
    }
}
