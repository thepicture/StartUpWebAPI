using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace StartUpWebAPI.Models
{
    public class UserAuthorizeObserver
    {
        public static bool IsAuthorized(HttpRequest request)
        {
            return request.Cookies[".ASPXAUTH"]?.Value != null;
        }
    }
}