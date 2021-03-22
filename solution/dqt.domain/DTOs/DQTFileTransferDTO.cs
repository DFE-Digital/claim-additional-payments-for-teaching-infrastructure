using System;
using dqt.datalayer.Model;

namespace dqt.api.DTOs
{
    public class DQTFileTransferDTO
    {
        public DateTime? LastSuccessfulRunDate { get; set; }

        public DateTime LastRunDate { get; set; }

        public string LastRunStatus { get; set; }

        public string LastRunError { get; set; }
    }
}
