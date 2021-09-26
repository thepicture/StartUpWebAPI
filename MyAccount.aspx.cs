using StartUpWebAPI.Entities;
using StartUpWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StartUpWebAPI
{
    public partial class MyAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Default.aspx?reason=" + HttpUtility.UrlEncode("Вы не авторизованы. Пожалуйста, войдите в систему"));
            }

            User user = AppData.Context.User.First(u => u.Login.Equals(User.Identity.Name));

            if (user.UserImage != null)
            {
                UserImage.ImageUrl = NativeImageUtils.ConvertFromBytes(user.UserImage);
            }
            else
            {
                UserImage.ImageUrl = NativeImageUtils.ConvertFromBitmap(Properties.Resources.noPicture);
            }

            LabelName.Text = user.Name;
            LabelRole.Text = user.TypeOfUser.Name;
        }

        protected void BtnAddImage_Click(object sender, EventArgs e)
        {

        }
    }
}