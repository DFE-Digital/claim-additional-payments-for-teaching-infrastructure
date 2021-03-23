using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Linq;
using dqt.domain.Rollbar;
using dqt.domain.QTS;
using dqt.api.Authorization;
using dqt.api.DTOs;
using System.Collections.Generic;
using dqt.datalayer.Model;

namespace dqt.api.Functions
{
    public class QualifiedTeacherStatusService
    {

        private readonly IQualifiedTeachersService _qtsService;
        private readonly IRollbarService _log;
        private readonly IAuthorize _authorize;

        public QualifiedTeacherStatusService(IQualifiedTeachersService qtsService, IRollbarService log, IAuthorize authorize)
        {
            _qtsService = qtsService;
            _log = log;
            _authorize = authorize;
        }

        [FunctionName("qualified-teacher-status-api")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "qualified-teachers/qualified-teaching-status")] HttpRequest req)
        {
            if (!_authorize.AuthorizeRequest(req))
            {
                _log.Warning($"Unauthorized request");

                return new UnauthorizedResult();
            }

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            try
            {
                var request = JsonConvert.DeserializeObject<ExistingQualifiedTeacherRequestDTO>(requestBody);

                if (request == null || string.IsNullOrWhiteSpace(request.TRN))
                {
                    return new BadRequestObjectResult(GetResultDto(null, "TeacherReferenceNumber is mandatory"));
                }

                _log.Info($"Fetching records for TRN number {request.TRN} and NI number {request.NINumber}");

                var results = await _qtsService.GetQualifiedTeacherRecords(request.TRN, request.NINumber);

                if (!results.Any())
                {
                    _log.Info($"No records found for TRN number {request.TRN} and NI number {request.NINumber}");

                    return new NotFoundObjectResult(GetResultDto(null, "No records found"));
                }

                return new OkObjectResult(GetResultDto(results.ToList()));
            }
            catch (JsonException jsonException)
            {
                _log.Error(jsonException);

                return new BadRequestObjectResult(GetResultDto(null, "Bad request"));
            }
            catch (Exception exception)
            {
                _log.Error(exception);

                return new ObjectResult(GetResultDto(null, exception.Message)) { StatusCode = 500 };
            }

            static ResultDTO<List<QualifiedTeacher>> GetResultDto(List<QualifiedTeacher> data, string message = null)
            {
                return new ResultDTO<List<QualifiedTeacher>>(data, message);
            }
        }
    }
}
