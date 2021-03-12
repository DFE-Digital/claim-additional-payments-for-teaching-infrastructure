using System;
using System.IO;
using System.Linq;
using Azure.Storage.Blobs;
using Microsoft.Azure.WebJobs;
using WinSCP;

namespace dqt.domain.SFTPToBlob
{
    public class SFTPToBlobProcessor : ISFTPToBlobProcessor
    {
        public void SaveCSVToBlob(ExecutionContext context)
        {
            using Session session = new Session
            {
                ExecutablePath = Path.Combine(context.FunctionAppDirectory, "winscp.exe")
            };

            session.Open(new SessionOptions
            {
                Protocol = Protocol.Sftp,
                HostName = Environment.GetEnvironmentVariable("SFTPHostName"),
                UserName = Environment.GetEnvironmentVariable("SFTPUserName"),
                Password = Environment.GetEnvironmentVariable("SFTPPassword"),
                SshHostKeyFingerprint = Environment.GetEnvironmentVariable("SFTPSshHostKeyFingerprint")
            });

            var remotePath = Environment.GetEnvironmentVariable("SFTPRemotePath");
            var directory = session.ListDirectory(remotePath);

            var files = directory.Files
                .Where(fileInfo => !fileInfo.IsDirectory && fileInfo.Name.EndsWith(".csv", StringComparison.OrdinalIgnoreCase));

            if (files.Count() == 0)
            {
                throw new Exception($"No mathcing file found in the location");
            }
            else if (files.Count() > 1)
            {
                throw new Exception($"More than one mathcing file found in the location");
            }
            else
            {
                var fileName = files.Single().Name;
                var tempPath = Path.GetTempFileName();
                var sourcePath = RemotePath.EscapeFileMask(remotePath + "/" + fileName);

                session.GetFiles(sourcePath, tempPath).Check();

                var blobServiceClient = new BlobServiceClient(Environment.GetEnvironmentVariable("AzureWebJobsStorage"));
                var containerClient = blobServiceClient.GetBlobContainerClient(Environment.GetEnvironmentVariable("DQTBlobContainerName"));
                var blobClient = containerClient.GetBlobClient(fileName);

                using FileStream uploadFileStream = File.OpenRead(tempPath);
                blobClient.Upload(uploadFileStream, true);
                uploadFileStream.Close();

                File.Delete(tempPath);
                session.RemoveFiles(sourcePath);
            }
        }
    }
}
