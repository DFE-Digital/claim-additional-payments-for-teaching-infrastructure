using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace claim_additional_payments_for_teaching_qts_api
{
    public static class DQTCsvProcessor
    {
        [FunctionName("DQTCsvProcessor")]
        public static void Run([BlobTrigger("[BLOB-CONTAINER]/{name}", Connection = "")]Stream myBlob, string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
        }
    }
}
