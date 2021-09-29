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
        public string SafeDescription
        {
            get
            {
                return string.IsNullOrWhiteSpace(Description) ? "Организатор не предоставил описание. Можете подать ему идею!" : Description;
            }
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
                    string role = StartUpOfUser.First(u => nullableLogin.Equals(u.User.Login)).RoleType.Name;

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
                string[] words = Category?.Name.Split(' ');

                if (words == null)
                {
                    return "Неизвестно";
                }

                if (words.Length > 2)
                {
                    return words[0] + " " + words[1] + "...";
                }
                else
                {
                    StringBuilder result = new StringBuilder();

                    foreach (var word in words)
                    {
                        result.Append(word + " ");
                    }

                    return result.ToString();
                }
            }
        }
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