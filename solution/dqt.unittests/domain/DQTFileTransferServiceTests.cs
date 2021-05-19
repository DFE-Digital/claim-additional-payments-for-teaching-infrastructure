using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Xunit;
using dqt.datalayer.Model;
using dqt.datalayer.Repository;
using dqt.domain.FileTransfer;
using dqt.api.DTOs;

namespace dqt.unittests.domain
{
    public class DQTFileTransferServiceTests
    {
        private readonly DateTime today = DateTime.Now;
        private readonly IDQTFileTransferService _dqtFileTransferService;
        private readonly List<DQTFileTransfer> mockFileTransferResults;

        private Mock<IRepository<DQTFileTransfer>> _repositoryMock;

        public DQTFileTransferServiceTests()
        {
            _repositoryMock = new Mock<IRepository<DQTFileTransfer>>();
            _dqtFileTransferService = new DQTFileTransferService(_repositoryMock.Object);
            mockFileTransferResults = new List<DQTFileTransfer>
            {
                new DQTFileTransfer() { LastRun = today, Status = DQTFileTransferStatus.Failure.ToString(), Error = "File not found" },
                new DQTFileTransfer() { LastRun = today.AddDays(-1), Status = DQTFileTransferStatus.Success.ToString(), Error = "" },
                new DQTFileTransfer() { LastRun = today.AddDays(-2), Status = DQTFileTransferStatus.Failure.ToString(), Error = "File columns didn't match" },
                new DQTFileTransfer() { LastRun = today.AddDays(-3), Status = DQTFileTransferStatus.Success.ToString(), Error = "" },
                new DQTFileTransfer() { LastRun = today.AddDays(-4), Status = DQTFileTransferStatus.Success.ToString(), Error = "" }
            };
        }

        [Fact]
        public async void Returns_LastRunDetailsAndLastSuccessfulRunDate()
        {
            _repositoryMock
                .Setup(q => q.FindAllAsync())
                .ReturnsAsync(mockFileTransferResults);

            _repositoryMock
                .Setup(q => q.FindAsync(r => r.Status == DQTFileTransferStatus.Success.ToString()))
                .ReturnsAsync(mockFileTransferResults.Where(r => r.Status == DQTFileTransferStatus.Success.ToString()));

            var actualResult = await _dqtFileTransferService.GetDQTFileTransferServiceStatus();
            var expectedResult = new DQTFileTransferDTO()
            {
                LastSuccessfulRunDate = today.AddDays(-1),
                LastRunDate = today,
                LastRunStatus = DQTFileTransferStatus.Failure.ToString(),
                LastRunError = "File not found"
            };

            Assert.Equal(actualResult.LastSuccessfulRunDate, expectedResult.LastSuccessfulRunDate);
            Assert.Equal(actualResult.LastRunDate, expectedResult.LastRunDate);
            Assert.Equal(actualResult.LastRunStatus, expectedResult.LastRunStatus);
        }

        [Fact]
        public async void Returns_LastSuccessfulRunDateNull_WhenNoSuccessRunFound()
        {
            _repositoryMock
                .Setup(q => q.FindAllAsync())
                .ReturnsAsync(mockFileTransferResults);

            _repositoryMock
                .Setup(q => q.FindAsync(r => r.Status == DQTFileTransferStatus.Success.ToString()))
                .ReturnsAsync(new List<DQTFileTransfer>());

            var actualResult = await _dqtFileTransferService.GetDQTFileTransferServiceStatus();
            var expectedResult = new DQTFileTransferDTO()
            {
                LastSuccessfulRunDate = null,
                LastRunDate = today,
                LastRunStatus = DQTFileTransferStatus.Failure.ToString(),
                LastRunError = "File not found"
            };

            Assert.Equal(actualResult.LastSuccessfulRunDate, expectedResult.LastSuccessfulRunDate);
            Assert.Equal(actualResult.LastRunDate, expectedResult.LastRunDate);
            Assert.Equal(actualResult.LastRunStatus, expectedResult.LastRunStatus);
        }

        [Fact]
        public async void Returns_Null_WhenNoRecordsFound()
        {
            _repositoryMock
                .Setup(q => q.FindAllAsync())
                .ReturnsAsync(new List<DQTFileTransfer>());

            _repositoryMock
                .Setup(q => q.FindAsync(x => x.Status == DQTFileTransferStatus.Success.ToString()))
                .ReturnsAsync(new List<DQTFileTransfer>());

            var results = await _dqtFileTransferService.GetDQTFileTransferServiceStatus();
            var actualResult = await _dqtFileTransferService.GetDQTFileTransferServiceStatus();

            Assert.Null(actualResult);
        }
    }
}
