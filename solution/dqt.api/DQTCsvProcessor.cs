using System.IO;
using System.Threading.Tasks;
using dqt.domain;
using dqt.domain.Rollbar;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

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

            log.Info($"Started processing DQT data from Blob 'dqt-cont'. \n Name:{name} \n Size: {csvBlob.Length} Bytes");

            await csvProcessor.SaveCSVDataToDatabase(csvBlob);

            log.Info($"Finished processing DQT data from Blob 'dqt-cont'. \n Name:{name} \n Size: {csvBlob.Length} Bytes");
        }
    }
}
