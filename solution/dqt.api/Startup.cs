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
using dqt.domain.Blob;
using dqt.domain.FileTransfer;
using dqt.domain.QTS;

[assembly: FunctionsStartup(typeof(dqt.api.Startup))]
namespace dqt.api
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddDbContext<DQTDataContext>(options => options.UseNpgsql(GetConnStr()));
            builder.Services.AddTransient<IRollbarService, RollbarService>();
            builder.Services.AddTransient<IDQTFileTransferService, DQTFileTransferService>();
            builder.Services.AddTransient<IQualifiedTeachersService, QualifiedTeachersService>();
            builder.Services.AddTransient<IRepository<QualifiedTeacher>, QualifiedTeachersRepository>();
            builder.Services.AddTransient<IRepository<DQTFileTransfer>, DQTFileTransferRepository>();
            builder.Services.AddTransient<ICSVProcessor, CSVProcessor>();
            builder.Services.AddTransient<ISFTPToBlobProcessor, SFTPToBlobProcessor>();
            builder.Services.AddTransient<IAuthorize, Authorize>();
            builder.Services.AddTransient<IBlobService, BlobService>();
            builder.Services.AddTransient<IConfigSettings, ConfigSettings>();
            builder.Services.AddLogging();
        }

        private string GetConnStr()
        {
            var configSettings = new ConfigSettings();
            return @$" Server={configSettings.DatabaseServerName};Database={configSettings.DatabaseName};Port=5432;User Id={configSettings.DatabaseUsername};Password={configSettings.DatabasePassword};Ssl Mode=Require;";
        }
    }
}
