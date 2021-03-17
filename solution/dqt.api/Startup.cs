using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using dqt.datalayer.Database;
using dqt.domain;
using dqt.datalayer.Repository;
using dqt.datalayer.Model;
using dqt.domain.Rollbar;
using dqt.domain.SFTPToBlob;
using dqt.api.Authorization;
using Rollbar.NetPlatformExtensions;
using Microsoft.Extensions.Logging;
using dqt.domain.Blob;

[assembly: FunctionsStartup(typeof(dqt.api.Startup))]
namespace dqt.api
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddDbContext<DQTDataContext>(options => options.UseNpgsql(GetConnStr()));
             builder.Services.AddTransient<IRollbarService, RollbarService>();
            builder.Services.AddTransient<IQualifiedTeachersService, QualifiedTeachersService>();
            builder.Services.AddTransient<IRepository<QualifiedTeacher>, QualifiedTeachersRepository>();
            builder.Services.AddTransient<ICSVProcessor, CSVProcessor>();
            builder.Services.AddTransient<ISFTPToBlobProcessor, SFTPToBlobProcessor>();
            builder.Services.AddTransient<IAuthorize, Authorize>();
            builder.Services.AddTransient<IBlobService, BlobService>();
        }

        private string GetConnStr()
        {
            var server = Environment.GetEnvironmentVariable("DatabaseServerName") ;
            var database = Environment.GetEnvironmentVariable("DatabaseName");
            var username = Environment.GetEnvironmentVariable("DatabaseUsername");
            var password = Environment.GetEnvironmentVariable("DatabasePassword");

            return @$" Server={server};Database={database};Port=5432;User Id={username};Password={password};Ssl Mode=Require;";
        }
    }
}
