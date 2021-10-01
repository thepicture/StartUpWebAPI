using StartUpWebAPI.Models;
using System;
using System.Linq;
using System.Web;

namespace StartUpWebAPI.Entities
{
    public partial class Team
    {
        public string RegionText
        {
            get
            {
                string regionId = RegionId?.ToString();
                return int.TryParse(regionId, out int _) ? Region.Name : "Регион не указан";
            }
        }
        public string SafeDescription
        {
            get
            {
                return string.IsNullOrEmpty(Description) ? "Организатор не предоставил описание. Можете подать ему идею!" : Description;
            }
        }
        public string MyRole
        {
            get
            {
                string nullableLogin = HttpContext.Current.User.Identity.Name;

                if (!TeamOfUser.Any(u => nullableLogin.Equals(u.User.Login)))
                {
                    return "";
                }
                else
                {
                    string role = TeamOfUser.First(u => nullableLogin.Equals(u.User.Login)).RoleType.Name;

                    if (role.Equals("Организатор"))
                    {
                        return "Моя команда";
                    }
                    else
                    {
                        return role;
                    }
                }
            }
        }

        public string CountOfMembers
        {
            get
            {
                return TeamOfUser.Count().ToString();
            }
        }
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
                    result = NativeImageUtils.ConvertFromBitmap(Properties.Resources.noPicture);
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