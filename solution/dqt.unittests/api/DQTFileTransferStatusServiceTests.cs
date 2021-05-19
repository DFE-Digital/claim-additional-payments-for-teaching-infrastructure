using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Xunit;
using Moq;
using Newtonsoft.Json;
using System.IO;
using dqt.datalayer.Model;
using dqt.api.Authorization;
using dqt.api.Functions;
using dqt.domain.Rollbar;
using dqt.domain.FileTransfer;
using dqt.api.DTOs;

namespace dqt.unittests.api
{
    public class DQTFileTransferStatusServiceTests
    {
        private readonly DateTime today = DateTime.Now;
        private const string API_KEY = "api-key";

        private readonly Mock<IRollbarService> _loggerMock;
        private readonly Mock<IDQTFileTransferService> _dqtFileTransferServiceMock;
        private readonly DQTFileTransferStatusService _qualifiedTeacherStatusService;
        private readonly Mock<IAuthorize> _mockAuth;

        public DQTFileTransferStatusServiceTests()
        {
            Environment.SetEnvironmentVariable("APIKey", API_KEY);

            _loggerMock = new Mock<IRollbarService>();
            _dqtFileTransferServiceMock = new Mock<IDQTFileTransferService>();
            _mockAuth = new Mock<IAuthorize>();
            _qualifiedTeacherStatusService = new DQTFileTransferStatusService(_dqtFileTransferServiceMock.Object, _loggerMock.Object, _mockAuth.Object);
        }

        [Fact]
        public async void Returns_UnauthorizedRequestResponse_WhenRequestIsUnAuthorized()
        {
            _mockAuth.Setup(x => x.AuthorizeRequest(It.IsAny<HttpRequest>())).Returns(false);

            var request = CreateMockHttpRequest();
            var response = (UnauthorizedResult)await _qualifiedTeacherStatusService.Run(request.Object);

            Assert.Equal(401, response.StatusCode);
        }

        [Fact]
        public async void Returns_NotFoundObjectResult_WhenRequestIsValidAndNoRecordsFound()
        {
            DQTFileTransferDTO mockResult = null;

            _mockAuth.Setup(x => x.AuthorizeRequest(It.IsAny<HttpRequest>())).Returns(true);
            _dqtFileTransferServiceMock.Setup(x => x.GetDQTFileTransferServiceStatus()).ReturnsAsync(mockResult);

            var request = CreateMockHttpRequest();
            var response = (NotFoundObjectResult)await _qualifiedTeacherStatusService.Run(request.Object);

            Assert.Equal(404, response.StatusCode);
            Assert.Equal("No file transfer record(s) found", response.Value);
        }

        [Fact]
        public async void Returns_InternalServerError_WhenAnyExceptionEncountered()
        {
            _mockAuth.Setup(x => x.AuthorizeRequest(It.IsAny<HttpRequest>())).Returns(true);
            string exceptionMessage = "Not able to connect to database";
            _dqtFileTransferServiceMock.Setup(x => x.GetDQTFileTransferServiceStatus()).ThrowsAsync(new Exception(exceptionMessage));

            var request = CreateMockHttpRequest();
            var response = (ObjectResult)await _qualifiedTeacherStatusService.Run(request.Object);
            var resultDto = (ResultDTO<DQTFileTransferDTO>)response.Value;

            Assert.Equal(500, response.StatusCode);
            Assert.Equal(exceptionMessage, resultDto.Message);
        }

        [Fact]
        public async void Returns_SuccessResponseWithDQTFileTransferRecords_WhenRequestIsValid()
        {
            var mockResult = new DQTFileTransferDTO()
            {
                LastSuccessfulRunDate = today.AddDays(-1),
                LastRunDate = today,
                LastRunStatus = DQTFileTransferStatus.Failure.ToString(),
                LastRunError = "File not found"
            };

            _mockAuth.Setup(x => x.AuthorizeRequest(It.IsAny<HttpRequest>())).Returns(true);
            _dqtFileTransferServiceMock.Setup(x => x.GetDQTFileTransferServiceStatus()).ReturnsAsync(mockResult);

            var request = CreateMockHttpRequest();
            var response = (OkObjectResult)await _qualifiedTeacherStatusService.Run(request.Object);
            var resultDto = (ResultDTO<DQTFileTransferDTO>)response.Value;

            Assert.Equal(200, response.StatusCode);
            Assert.Equal(mockResult, resultDto.Data);
        }

        private Mock<HttpRequest> CreateMockHttpRequest()
        {
            var mockRequest = new Mock<HttpRequest>();

            var memoryStream = new MemoryStream();
            var writer = new StreamWriter(memoryStream);

            writer.Write("");
            writer.Flush();

            memoryStream.Position = 0;
            mockRequest.Setup(x => x.Body).Returns(memoryStream);

            return mockRequest;
        }
    }
}
