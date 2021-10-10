using StartUpWebAPI.Models;
using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;

namespace StartUpWebAPI.Entities
{
    /// <summary>
    /// Public partial class for the StartUp entity.
    /// </summary>
    public partial class StartUp
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
                return string.IsNullOrWhiteSpace(Description) ? "Организатор " +
                    "не предоставил описание. " +
                    "Можете подать ему идею!" : Description;
            }
        }

        public string RestrictedDescription
        {
            get
            {
                int descLength = 128;

                if (StartUpHasDescription())
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

        private bool StartUpHasDescription()
        {
            return Description != null;
        }

        public string MyRole
        {
            get
            {
                string nullableLogin = HttpContext.Current.User.Identity.Name;

                if (!StartUpOfUser.Any(u => nullableLogin.Equals(u.User.Login)))
                {
                    return "";
                }
                else
                {
                    string role = StartUpOfUser.First(u => nullableLogin.Equals(u.User.Login))
                        .RoleType
                        .Name;

                    if (role.Equals("Организатор"))
                    {
                        return "Мой стартап";
                    }
                    else
                    {
                        return role;
                    }
                }
            }
        }

        public string IsDoneText
        {
            get
            {
                return IsDone ? "Завершён" : "Актуален";
            }
        }

        public string SpacedTitle
        {
            get
            {
                return Name.Replace(" ", "<br>");
            }
        }

        public string SplittedCategory
        {
            get
            {
                if (CategoryIsUnknown())
                {
                    return "Неизвестно";
                }

                if (CategoryNameIsTooLong())
                {
                    return string.Concat(Category.Name.Take(20)) + "...";
                }

                return Category.Name;
            }
        }

        private bool CategoryNameIsTooLong()
        {
            return Category.Name.Length > 20;
        }

        private bool CategoryIsUnknown()
        {
            return Category == null;
        }

        public string ImagePreview
        {
            get
            {
                StartUpImage imagePreview = StartUpImage.ToList().FirstOrDefault();

                if (imagePreview != null)
                {
                    return ResizingNativeImageUtils.CropImageAndGiveItAsBase64String(imagePreview.Image, 310, 310);
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
                string userName = StartUpOfUser
                    .FirstOrDefault()?
                    .User
                    .Name;

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