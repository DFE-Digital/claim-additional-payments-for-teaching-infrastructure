using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace dqt.api.Authorization
{
   public interface IAuthorize
    {
        bool AuthorizeRequest(HttpRequest request);
    }
}
