using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using dqt.domain;
using Newtonsoft.Json;
using System;
using System.Linq;
using dqt.api.Authorization;
using dqt.domain.Rollbar;

namespace dqt.api
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
            _log.Configure(Environment.GetEnvironmentVariable("DQTAPIRollbarEnvironemnt"));
        }

        [FunctionName("qualified-teacher-status")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "qualified-teachers/qualified-teaching-status")] HttpRequest req)
        {
            if (!_authorize.AuthorizeRequest(req))
            {
                _log.Warning($"Unauthorized request");
                return new UnauthorizedResult();
            }

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            try
            {
                var request = JsonConvert.DeserializeObject<ExistingQualifiedTeacherRequest>(requestBody);

                if (request == null || string.IsNullOrWhiteSpace(request.TRN))
                {
                    return new BadRequestObjectResult("TeacherReferenceNumber is mandatory");
                }

                _log.Info($"Fetching records for TRN number {request.TRN} and NI number {request.NINumber}");

                var results = await _qtsService.GetQualifiedTeacherRecords(request.TRN, request.NINumber);

                if (!results.Any())
                {
                    _log.Info($"No records found for TRN number {request.TRN} and NI number {request.NINumber}");
                    return new NotFoundObjectResult("No records found");
                }

                return new OkObjectResult(results.ToList());
            }
            catch (JsonException jsonException)
            {
                _log.Error(jsonException);
                return new BadRequestObjectResult("Bad request data");
            }
            catch(Exception exception)
            {
                _log.Error(exception);
                return new ObjectResult(exception.Message) { StatusCode = 500 };
            }
        }
    }
}
