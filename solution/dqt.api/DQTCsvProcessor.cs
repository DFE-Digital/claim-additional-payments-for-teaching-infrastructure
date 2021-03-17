using System;
using System.IO;
using System.Threading.Tasks;
using dqt.domain;
using dqt.domain.Rollbar;
using Microsoft.Azure.WebJobs; 

namespace dqt.api
{
    public class DQTCsvProcessor
    {
        private readonly ICSVProcessor csvProcessor;
        private readonly IRollbarService log;

        public DQTCsvProcessor(ICSVProcessor csvProcessor, IRollbarService log)
        {
            this.csvProcessor = csvProcessor;
            this.log = log;
        }

        [FunctionName("dqt-csv-processor")]
        public async Task Run([BlobTrigger("dqt-cont/{name}", Connection = "AzureWebJobsStorage")] Stream csvBlob, string name)
        {
            try
            {
                log.Info($"Started processing DQT data from Blob 'dqt-cont'. \n Name:{name} \n Size: {csvBlob.Length} Bytes");

                await csvProcessor.SaveCSVDataToDatabase(csvBlob, name);
 

                log.Info($"Finished processing DQT data from Blob 'dqt-cont'. \n Name:{name} \n Size: {csvBlob.Length} Bytes");
            }
            catch (Exception ex)
            {
                var msg = $"Error processing DQT data from Blob 'dqt-cont'. \n Name:{name} \n Size: {csvBlob.Length} Bytes";
                log.Error(new Exception(msg, ex));

                throw ex;
            }
        }
    }
}
