using StartUpWebAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StartUpWebAPI.Models
{
    public class UserDistinctor
    {
        public static IEnumerable<User> GetDistinctUsers(IEnumerable<User> users)
        {
            return users.Distinct();
        }
    }
}