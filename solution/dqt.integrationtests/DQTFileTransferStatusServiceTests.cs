using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Xunit;
using Moq;
using System.IO;
using dqt.api.Functions;
using dqt.domain.Rollbar;
using dqt.domain.FileTransfer;
using dqt.api.DTOs;
using dqt.datalayer.Repository;
using dqt.datalayer.Model;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using dqt.datalayer.Database;
using System.Linq;
using dqt.api.Authorization;
using dqt.domain;
using Microsoft.Extensions.Primitives;

namespace dqt.integrationtests
{
    public class DQTFileTransferStatusServiceTests
    {
        private readonly DateTime today = DateTime.Now;
        private const string API_KEY = "api-key";

        private readonly DQTFileTransferStatusService _qualifiedTeacherStatusService;

        public DQTFileTransferStatusServiceTests()
        {
            Environment.SetEnvironmentVariable("APIKey", API_KEY);

            var loggerMock = new Mock<IRollbarService>();
            var configSettingsMock = new Mock<IConfigSettings>();
            configSettingsMock.Setup(config => config.DQTApiKey).Returns(API_KEY);

            var dqtFileTransfers = new List<DQTFileTransfer>
            {
                new DQTFileTransfer() { LastRun = today, Status = DQTFileTransferStatus.Failure.ToString(), Error = "File not found" },
                new DQTFileTransfer() { LastRun = today.AddDays(-1), Status = DQTFileTransferStatus.Success.ToString(), Error = "" },
                new DQTFileTransfer() { LastRun = today.AddDays(-2), Status = DQTFileTransferStatus.Failure.ToString(), Error = "File columns didn't match" },
                new DQTFileTransfer() { LastRun = today.AddDays(-3), Status = DQTFileTransferStatus.Success.ToString(), Error = "" },
                new DQTFileTransfer() { LastRun = today.AddDays(-4), Status = DQTFileTransferStatus.Success.ToString(), Error = "" }
            };

            var dbSetMock = new Mock<DbSet<DQTFileTransfer>>();

            dbSetMock.As<IQueryable<DQTFileTransfer>>().Setup(x => x.Provider).Returns(dqtFileTransfers.AsQueryable().Provider);
            dbSetMock.As<IQueryable<DQTFileTransfer>>().Setup(x => x.Expression).Returns(dqtFileTransfers.AsQueryable().Expression);
            dbSetMock.As<IQueryable<DQTFileTransfer>>().Setup(x => x.ElementType).Returns(dqtFileTransfers.AsQueryable().ElementType);
            dbSetMock.As<IQueryable<DQTFileTransfer>>().Setup(x => x.GetEnumerator()).Returns(dqtFileTransfers.AsQueryable().GetEnumerator());

            var dbContextMock = new Mock<DQTDataContext>();
            dbContextMock.Setup(x => x.Set<DQTFileTransfer>()).Returns(dbSetMock.Object);

            var repository = new DQTFileTransferRepository(dbContextMock.Object);
            var dqtFileTransferService = new DQTFileTransferService(repository);

            _qualifiedTeacherStatusService = new DQTFileTransferStatusService(dqtFileTransferService, loggerMock.Object, new Authorize(configSettingsMock.Object));
        }

        [Fact]
        public async void Returns_UnauthorizedRequestResponse_WhenRequestIsUnAuthorized()
        {
            var request = CreateMockHttpRequest(false);
            var response = (UnauthorizedResult)await _qualifiedTeacherStatusService.Run(request.Object);

            Assert.Equal(401, response.StatusCode);
        }

        [Fact]
        public async void Returns_SuccessResponseWithDQTFileTransferRecords_WhenRequestIsValid()
        {
            var mockResult = new DQTFileTransferDTO()
            {
                LastSuccessfulRunDate = today.AddDays(-1),
                LastRunDate = today.AddDays(-1),
                LastRunStatus = DQTFileTransferStatus.Success.ToString(),
                LastRunError = ""
            };

            var request = CreateMockHttpRequest();
            var response = (OkObjectResult)await _qualifiedTeacherStatusService.Run(request.Object);
            var resultDto = (ResultDTO<DQTFileTransferDTO>)response.Value;

            Assert.Equal(200, response.StatusCode);
            Assert.Equal(mockResult.LastRunDate, resultDto.Data.LastRunDate);
        }

        private Mock<HttpRequest> CreateMockHttpRequest(bool addAuthKey = true)
        {
            var mockRequest = new Mock<HttpRequest>();

            var headDictionary = new Dictionary<string, StringValues>();

            if (addAuthKey)
            {
                headDictionary.Add("Authorization", API_KEY);
            }

            mockRequest.Setup(x => x.Headers).Returns(new HeaderDictionary(headDictionary));

            return mockRequest;
        }
    }
}
