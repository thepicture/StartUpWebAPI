﻿using StartUpWebAPI.Entities;
using System;
using System.Linq;

namespace StartUpWebAPI.Models
{
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
                Console.WriteLine(ex.Message);
            }
        }
    }
}