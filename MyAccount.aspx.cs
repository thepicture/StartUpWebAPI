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
            LoadBgImage(); 
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Default.aspx?reason=" + HttpUtility.UrlEncode("Вы не авторизованы. Пожалуйста, войдите в систему"));
            }

            User user = AppData.Context.User.First(u => u.Login.Equals(User.Identity.Name));

            ReloadUserImage(user);

            LabelName.Text = user.Name;
            LabelRole.Text = user.TypeOfUser.Name;
        }

        /// <summary>
        /// Reloads the user's image.
        /// </summary>
        /// <param name="user">Who is user.</param>
        private void ReloadUserImage(User user)
        {
            if (user.UserImage != null)
            {
                UserImage.ImageUrl = NativeImageUtils.ConvertFromBytes(user.UserImage);
            }
            else
            {
                UserImage.ImageUrl = NativeImageUtils.ConvertFromBitmap(Properties.Resources.noPicture);
            }
        }
        private void LoadBgImage()
        {
            BgImage.ImageUrl = NativeImageUtils.ConvertFromBitmap(Properties.Resources.bgacc);
        }


        /// <summary>
        /// Adds a new image as the user's image.
        /// </summary>
        protected void BtnAddImage_Click(object sender, EventArgs e)
        {
            if (FileUploadImage.Visible)
            {
                ChangePicture();
            }
            else
            {
                FileUploadImage.Visible = true;
            }
        }

        /// <summary>
        /// Changes the current user's image to new from FileUpload control.
        /// </summary>
        private void ChangePicture()
        {
            if (FileUploadImage.HasFile)
            {
                HttpPostedFile file = FileUploadImage.PostedFile;

                if (!file.ContentType.Contains("image"))
                {
                    return;
                }

                System.IO.Stream stream = file.InputStream;

                System.Drawing.Image image = NativeImageUtils.ConvertFromStream(stream);

                User user = AppData.Context.User.First(u => u.Login.Equals(User.Identity.Name));

                user.UserImage = NativeImageUtils.ConvertImageToBytes(image);

                try
                {
                    AppData.Context.SaveChanges();

                    AppData.Context.ChangeTracker.Entries().ToList().ForEach(e => e.Reload());

                    Response.Redirect("~/MyAccount?reason=" + HttpUtility.UrlEncode("Изображение успешно изменено!"), false);

                    ReloadUserImage(user);
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/MyAccount?reason=" + HttpUtility.UrlEncode("Не удалось изменить изображение. " +
                        "Пожалуйста, попробуйте ещё раз"));
                    System.Diagnostics.Debug.Write(ex.StackTrace);
                }
            }
        }
    }
}