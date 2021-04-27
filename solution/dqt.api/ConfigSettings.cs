using dqt.domain;
using System;

namespace dqt.api
{
    public class ConfigSettings : IConfigSettings
    {
        public string AzureWebJobsStorage => Environment.GetEnvironmentVariable("AzureWebJobsStorage");

        public string DatabaseName => Environment.GetEnvironmentVariable("DatabaseName");

        public string DatabaseServerName => Environment.GetEnvironmentVariable("DatabaseServerName");

        public string DatabaseUsername => Environment.GetEnvironmentVariable("DatabaseUsername");

        public string DatabasePassword => Environment.GetEnvironmentVariable("DatabasePassword");

        public string DQTApiKey => Environment.GetEnvironmentVariable("DQTApiKey");

        public string DQTRollbarAccessToken => Environment.GetEnvironmentVariable("DQTRollbarAccessToken");

        public string DQTRollbarEnvironment => Environment.GetEnvironmentVariable("DQTRollbarEnvironment");

        public string DQTSFTPRollbarEnvironment => Environment.GetEnvironmentVariable("DQTSFTPRollbarEnvironment");

        public string SFTPScheduleTriggerTime => Environment.GetEnvironmentVariable("SFTPScheduleTriggerTime");

        public string SFTPHostName => Environment.GetEnvironmentVariable("SFTPHostName");

        public string SFTPUserName => Environment.GetEnvironmentVariable("SFTPUserName");

        public string SFTPPassword => Environment.GetEnvironmentVariable("SFTPPassword");

        public string SFTPSshHostKeyFingerprint => Environment.GetEnvironmentVariable("SFTPSshHostKeyFingerprint");

        public string SFTPRemotePath => Environment.GetEnvironmentVariable("SFTPRemotePath");

        public string DQTBlobContainerName => Environment.GetEnvironmentVariable("DQTBlobContainerName");

        public string SFTPPortNumber => Environment.GetEnvironmentVariable("SFTPPortNumber");
    }
}
