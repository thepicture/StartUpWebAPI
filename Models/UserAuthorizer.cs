using StartUpWebAPI.Entities;
using System.Linq;

namespace StartUpWebAPI.Models
{
    /// <summary>
    /// Class for authorizing the user into DbContext used by FormsAuthentication.
    /// </summary>
    public class UserAuthorizer
    {
        /// <summary>
        /// Returns the user from DbContext with given parameters.
        /// </summary>
        /// <param name="login">The user login.</param>
        /// <param name="password">The user password.</param>
        /// <returns></returns>
        public static User Authorize(string login, string password)
        {
            User user = AppData.Context.User.FirstOrDefault(u => u.Login.Equals(login) && u.Password.Equals(password));

            return user;
        }
    }
}