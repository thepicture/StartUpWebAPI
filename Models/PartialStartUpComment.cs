using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace StartUpWebAPI.Entities
{
    public partial class StartUpComment
    {
        public bool IsNotSelfCommentAndICanChange
        {
            get
            {
                return !User.Login.Equals(HttpContext.Current.User.Identity.Name)
                    && StartUp
                    .StartUpOfUser.Any(s => s.User.Login.Equals(HttpContext.Current.User.Identity.Name)
                    && (s.RoleType.Name.Equals("Организатор") || s.RoleType.Name.Equals("Помощник")));
            }
        }

        public bool ICanChange
        {
            get
            {
                return User.Login.Equals(HttpContext.Current.User.Identity.Name) || StartUp
                    .StartUpOfUser.Any(s => s.User.Login.Equals(HttpContext.Current.User.Identity.Name)
                    && (s.RoleType.Name.Equals("Организатор") || s.RoleType.Name.Equals("Помощник")));
            }
        }

        public string BanUserText
        {
            get
            {
                if (StartUp.StartUpOfUser.Any(s => s.User.Login.Equals(User.Login) && s.RoleType.Name.Equals("Забанен")))
                {
                    return "Разбанить комментатора";
                }
                else
                {
                    return "Ограничить доступ комментатора к стартапу";
                }
            }
        }

        public string GetCommentImage
        {
            get
            {
                string result = "data:image/jpg;base64,";

                if (User.UserImage != null)
                {
                    result += Convert.ToBase64String(User.UserImage);
                }
                else
                {
                    ImageConverter converter = new ImageConverter();
                    Bitmap noPicture = Properties.Resources.noPicture;
                    result += Convert.ToBase64String((byte[])converter.ConvertTo(noPicture, typeof(byte[])));
                }

                return result;
            }
        }
    }
}