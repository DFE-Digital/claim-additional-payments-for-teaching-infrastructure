using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using System;
using dqt.api.Authorization;
using dqt.domain.Rollbar;
using dqt.domain.FileTransfer;

namespace dqt.api.Functions
{
    public class DQTFileTransferStatusService
    {

        private readonly IDQTFileTransferService _dqtFileTransferService;
        private readonly IRollbarService _log;
        private readonly IAuthorize _authorize;

        public DQTFileTransferStatusService(IDQTFileTransferService dqtFileTransferService, IRollbarService log, IAuthorize authorize)
        {
            _dqtFileTransferService = dqtFileTransferService;
            _log = log;
            _authorize = authorize;
        }

        [FunctionName("dqt-file-transfer-status-api")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "qualified-teachers/dqt-file-transfer-status")] HttpRequest req)
        {
            if (!_authorize.AuthorizeRequest(req))
            {
                _log.Warning($"Unauthorized request");

                return new UnauthorizedResult();
            }

            try
            {
                var result = await _dqtFileTransferService.GetDQTFileTransferServiceStatus();

                if(result == null)
                {
                    return new NotFoundObjectResult("No file transfer record(s) found");
                }

                return new OkObjectResult(result);
            }
            catch (Exception exception)
            {
                _log.Error(exception);
                return new ObjectResult(exception.Message) { StatusCode = 500 };
            }
        }
    }
}
