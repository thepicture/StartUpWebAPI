using StartUpWebAPI.Entities;
using System.Collections.Generic;
using System.Linq;

namespace StartUpWebAPI.Models
{
    public class TeamUserGetter : IUserGetter<Team>
    {
        public IEnumerable<User> Get(Team input)
        {
            return input
                .TeamOfUser
                .Select(s => s.User);
        }
    }
}