using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using dqt.domain;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace dqt.api
{
    public class QualifiedTeacherStatusService
    {
        private const string AUTH_HEADER = "Authorization";
        private readonly IQualifiedTeachersService _qtsService;

        public QualifiedTeacherStatusService(IQualifiedTeachersService qtsService)
        { 
            _qtsService = qtsService;
        }

        [FunctionName("qualified-teacher-status")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "qualified-teachers/qualified-teaching-status")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            if (!req.Headers.ContainsKey(AUTH_HEADER)
                || req.Headers[AUTH_HEADER] != Environment.GetEnvironmentVariable("APIKey"))
            {
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

                var results = await _qtsService.GetQualifiedTeacherRecords(request.TRN, request.NINumber);

                if (!results.Any())
                {
                    return new NotFoundObjectResult("No records found");
                }

                return new OkObjectResult(results);
            }
            catch (JsonException)
            {
                return new BadRequestObjectResult("Bad request data");
            }
            catch(Exception ex)
            {
                return new ObjectResult(ex.Message) { StatusCode = 500 };
            }
        }
    }
}
