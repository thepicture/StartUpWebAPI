using StartUpWebAPI.Entities;
using StartUpWebAPI.Models;
using System;
using System.Linq;
using System.Web.Security;
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
            BgImage.ImageUrl = NativeImageUtils.ConvertFromBitmap(Properties.Resources.commonRegisterBg);
        }

        protected void CreateUser_Click(object sender, EventArgs e)
        {
            if (IsValid && IsNoSimilarUsernames())
            {
                UserBuilder uBuilder = new UserBuilder();
                uBuilder
                    .NewInstance()
                    .WithFullName(FullNameBox.Text)
                    .WithLogin(LoginBox.Text)
                    .WithPassword(PasswordBox.Text)
                    .Save();

                Response.Redirect("~/Account/Login.aspx?isregistered=true");
            }
            else
            {
                SameUsernamesMessage.Visible = true;
            }
        }

        private bool IsNoSimilarUsernames()
        {
            string username = LoginBox.Text;

            return !AppData.Context.User.Any(u => u.Login.Equals(username));
        }
    }
}