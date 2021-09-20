using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using StartUpWebAPI.Entities;
using StartUpWebAPI.Models;

namespace StartUpWebAPI.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["isregistered"] != null)
            {
                RegSuccessMessage.Visible = true;
            }
        }

        protected void LogIn(object sender, EventArgs e)
        {
            if (IsValid)
            {
                TryToAuthorizeUser();
            }
        }

        private void TryToAuthorizeUser()
        {
            string login = LoginBox.Text;
            string password = PasswordBox.Text;

            User user = UserAuthorizer.Authorize(login, password);

            bool isUserFound = user != null;

            if (isUserFound)
            {
                RedirectToMainPage(login);
            }
            else
            {
                LogFailedMessage.Visible = true;
            }
        }

        private void RedirectToMainPage(string login)
        {
            bool isRemember = RememberMe.Checked;

            FormsAuthentication.RedirectFromLoginPage(login, isRemember);
        }
    }
}