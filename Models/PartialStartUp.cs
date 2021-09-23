using StartUpWebAPI.Models;
using System;
using System.Drawing;
using System.Linq;

namespace StartUpWebAPI.Entities
{
    /// <summary>
    /// Public partial class for the StartUp entity.
    /// </summary>
    public partial class StartUp
    {
        public string ImagePreview
        {
            get
            {
                StartUpImage imagePreview = StartUpImage.ToList().FirstOrDefault();
                string result = "data:image/jpg;base64,";

                if (imagePreview != null)
                {
                    result += Convert.ToBase64String(imagePreview.Image);
                }
                else
                {
                    result = NativeImageUtils.ConvertFromBytes(Properties.Resources.noPicture);
                }

                return result;
            }
        }
        public string FirstCreator
        {
            get
            {
                string template = "Главный организатор: ";
                string userName = StartUpOfUser.FirstOrDefault()?.User.Name;

                if (userName == null)
                {
                    return template + "Неизвестен";
                }
                else
                {
                    string[] credentials = userName.Split(' ');
                    string lastName = credentials[0];
                    string firstName = credentials[1].ToCharArray().ElementAt(0) + ".";
                    return template + lastName + " " + firstName;
                }
            }
        }
    }
}