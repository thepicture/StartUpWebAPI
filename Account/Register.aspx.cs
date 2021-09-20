using StartUpWebAPI.Models;
using System;
using System.Web.UI;

namespace StartUpWebAPI.Account
{
    public partial class Register : Page
    {
        protected void CreateUser_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                UserBuilder uBuilder = new UserBuilder();
                uBuilder
                    .NewInstance()
                    .WithFullName(FullNameBox.Text)
                    .WithLogin(LoginBox.Text)
                    .WithPassword(PasswordBox.Text)
                    .Save();
            }
        }
    }
}