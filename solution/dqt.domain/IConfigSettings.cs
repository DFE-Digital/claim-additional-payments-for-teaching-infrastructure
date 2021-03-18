using System;
using System.Collections.Generic;
using System.Text;

namespace dqt.domain
{
    public interface IConfigSettings
    {
        public string AzureWebJobsStorage { get; }

        public string DatabaseName { get; }

        public string DatabaseServerName { get; }

        public string DatabaseUsername { get; }

        public string DatabasePassword { get; }

        public string DQTApiKey { get; }

        public string DQTRollbarAccessToken { get; }

        public string DQTRollbarEnvironment { get; }

        public string SFTPScheduleTriggerTime { get; }

        public string SFTPHostName { get; }

        public string SFTPUserName { get; }

        public string SFTPPassword { get; }

        public string SFTPSshHostKeyFingerprint { get; }

        public string SFTPRemotePath { get; }

        public string DQTBlobContainerName { get; }
    }
}