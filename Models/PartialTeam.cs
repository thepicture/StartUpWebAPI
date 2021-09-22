using StartUpWebAPI.Models;
using System;
using System.Linq;

namespace StartUpWebAPI.Entities
{
    public partial class Team
    {
        public string ImagePreview
        {
            get
            {
                string result = "data:image/jpg;base64,";

                if (Image != null)
                {
                    result += Convert.ToBase64String(Image);
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
                string userName = TeamOfUser.FirstOrDefault()?.User.Name;

                if (userName == null)
                {
                    return template + "Неизвестен";
                }
                else
                {
                    return template + userName;
                }
            }
        }
    }
}