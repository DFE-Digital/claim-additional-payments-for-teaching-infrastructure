using System.IO;
using System.Threading.Tasks;
using dqt.domain;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace dqt.api
{
    public class DQTCsvProcessor
    {
        private readonly ICSVProcessor csvProcessor;

        public DQTCsvProcessor(ICSVProcessor csvProcessor)
        {
            this.csvProcessor = csvProcessor;
        }
        [FunctionName("dqt-csv-processor")]
        public async Task Run([BlobTrigger("dqt-cont/{name}", Connection = "AzureWebJobsStorage")] Stream csvBlob, string name, ILogger log)
        {

            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {csvBlob.Length} Bytes");

            await csvProcessor.SaveCSVDataToDatabase(csvBlob);

            log.LogInformation($"DONE C# Blob trigger function Processed blob\n Name:{name} \n Size: {csvBlob.Length} Bytes");
        }
    }
}
