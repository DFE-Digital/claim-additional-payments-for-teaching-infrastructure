using System;
using System.Linq;
using System.Threading.Tasks;
using dqt.api.DTOs;
using dqt.datalayer.Model;
using dqt.datalayer.Repository;

namespace dqt.domain.FileTransfer
{
    public class DQTFileTransferService : IDQTFileTransferService
    {
        private readonly IRepository<DQTFileTransfer> _repository;

        public DQTFileTransferService(IRepository<DQTFileTransfer> repository)
        {
            _repository = repository;
        }

        public async Task<DQTFileTransferDTO> GetDQTFileTransferServiceStatus()
        {
            var fileTransfers = await _repository.FindAllAsync();

            if(fileTransfers.Any())
            {
                var latestFileTransfer = fileTransfers.OrderByDescending(r => r.LastRun).First();
                var successfulFileTransfers = await _repository.FindAsync(r => r.Status == DQTFileTransferStatus.Success.ToString());

                var result = new DQTFileTransferDTO()
                {
                    LastSuccessfulRunDate = null,
                    LastRunDate = latestFileTransfer.LastRun,
                    LastRunStatus = latestFileTransfer.Status,
                    LastRunError = latestFileTransfer.Error
                };

                if (successfulFileTransfers.Any())
                {
                    result.LastSuccessfulRunDate = successfulFileTransfers.Max(s => s.LastRun);
                }

                return await Task.FromResult(result);
            }

            return await Task.FromResult<DQTFileTransferDTO>(null);
        }

        public async Task<int> AddDQTFileTransferDetails(DateTime date, DQTFileTransferStatus status, string error)
        {
            var entity = new DQTFileTransfer()
            {
                LastRun = date,
                Error = error,
                Status = status.ToString()
            };

            return await _repository.InsertAsync(entity);
        }
    }
}
