using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Windows.Forms;
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
            bool isRegisteredQuery = Convert.ToBoolean(Request.QueryString["isregistered"]);

            if (isRegisteredQuery == true)
            {
                RegSuccessMessage.Visible = true;
            }

            LoadBgImage();

            System.Web.UI.Control footer = Master.FindControl("FooterIdentity");
            footer.Visible = false;

        }

        private void LoadBgImage()
        {
            BgImage.ImageUrl = NativeImageUtils.ConvertFromBytes(Properties.Resources.commonLoginBg);
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
            HttpCookie userNameCookie = new HttpCookie("username", login)
            {
                Expires = DateTime.Now + TimeSpan.FromDays(1),
                SameSite = SameSiteMode.Lax,
            };
            Response.Cookies.Add(userNameCookie);

            FormsAuthentication.SetAuthCookie(login, isRemember);
            Response.Redirect("~/Default.aspx");
        }
    }
}