using System;
using System.Collections.Generic;
using Moq;
using Xunit;
using dqt.datalayer.Model;
using Microsoft.EntityFrameworkCore;
using dqt.datalayer.Database;
using dqt.datalayer.Repository;
using System.Linq;
using dqt.domain.FileTransfer;

namespace dqt.unittests.datalayer
{
    public class DQTFileTransferRepositoryTests
    {
        private readonly List<DQTFileTransfer> dqtFileTransfers;
        private readonly DateTime today = DateTime.Now;
        private readonly Mock<DbSet<DQTFileTransfer>> _dbSetMock;
        private readonly Mock<DQTDataContext> _dbContextMock;

        private readonly DQTFileTransferRepository _repository;

        public DQTFileTransferRepositoryTests()
        {
            dqtFileTransfers = new List<DQTFileTransfer>
            {
                new DQTFileTransfer() { LastRun = today, Status = DQTFileTransferStatus.Failure.ToString(), Error = "File not found" },
                new DQTFileTransfer() { LastRun = today.AddDays(-1), Status = DQTFileTransferStatus.Success.ToString(), Error = "" },
                new DQTFileTransfer() { LastRun = today.AddDays(-2), Status = DQTFileTransferStatus.Failure.ToString(), Error = "File columns didn't match" },
                new DQTFileTransfer() { LastRun = today.AddDays(-3), Status = DQTFileTransferStatus.Success.ToString(), Error = "" },
                new DQTFileTransfer() { LastRun = today.AddDays(-4), Status = DQTFileTransferStatus.Success.ToString(), Error = "" }
            };

            _dbSetMock = new Mock<DbSet<DQTFileTransfer>>();

            _dbSetMock.As<IQueryable<DQTFileTransfer>>().Setup(x => x.Provider).Returns(dqtFileTransfers.AsQueryable().Provider);
            _dbSetMock.As<IQueryable<DQTFileTransfer>>().Setup(x => x.Expression).Returns(dqtFileTransfers.AsQueryable().Expression);
            _dbSetMock.As<IQueryable<DQTFileTransfer>>().Setup(x => x.ElementType).Returns(dqtFileTransfers.AsQueryable().ElementType);
            _dbSetMock.As<IQueryable<DQTFileTransfer>>().Setup(x => x.GetEnumerator()).Returns(dqtFileTransfers.AsQueryable().GetEnumerator());

            _dbContextMock = new Mock<DQTDataContext>();
            _dbContextMock.Setup(x => x.Set<DQTFileTransfer>()).Returns(_dbSetMock.Object);

            _repository = new DQTFileTransferRepository(_dbContextMock.Object);
        }

        [Fact]
        public async void Returns_DQTFileTransferRecords_WhenMatchFound()
        {
            var results = await _repository.FindAsync(x => x.Status == DQTFileTransferStatus.Success.ToString());

            Assert.Equal(3, results.Count());
        }

        [Fact]
        public async void Returns_AllRecords()
        {
            var results = await _repository.FindAllAsync();

            Assert.Equal(5, results.ToList().Count());
        }
    }
}
