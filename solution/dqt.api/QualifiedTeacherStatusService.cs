using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Linq;
using dqt.datalayer;
using dqt.api.Repository;

namespace dqt.api
{
    public class QualifiedTeacherStatusService
    {
        private readonly IRepository _repository;

        public QualifiedTeacherStatusService(IRepository repository)
        {
            _repository = repository;
        }

        [FunctionName("qualified-teacher-status")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "qualified-teachers/qualified-teaching-status")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var results = _repository.GetQualifiedTeacherRecords();
            return new JsonResult(results);
        }
    }
}
