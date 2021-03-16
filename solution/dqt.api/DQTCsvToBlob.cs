using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using dqt.domain.Rollbar;
using dqt.domain.SFTPToBlob;
using Microsoft.Azure.WebJobs;

namespace dqt.api
{
    public class DQTCsvToBlob
    {
        private readonly IRollbarService _log;
        private readonly ISFTPToBlobProcessor _processor;

        public DQTCsvToBlob(IRollbarService log, ISFTPToBlobProcessor processor)
        {
            _log = log;
            _processor = processor;
            _log.Configure(Environment.GetEnvironmentVariable("DQTSFTPRollbarEnvironment"));
        }

        [FunctionName("dqt-csv-to-blob")]
        public async Task Run([TimerTrigger("%SFTPScheduleTriggerTime%")] TimerInfo myTimer, ExecutionContext context)
        {
            try
            {
                _log.Info($"dqt-csv-to-blob started at {DateTime.Now}");

                await _processor.SaveCSVToBlobAsync(context);

                _log.Info($"dqt-csv-to-blob completed at {DateTime.Now}");

            }
            catch (Exception exception)
            {
                _log.Error(new Exception($"SFTP Trigget failed at {DateTime.Now}", exception));
                throw exception;
            }
        }
    }
}