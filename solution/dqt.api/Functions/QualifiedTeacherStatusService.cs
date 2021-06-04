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
using System.Collections.Generic;
using dqt.datalayer.Model;
using System.Text;
using dqt.domain.DTOs;

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

            string requestReference = req.Headers["x-correlation-id"];
            if (string.IsNullOrWhiteSpace(requestReference))
            {
                requestReference = Guid.NewGuid().ToString();
            }

            _log.Info($"Received request. CorrelationId : {requestReference}");

            if (!_authorize.AuthorizeRequest(req))
            {
                _log.Warning($"Unauthorized request. CorrelationId : {requestReference}");
                return new UnauthorizedResult();
            }

            try
            {
                var trn = req.Query["trn"];
                var ni = req.Query["ni"];

                if (string.IsNullOrWhiteSpace(trn))
                {
                    var msg = $"TeacherReferenceNumber is mandatory.";
                    _log.Info($"{msg} CorrelationId : {requestReference}");
                    return new BadRequestObjectResult(GetResultDto(null, msg));
                }                 

                _log.Info($"Fetching records. CorrelationId : {requestReference}");

                var results = await _qtsService.GetQualifiedTeacherRecords(trn.ToString().Trim(), ni.ToString().Trim());

                if (!results.Any())
                {
                    var msg = $"No records found.";
                    _log.Info($"{msg} CorrelationId : {requestReference}");
                    return new NotFoundObjectResult(GetResultDto(null, msg));
                }

                return new OkObjectResult(GetResultDto(results.ToList()));
            }
            catch (JsonException jsonException)
            {
                var errorMsg = $"Error retrieving qualified teacher status data.";
                _log.Error(new JsonException($"{errorMsg} CorrelationId : {requestReference}", jsonException));
                return new BadRequestObjectResult(GetResultDto(null, errorMsg));
            }
            catch (Exception exception)
            {
                var errorMsg = $"Error retrieving qualified teacher status data. {exception.Message}";
                _log.Error(new Exception($"{errorMsg} CorrelationId : {requestReference}", exception));
                return new ObjectResult(GetResultDto(null, errorMsg)) { StatusCode = 500 };
            }

            static ResultDTO<List<QualifiedTeacherDTO>> GetResultDto(List<QualifiedTeacherDTO> data, string message = null)
            {
                return new ResultDTO<List<QualifiedTeacherDTO>>(data, message);
            }
        }

        private string Base64StringDecode(string param)
        {
            var bytes = Convert.FromBase64String(param);
            var decodedString = Encoding.UTF8.GetString(bytes);
            return decodedString;
        }

        public string Base64StringEncode(string originalString)
        {
            var bytes = Encoding.UTF8.GetBytes(originalString);
            var encodedString = Convert.ToBase64String(bytes);
            return encodedString;
        }
    }
}
