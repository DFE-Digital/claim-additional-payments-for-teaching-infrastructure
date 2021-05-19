using System;
using System.IO;
using System.Threading.Tasks;
using dqt.datalayer.Model;
using dqt.domain;
using dqt.domain.FileTransfer;
using dqt.domain.Rollbar;
using Microsoft.Azure.WebJobs;

namespace dqt.api.Functions
{
    public class DQTCsvProcessor
    {
        private readonly ICSVProcessor csvProcessor;
        private readonly IRollbarService log;
        private readonly IDQTFileTransferService dqtFileTransferService;

        public DQTCsvProcessor(ICSVProcessor csvProcessor, IRollbarService log, IDQTFileTransferService dqtFileTransferService)
        {
            this.csvProcessor = csvProcessor;
            this.log = log;
            this.dqtFileTransferService = dqtFileTransferService;
        }

        [FunctionName("dqt-csv-processor")]
        public async Task Run([BlobTrigger("dqt-cont/{name}", Connection = "AzureWebJobsStorage")] Stream csvBlob, string name)
        {
            try
            {
                log.Info($"Started processing DQT data from Blob 'dqt-cont'. \n Name:{name} \n Size: {csvBlob.Length} Bytes");

                await csvProcessor.SaveCSVDataToDatabase(csvBlob, name);

                await dqtFileTransferService.AddDQTFileTransferDetails(DateTime.Now, DQTFileTransferStatus.Success, string.Empty);

                log.Info($"Finished processing DQT data from Blob 'dqt-cont'. \n Name:{name} \n Size: {csvBlob.Length} Bytes");
            }
            catch (Exception ex)
            {
                await dqtFileTransferService.AddDQTFileTransferDetails(DateTime.Now, DQTFileTransferStatus.Failure, ex.Message);

                var msg = $"Error processing DQT data from Blob 'dqt-cont'. \n Name:{name} \n Size: {csvBlob.Length} Bytes";
                log.Error(new Exception(msg, ex));

                throw ex;
            }
        }
    }
}
