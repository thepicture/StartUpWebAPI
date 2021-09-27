using StartUpWebAPI.Entities;
using System;
using System.Linq;

namespace StartUpWebAPI.Models
{
    /// <summary>
    /// Class for building users and inserting them in DbContext after.
    /// </summary>
    public class UserBuilder
    {
        private User user;

        /// <summary>
        /// Creates a new instance of a user.
        /// </summary>
        /// <returns></returns>
        public UserBuilder NewInstance()
        {
            user = new User();

            return this;
        }

        /// <summary>
        /// Appends the login.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <returns></returns>
        public UserBuilder WithLogin(string login)
        {
            user.Login = login;

            return this;
        }

        /// <summary>
        /// Appends the full name.
        /// </summary>
        /// <param name="fullName">The full name.</param>
        /// <returns></returns>
        public UserBuilder WithFullName(string fullName)
        {
            user.Name = fullName;

            return this;
        }

        /// <summary>
        /// Appends the password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public UserBuilder WithPassword(string password)
        {
            user.Password = password;

            return this;
        }

        /// <summary>
        /// Saves the builded user into DbContext.
        /// </summary>
        public void Save()
        {
            user.TypeOfUser = AppData.Context.TypeOfUser.First(t => t.Name.Equals("Пользователь"));

            AppData.Context.User.Add(user);

            try
            {
                AppData.Context.SaveChanges();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }
}