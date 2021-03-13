using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Xunit;
using Moq;
using Newtonsoft.Json;
using System.IO;
using dqt.api;
using dqt.domain;
using dqt.datalayer.Model;
using dqt.api.Authorization;

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
        private readonly ExistingQualifiedTeacherRequest _requestObj;
        private readonly List<QualifiedTeacher> _mockQualifiedTeachers;
        private readonly Mock<IAuthorize> _mockAuth;

        public QualifiedTeacherStatusServiceTests()
        {
            Environment.SetEnvironmentVariable("APIKey", API_KEY);

            _loggerMock = new Mock<IRollbarService>();
            _qualifiedTeachersServiceMock = new Mock<IQualifiedTeachersService>();
            _mockAuth = new Mock<IAuthorize>();
            _qualifiedTeacherStatusService = new QualifiedTeacherStatusService(_qualifiedTeachersServiceMock.Object, _loggerMock.Object, _mockAuth.Object);

            _requestObj = new ExistingQualifiedTeacherRequest()
            {
                TRN = TRN,
                NINumber = NI
            };

            _mockQualifiedTeachers = new List<QualifiedTeacher>
            {
                new QualifiedTeacher() { Name = "TEST1", Trn = TRN, NINumber = NI }
            };
        }

        [Fact]
        public async void Returns_UnauthorizedRequestResponse_WhenRequestIsUnAuthorized()
        {

            _mockAuth.Setup(x => x.AuthorizeRequest(It.IsAny<HttpRequest>())).Returns(false);
            var request = CreateMockHttpRequest(_requestObj, null);
            var response = (UnauthorizedResult)await _qualifiedTeacherStatusService.Run(request.Object);

            Assert.Equal(401, response.StatusCode);
        }

        [Fact]
        public async void Returns_BadRequestResponse_WhenRequestBodyNull()
        {

            _mockAuth.Setup(x => x.AuthorizeRequest(It.IsAny<HttpRequest>())).Returns(true);
            var request = CreateMockHttpRequest(null);

            var response = (BadRequestObjectResult)await _qualifiedTeacherStatusService.Run(request.Object);

            Assert.Equal(400, response.StatusCode);
            Assert.Equal("TeacherReferenceNumber is mandatory", response.Value);
        }

        [Fact]
        public async void Returns_BadRequestResponse_WhenRequestBodyIsNotDeserializable()
        {
            var memoryStream = new MemoryStream();
            var writer = new StreamWriter(memoryStream);

            var json = "{ abs: abs' }";

            writer.Write(json);
            writer.Flush();
            memoryStream.Position = 0;

            _mockAuth.Setup(x => x.AuthorizeRequest(It.IsAny<HttpRequest>())).Returns(true);
            var request = CreateMockHttpRequest(null);
            request.Setup(r => r.Body).Returns(memoryStream);
            _mockAuth.Setup(x => x.AuthorizeRequest(It.IsAny<HttpRequest>())).Returns(true);

            var response = (BadRequestObjectResult)await _qualifiedTeacherStatusService.Run(request.Object);

            Assert.Equal(400, response.StatusCode);
            Assert.Equal("Bad request data", response.Value);
        }

        [Fact]
        public async void Returns_BadRequestResponse_WhenTRNNotPresent()
        {
            var requestBody = new ExistingQualifiedTeacherRequest()
            {
                NINumber = NI
            };

            _mockAuth.Setup(x => x.AuthorizeRequest(It.IsAny<HttpRequest>())).Returns(true);
            var request = CreateMockHttpRequest(requestBody);
            var response = (BadRequestObjectResult)await _qualifiedTeacherStatusService.Run(request.Object);

            Assert.Equal(400, response.StatusCode);
            Assert.Equal("TeacherReferenceNumber is mandatory", response.Value);
        }

        [Fact]
        public async void Returns_NotFoundObjectResult_WhenRequestIsValidAndNoMatchFound()
        {

            _mockAuth.Setup(x => x.AuthorizeRequest(It.IsAny<HttpRequest>())).Returns(true);
            _qualifiedTeachersServiceMock.Setup(x => x.GetQualifiedTeacherRecords(TRN, NI)).ReturnsAsync(new List<QualifiedTeacher>());

            var request = CreateMockHttpRequest(_requestObj);
            var response = (NotFoundObjectResult)await _qualifiedTeacherStatusService.Run(request.Object);

            Assert.Equal(404, response.StatusCode);
            Assert.Equal("No records found", response.Value);
        }

        [Fact]
        public async void Returns_InternalServerError_WhenAnyExceptionEncountered()
        {
            _mockAuth.Setup(x => x.AuthorizeRequest(It.IsAny<HttpRequest>())).Returns(true);
            string exceptionMessage = "Not able to connect to database";
            _qualifiedTeachersServiceMock.Setup(x => x.GetQualifiedTeacherRecords(TRN, NI)).ThrowsAsync(new Exception(exceptionMessage));

            var request = CreateMockHttpRequest(_requestObj);
            var response = (ObjectResult)await _qualifiedTeacherStatusService.Run(request.Object);

            Assert.Equal(500, response.StatusCode);
            Assert.Equal(exceptionMessage, response.Value);
        }

        [Fact]
        public async void Returns_SuccessResponseWithQualifiedTeacherRecords_WhenRequestIsValid()
        {

            _mockAuth.Setup(x => x.AuthorizeRequest(It.IsAny<HttpRequest>())).Returns(true);
            _qualifiedTeachersServiceMock.Setup(x => x.GetQualifiedTeacherRecords(TRN, NI)).ReturnsAsync(_mockQualifiedTeachers);

            var request = CreateMockHttpRequest(_requestObj);
            var response = (OkObjectResult)await _qualifiedTeacherStatusService.Run(request.Object);

            Assert.Equal(200, response.StatusCode);
            Assert.Equal(_mockQualifiedTeachers, response.Value);
        }

        private Mock<HttpRequest> CreateMockHttpRequest(ExistingQualifiedTeacherRequest body, string apiKey = API_KEY)
        {
            //var headers = new HeaderDictionary(new Dictionary<string, StringValues>
            //{
            //    { "content-type", "application/json" },
            //}) as IHeaderDictionary;

            //if (apiKey != null)
            //{
            //    headers.Add("Authorization", apiKey);
            //}

            var mockRequest = new Mock<HttpRequest>();
            //mockRequest.Setup(r => r.Headers).Returns(headers);

            var memoryStream = new MemoryStream();
            var writer = new StreamWriter(memoryStream);

            var json = body != null ? JsonConvert.SerializeObject(body) : "";

            writer.Write(json);
            writer.Flush();

            memoryStream.Position = 0;
            mockRequest.Setup(x => x.Body).Returns(memoryStream);

            return mockRequest;
        }
    }
}
