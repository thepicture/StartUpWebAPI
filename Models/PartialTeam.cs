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

        public string RestrictedDescription
        {
            get
            {
                int descLength = 128;

                if (TeamHasDescription())
                {
                    if (DescriptionIsTooLong(descLength))
                    {
                        return string.Concat(Description.Take(descLength)) + "...";
                    }
                    return Description;
                }
                return "Организатор не предоставил описание. Можете подать ему идею!";
            }
        }

        private bool DescriptionIsTooLong(int descLength)
        {
            return Description.Length > descLength;
        }

        private bool TeamHasDescription()
        {
            return Description != null;
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
                if (Image != null)
                {
                    return ResizingNativeImageUtils.CropImageAndGiveItAsBase64String(Image, 310, 310);
                }
                else
                {
                    return NativeImageUtils.ConvertFromBitmap(Properties.Resources.noPicture);
                }
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