using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace dqt.api.Authorization
{
    public class Authorize : IAuthorize
    {
        private const string AUTH_HEADER = "Authorization";

        public bool AuthorizeRequest(HttpRequest req)
        {
            return !req.Headers.ContainsKey(AUTH_HEADER)
                || req.Headers[AUTH_HEADER] != Environment.GetEnvironmentVariable("DQTApiKey");
        }
    }
}
