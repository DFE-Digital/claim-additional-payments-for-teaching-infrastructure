using dqt.domain;
using Microsoft.AspNetCore.Http; 

namespace dqt.api.Authorization
{
    public class Authorize : IAuthorize
    {
        private const string AUTH_HEADER = "Authorization";
        private readonly IConfigSettings _configSettings;

        public Authorize(IConfigSettings configSettings)
        {
            _configSettings = configSettings;
        }

        public bool AuthorizeRequest(HttpRequest req)
        {
            return req.Headers.ContainsKey(AUTH_HEADER)
                && req.Headers[AUTH_HEADER] == _configSettings.DQTApiKey;
        }
    }
}
