using StartUpWebAPI.Entities;
using StartUpWebAPI.Models;
using System;
using System.Linq;
using System.Web.UI;

namespace StartUpWebAPI.Account
{
    public partial class Register : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadBgImage();
            RemoveFooter();
        }

        private void RemoveFooter()
        {
            Control footer = Master.FindControl("FooterIdentity");
            footer.Visible = false;
        }

        private void LoadBgImage()
        {
            BgImage.ImageUrl = NativeImageUtils.ConvertFromBitmap(Properties.Resources.loginRegisterBg);
        }

        protected void CreateUser_Click(object sender, EventArgs e)
        {
            if (IsValid && IsNoSimilarUsernames())
            {
                BuildUser();
                Response.Redirect("~/Account/Login.aspx?isregistered=true");
            }
            else
            {
                SameUsernamesMessage.Visible = true;
            }
        }

        private void BuildUser()
        {
            UserBuilder uBuilder = new UserBuilder();
            uBuilder
                .NewInstance()
                .WithFullName(FullNameBox.Text)
                .WithLogin(LoginBox.Text)
                .WithPassword(PasswordBox.Text)
                .SaveTheUserInDbContext();
        }

        private bool IsNoSimilarUsernames()
        {
            string username = LoginBox.Text;

            using (StartUpBaseEntities context = new StartUpBaseEntities())
            {
                return !context.User.Any(u => u.Login.Equals(username));
            }
        }
    }
}