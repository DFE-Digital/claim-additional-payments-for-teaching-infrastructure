using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using dqt.datalayer.Model;
using dqt.domain.FileTransfer;
using dqt.domain.Rollbar;
using dqt.domain.SFTPToBlob;
using Microsoft.Azure.WebJobs;

namespace dqt.api.Functions
{
    public class DQTCsvToBlob
    {
        private readonly IRollbarService _log;
        private readonly ISFTPToBlobProcessor _processor;
        private readonly IDQTFileTransferService _dqtFileTransferService;

        public DQTCsvToBlob(IRollbarService log, ISFTPToBlobProcessor processor, IDQTFileTransferService dqtFileTransferService)
        {
            _log = log;
            _processor = processor;
            _dqtFileTransferService = dqtFileTransferService;
        }

        [FunctionName("dqt-csv-to-blob")]
        public async Task Run([TimerTrigger("%SFTPScheduleTriggerTime%")] TimerInfo myTimer, ExecutionContext context)
        {
            try
            {
                _log.Info($"File transfer from SFTP to Blob started at {DateTime.Now}");

                await _processor.SaveCSVToBlobAsync(context);

                _log.Info($"File transfer from SFTP to Blob completed at {DateTime.Now}");

            }
            catch (Exception exception)
            {
                try
                {
                    await _dqtFileTransferService.AddDQTFileTransferDetails(DateTime.Now, DQTFileTransferStatus.Failure, exception.Message);
                }
                catch(Exception e)
                {
                    _log.Error(new Exception($"Failed to insert the last run error details", e));
                }

                _log.Error(new Exception($"File transfer from SFTP to blob failed at {DateTime.Now}", exception));

                throw exception;
            }
        }
    }
}