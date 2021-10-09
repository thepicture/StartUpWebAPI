using StartUpWebAPI.Entities;
using System.Linq;

namespace StartUpWebAPI.Models
{
    public class UserAuthorizer
    {
        public static User GetUserFromDbContextAndAuthorize(string login, string password)
        {
            using (StartUpBaseEntities context = new StartUpBaseEntities())
            {
                User user = context.User.FirstOrDefault(u => u.Login.Equals(login) && u.Password.Equals(password));

                return user;
            }
        }
    }
}