using StartUpWebAPI.Entities;
using System.Collections.Generic;
using System.Linq;

namespace StartUpWebAPI.Models
{
    public class StartUpUserGetter : IUserGetter<StartUp>
    {
        public IEnumerable<User> Get(StartUp input)
        {
            return input
                .StartUpOfUser
                .Select(s => s.User);
        }
    }
}