using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using dqt.datalayer;

namespace dqt.api
{
    public class DQTCsvProcessor
    {
        private readonly DQTDataContext _context;

        public DQTCsvProcessor(DQTDataContext context)
        {
            _context = context;
        }

        [FunctionName("dqt-csv-processor")]
        public void Run([BlobTrigger("[BLOB-CONTAINER]/{name}", Connection = "")]Stream myBlob, string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");

            _context.Database.EnsureCreated();

            _context.SaveChanges();
        }
    }
}
