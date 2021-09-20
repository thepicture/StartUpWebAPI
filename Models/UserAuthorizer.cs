using StartUpWebAPI.Entities;
using System.Linq;

namespace StartUpWebAPI.Models
{
    public class UserAuthorizer
    {
        public static User Authorize(string login, string password)
        {
            User user = AppData.Context.User.FirstOrDefault(u => u.Login.Equals(login) && u.Password.Equals(password));

            return user;
        }
    }
}