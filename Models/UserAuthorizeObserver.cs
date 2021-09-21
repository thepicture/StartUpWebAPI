using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StartUpWebAPI.Models
{
    public class UserAuthorizeObserver
    {
        public static bool IsAuthorized(HttpRequest request)
        {
            return request.Cookies.Get("username") != null;
        }
    }
}