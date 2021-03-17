using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace dqt.domain.Blob
{
    public class BlobService: IBlobService
    {
        private CloudBlobClient CloudStorageAccountInstance()
        {

            if (CloudStorageAccount.TryParse(Environment.GetEnvironmentVariable("AzureWebJobsStorage"), out var cloudStorageAccount))
            {
                var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                return cloudBlobClient;
            }

            throw new Exception("Couldn't connect to the blob.");
        }

        public async Task UploadFile(FileStream filestream, string fileName)
        {  
            var cloudBlobContainer = CloudStorageAccountInstance().GetContainerReference(Environment.GetEnvironmentVariable("DQTBlobContainerName"));
            var cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(fileName);
            await cloudBlockBlob.UploadFromStreamAsync(filestream);
        }

        public async Task DeleteFile(string fileName)
        {
            var cloudBlobContainer = CloudStorageAccountInstance().GetContainerReference(Environment.GetEnvironmentVariable("DQTBlobContainerName"));
            var cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(fileName);
            await cloudBlockBlob.DeleteIfExistsAsync();
        }
    }
}
