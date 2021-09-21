using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StartUpWebAPI.Models
{
    public class CookieUtils
    {
        public static string GetUserNameOrNullOf(HttpRequest request)
        {
            return request.Cookies.Get("username")?.Value;
        }
    }
}