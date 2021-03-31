using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Xunit;
using Moq;
using Newtonsoft.Json;
using System.IO;
using dqt.datalayer.Model;
using dqt.api.DTOs;
using dqt.api.Authorization;
using dqt.api.Functions;
using dqt.domain.Rollbar;
using dqt.domain.QTS;
using System.Web;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Http.Internal;
using System.Text;
using dqt.domain.Encoding;

namespace dqt.unittests.api
{
    public class QualifiedTeacherStatusServiceTests
    {
        private const string TRN = "TRN";
        private const string NI = "NI";
        private const string API_KEY = "api-key";

        private readonly Mock<IRollbarService> _loggerMock;
        private readonly Mock<IQualifiedTeachersService> _qualifiedTeachersServiceMock;
        private readonly QualifiedTeacherStatusService _qualifiedTeacherStatusService;
        private readonly List<QualifiedTeacher> _mockQualifiedTeachers;
        private readonly Mock<IAuthorize> _mockAuth;

        public QualifiedTeacherStatusServiceTests()
        {
            Environment.SetEnvironmentVariable("APIKey", API_KEY);

            _loggerMock = new Mock<IRollbarService>();
            _qualifiedTeachersServiceMock = new Mock<IQualifiedTeachersService>();
            _mockAuth = new Mock<IAuthorize>();
            _qualifiedTeacherStatusService = new QualifiedTeacherStatusService(_qualifiedTeachersServiceMock.Object, _loggerMock.Object, _mockAuth.Object);

            _mockQualifiedTeachers = new List<QualifiedTeacher>
            {
                new QualifiedTeacher() { Name = "TEST1", Trn = TRN, NINumber = NI }
            };
        }

        [Fact]
        public async void Returns_UnauthorizedRequestResponse_WhenRequestIsUnAuthorized()
        {
            RequestInfo requestInfo = new RequestInfo()
            {
                TRN = "Test-TRN",
                NINumber = "Test-NI"
            };

            _mockAuth.Setup(x => x.AuthorizeRequest(It.IsAny<HttpRequest>())).Returns(false);
            var request = CreateMockHttpRequest(requestInfo);
            var response = (UnauthorizedResult)await _qualifiedTeacherStatusService.Run(request.Object);

            Assert.Equal(401, response.StatusCode);
        }

        [Fact]
        public async void Returns_BadRequestResponse_WhenTRNNotPresent()
        {

            RequestInfo requestInfo = new RequestInfo()
            {
                NINumber = "Test-NI"
            };

            _mockAuth.Setup(x => x.AuthorizeRequest(It.IsAny<HttpRequest>())).Returns(true);
            var request = CreateMockHttpRequest(requestInfo);
            var response = (BadRequestObjectResult)await _qualifiedTeacherStatusService.Run(request.Object);
            var resultDto = (ResultDTO<List<QualifiedTeacher>>)response.Value;

            Assert.Equal(400, response.StatusCode);
            Assert.Equal("TeacherReferenceNumber is mandatory.", resultDto.Message);
        }

        [Fact]
        public async void Returns_NotFoundObjectResult_WhenRequestIsValidAndNoMatchFound()
        {
            RequestInfo requestInfo = new RequestInfo()
            {
                TRN = "Test-TRN",
                NINumber = "Test-NI"
            };

            _mockAuth.Setup(x => x.AuthorizeRequest(It.IsAny<HttpRequest>())).Returns(true);
            _qualifiedTeachersServiceMock.Setup(x => x.GetQualifiedTeacherRecords(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new List<QualifiedTeacher>());

            var request = CreateMockHttpRequest(requestInfo);
            var response = (NotFoundObjectResult)await _qualifiedTeacherStatusService.Run(request.Object);
            var resultDto = (ResultDTO<List<QualifiedTeacher>>)response.Value;

            Assert.Equal(404, response.StatusCode);
            Assert.Equal("No records found.", resultDto.Message);
        }

        [Fact]
        public async void Returns_InternalServerError_WhenAnyExceptionEncountered()
        {
            RequestInfo requestInfo = new RequestInfo()
            {
                TRN = "1229708",
                NINumber = "6A9Y58Z41"
            };

            _mockAuth.Setup(x => x.AuthorizeRequest(It.IsAny<HttpRequest>())).Returns(true);
            string exceptionMessage = "Not able to connect to database";
            _qualifiedTeachersServiceMock.Setup(x => x.GetQualifiedTeacherRecords(It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new Exception(exceptionMessage));


            var request = CreateMockHttpRequest(requestInfo);
            var response = (ObjectResult)await _qualifiedTeacherStatusService.Run(request.Object);
            var resultDto = (ResultDTO<List<QualifiedTeacher>>)response.Value;

            Assert.Equal(500, response.StatusCode);
            Assert.Equal($"Error retrieving qualified teacher status data. {exceptionMessage}", resultDto.Message);
        }

        [Fact]
        public async void Returns_SuccessResponseWithQualifiedTeacherRecords_WhenRequestIsValid()
        {
            RequestInfo requestInfo = new RequestInfo()
            {
                TRN = "Test-TRN",
                NINumber = "Test-NI"
            };

            _mockAuth.Setup(x => x.AuthorizeRequest(It.IsAny<HttpRequest>())).Returns(true);
            _qualifiedTeachersServiceMock.Setup(x => x.GetQualifiedTeacherRecords(requestInfo.TRN, requestInfo.NINumber)).ReturnsAsync(_mockQualifiedTeachers);

            var request = CreateMockHttpRequest(requestInfo);

            var response = (OkObjectResult)await _qualifiedTeacherStatusService.Run(request.Object);
            var resultDto = (ResultDTO<List<QualifiedTeacher>>)response.Value;

            Assert.Equal(200, response.StatusCode);
            Assert.Equal(_mockQualifiedTeachers, resultDto.Data);
        }

        private Mock<HttpRequest> CreateMockHttpRequest(RequestInfo requestDto)
        {
            var mockRequest = new Mock<HttpRequest>();

            var paramsDictionary = new Dictionary<string, StringValues> {
                { "trn",  string.IsNullOrWhiteSpace(requestDto.TRN)? null:ParameterEncoder.Base64StringEncode(requestDto.TRN) },
                { "ni", string.IsNullOrWhiteSpace(requestDto.NINumber)? null: ParameterEncoder.Base64StringEncode(requestDto.NINumber) }
            };

            mockRequest.SetupGet(x => x.Query).Returns(new QueryCollection(paramsDictionary));

            var headDictionary = new Dictionary<string, StringValues>
            {
                {"x-correlation-id",  Guid.NewGuid().ToString() }
            };

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["x-correlation-id"] = Guid.NewGuid().ToString();

            IHeaderDictionary x = new HeaderDictionary(headDictionary);

            mockRequest.Setup(x => x.Headers).Returns(x);

            return mockRequest;
        }
    }
    public class RequestInfo
    {

        public string TRN { get; set; }
        public string NINumber { get; set; }
    }

}
