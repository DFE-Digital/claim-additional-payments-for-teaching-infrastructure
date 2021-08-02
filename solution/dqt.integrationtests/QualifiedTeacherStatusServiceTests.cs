using Xunit;
using Moq;
using dqt.domain;
using dqt.domain.Rollbar;
using dqt.api.Functions;
using dqt.api.Authorization;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Primitives;
using System;
using Microsoft.AspNetCore.Mvc;
using dqt.datalayer.Model;
using dqt.domain.QTS;
using dqt.datalayer.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using dqt.datalayer.Repository;
using dqt.domain.DTOs;

namespace dqt.integrationtests
{
    public class QualifiedTeacherStatusServiceTests
    {
        private const string API_KEY = "api-key";
        private readonly List<QualifiedTeacher> _qualifiedTeachers = new List<QualifiedTeacher>
        {
            new QualifiedTeacher{ Trn="1229708", NINumber="AP558641W"},
            new QualifiedTeacher{ Trn="1234708", NINumber="TS9Y5841Q"},
            new QualifiedTeacher{ Trn="3429708", NINumber="SX9Y5841P"},
            new QualifiedTeacher{ Trn="12345", NINumber="MS8T6541Q"},
            new QualifiedTeacher{ Trn="0053100", NINumber="WE3X9356A"},
        };

        private readonly QualifiedTeacherStatusService _qualifiedTeacherStatusService;

        public QualifiedTeacherStatusServiceTests()
        {
            var loggerMock = new Mock<IRollbarService>();
            var configSettingsMock = new Mock<IConfigSettings>();
            configSettingsMock.Setup(config => config.DQTApiKey).Returns(API_KEY);

            var dbSetMock = new Mock<DbSet<QualifiedTeacher>>();

            dbSetMock.As<IQueryable<QualifiedTeacher>>().Setup(x => x.Provider).Returns(_qualifiedTeachers.AsQueryable().Provider);
            dbSetMock.As<IQueryable<QualifiedTeacher>>().Setup(x => x.Expression).Returns(_qualifiedTeachers.AsQueryable().Expression);
            dbSetMock.As<IQueryable<QualifiedTeacher>>().Setup(x => x.ElementType).Returns(_qualifiedTeachers.AsQueryable().ElementType);
            dbSetMock.As<IQueryable<QualifiedTeacher>>().Setup(x => x.GetEnumerator()).Returns(_qualifiedTeachers.AsQueryable().GetEnumerator());

            var dbContextMock = new Mock<DQTDataContext>();
            dbContextMock.Setup(x => x.Set<QualifiedTeacher>()).Returns(dbSetMock.Object);

            var repository = new QualifiedTeachersRepository(dbContextMock.Object);
            var qualifiedTeacherService = new QualifiedTeachersService(repository);

            _qualifiedTeacherStatusService = new QualifiedTeacherStatusService(qualifiedTeacherService, loggerMock.Object, new Authorize(configSettingsMock.Object));
        }

        [Fact]
        public async void Returns_UnauthorizedRequestResponse_WhenRequestIsUnAuthorized()
        {
            RequestInfo requestInfo = new RequestInfo()
            {
                TRN = "Test-TRN",
                NINumber = "Test-NI"
            };

            var request = CreateMockHttpRequest(requestInfo, false);
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

            var request = CreateMockHttpRequest(requestInfo);
            var response = (BadRequestObjectResult)await _qualifiedTeacherStatusService.Run(request.Object);
            var resultDto = (ResultDTO<List<QualifiedTeacherDTO>>)response.Value;

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

            var request = CreateMockHttpRequest(requestInfo);
            var response = (NotFoundObjectResult)await _qualifiedTeacherStatusService.Run(request.Object);
            var resultDto = (ResultDTO<List<QualifiedTeacherDTO>>)response.Value;

            Assert.Equal(404, response.StatusCode);
            Assert.Equal("No records found.", resultDto.Message);
        }

        [Fact]
        public async void Returns_SuccessResponseWithQualifiedTeacherRecords_WhenRequestIsValid()
        {
            RequestInfo requestInfo = new RequestInfo()
            {
                TRN = "1229708",
                NINumber = "AP558641W"
            };

            var request = CreateMockHttpRequest(requestInfo);

            var response = (OkObjectResult)await _qualifiedTeacherStatusService.Run(request.Object);
            var resultDto = (ResultDTO<List<QualifiedTeacherDTO>>)response.Value;

            Assert.Equal(200, response.StatusCode);
            Assert.Single(resultDto.Data);
        }

        [Fact]
        public async void Returns_SuccessResponseWithQualifiedTeacherRecords_WhenTrnStartsWithZeros()
        {
            var requestInfo = new RequestInfo
            {
                TRN = "0012345"
            };

            var request = CreateMockHttpRequest(requestInfo);

            var response = (OkObjectResult)await _qualifiedTeacherStatusService.Run(request.Object);
            var resultDto = (ResultDTO<List<QualifiedTeacherDTO>>)response.Value;

            Assert.Equal(200, response.StatusCode);
            Assert.Single(resultDto.Data);
        }

        [Fact]
        public async void Returns_SuccessResponseWithQualifiedTeacherRecords_WhenTrnNeedsToBePaddedWithZeros()
        {
            var requestInfo = new RequestInfo
            {
                TRN = "53100"
            };

            var request = CreateMockHttpRequest(requestInfo);

            var response = (OkObjectResult)await _qualifiedTeacherStatusService.Run(request.Object);
            var resultDto = (ResultDTO<List<QualifiedTeacherDTO>>)response.Value;

            Assert.Equal(200, response.StatusCode);
            Assert.Single(resultDto.Data);
        }

        [Fact]
        public async void Returns_SuccessResponseWithQualifiedTeacherRecords_WhenNIMatchesCaseInsensitively()
        {
            RequestInfo requestInfo = new RequestInfo()
            {
                TRN = "Test-TRN",
                NINumber = "aP558641W"
            };

            var request = CreateMockHttpRequest(requestInfo);

            var response = (OkObjectResult)await _qualifiedTeacherStatusService.Run(request.Object);
            var resultDto = (ResultDTO<List<QualifiedTeacherDTO>>)response.Value;

            Assert.Equal(200, response.StatusCode);
            Assert.Single(resultDto.Data);
        }

        private static Mock<HttpRequest> CreateMockHttpRequest(RequestInfo requestDto, bool addAuthKey = true)
        {
            var mockRequest = new Mock<HttpRequest>();

            var paramsDictionary = new Dictionary<string, StringValues> {
                { "trn", requestDto.TRN },
                { "ni", requestDto.NINumber}
            };

            mockRequest.SetupGet(x => x.Query).Returns(new QueryCollection(paramsDictionary));

            var headDictionary = new Dictionary<string, StringValues>
            {
                {"x-correlation-id",  Guid.NewGuid().ToString() }
            };

            if (addAuthKey)
            {
                headDictionary.Add("Authorization", API_KEY);
            }

            mockRequest.Setup(x => x.Headers).Returns(new HeaderDictionary(headDictionary));

            return mockRequest;
        }
    }

    public class RequestInfo
    {
        public string TRN { get; set; }
        public string NINumber { get; set; }
    }
}
