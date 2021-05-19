using System;
using System.Threading.Tasks;
using dqt.api.DTOs;
using dqt.datalayer.Model;

namespace dqt.domain.FileTransfer
{
    public interface IDQTFileTransferService
    {
        Task<DQTFileTransferDTO> GetDQTFileTransferServiceStatus();

        Task<int> AddDQTFileTransferDetails(DateTime date, DQTFileTransferStatus status, string error);
    }
}
