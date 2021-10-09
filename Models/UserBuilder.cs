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

        public UserBuilder NewInstance()
        {
            user = new User();

            return this;
        }

        public UserBuilder WithLogin(string login)
        {
            user.Login = login;

            return this;
        }

        public UserBuilder WithFullName(string fullName)
        {
            user.Name = fullName;

            return this;
        }

        public UserBuilder WithPassword(string password)
        {
            user.Password = password;

            return this;
        }

        public void SaveTheUserInDbContext()
        {
            using (StartUpBaseEntities context = new StartUpBaseEntities())
            {
                user.TypeOfUser = context.TypeOfUser.First(t => t.Name.Equals("Пользователь"));

                context.User.Add(user);

                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
        }
    }
}