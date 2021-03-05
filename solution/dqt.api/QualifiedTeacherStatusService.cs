using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using dqt.domain;
using Newtonsoft.Json;

namespace dqt.api
{
    public class QualifiedTeacherStatusService
    {
        private readonly IQualifiedTeachersService qtsService;

        public QualifiedTeacherStatusService(IQualifiedTeachersService qtsService)
        { 
            this.qtsService = qtsService;
        }

        [FunctionName("qualified-teacher-status")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "qualified-teachers/qualified-teaching-status")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            dynamic data = JsonConvert.DeserializeObject(requestBody);

            var results = await qtsService.GetQualifiedTeacherRecords("TRN1234", "NI1234");
            return new JsonResult(results);
        }
    }
}
