using System;
using System.IO;
using System.Linq;
using Microsoft.WindowsAzure.Storage;
using Microsoft.Azure.WebJobs;
using WinSCP;
using dqt.domain.Blob;

namespace dqt.domain.SFTPToBlob
{
    public class SFTPToBlobProcessor : ISFTPToBlobProcessor
    {
        private readonly IBlobService _blobService;

        public SFTPToBlobProcessor(IBlobService blobService)
        {
            _blobService = blobService;
        }
        public async System.Threading.Tasks.Task SaveCSVToBlobAsync(ExecutionContext context)
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

                using var uploadFileStream = File.OpenRead(tempPath);
                await _blobService.UploadFile(uploadFileStream, fileName);
                uploadFileStream.Close();

                File.Delete(tempPath);
                session.RemoveFiles(sourcePath);
            }
        }
    }
}
