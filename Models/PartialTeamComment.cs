using StartUpWebAPI.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace StartUpWebAPI.Entities
{
    public partial class TeamComment
    {
        public string IsMemberText
        {
            get
            {
                using (StartUpBaseEntities context = new StartUpBaseEntities())
                {
                    bool isMember = context.TeamOfUser.Any(s => s.UserId == User.Id
                    && s.RoleType.Name.Equals("Участник")
                    && s.TeamId == TeamId);
                    bool isNotBanned = !context.TeamOfUser.Any(s => s.UserId == User.Id
                    && s.RoleType.Name.Equals("Забанен")
                    && s.TeamId == Team.Id);

                    if (!isNotBanned)
                    {
                        return "Забанен";
                    }

                    return isMember ? "Участник" : "Гость";
                }
            }
        }
        public string ChangeUserRoleTypeText
        {
            get
            {
                using (StartUpBaseEntities context = new StartUpBaseEntities())
                {
                    bool userIsHelper = Team.TeamOfUser.Any(s => s.User.Login.Equals(User.Login)
                    && s.RoleType.Name.Equals("Помощник")
                    && s.TeamId == Team.Id);
                    bool isNotBanned = !context.TeamOfUser.Any(s => s.UserId == User.Id
                    && s.RoleType.Name.Equals("Забанен")
                    && s.TeamId == Team.Id);

                    if (userIsHelper)
                    {
                        return "Убрать статус помощника";
                    }
                    else
                    {
                        return "Назначить помощником";
                    }
                }
            }
        }
        public bool IsNotSelfAndIAmOrganizerAndUserIsMember
        {
            get
            {
                using (StartUpBaseEntities context = new StartUpBaseEntities())
                {
                    bool isBanned = context.TeamOfUser.Any(s => s.UserId == User.Id
                    && s.RoleType.Name.Equals("Забанен")
                    && s.TeamId == Team.Id);

                    if (isBanned) return false;

                    bool isNotSelf = !User.Login.Equals(HttpContext.Current.User.Identity.Name);
                    bool iAmAOrganizer = Team.TeamOfUser.Any(s => s.User.Login.Equals(HttpContext.Current.User.Identity.Name)
                        && s.RoleType.Name.Equals("Организатор")
                        && s.TeamId == Team.Id);
                    bool userIsMember = context.TeamOfUser.Any(s => s.UserId == User.Id
                    && s.RoleType.Name.Equals("Участник")
                    && s.TeamId == Team.Id);

                    return isNotSelf && iAmAOrganizer && userIsMember;
                }
            }
        }
        public bool IsNotSelfCommentAndICanChange
        {
            get
            {
                return !User.Login.Equals(HttpContext.Current.User.Identity.Name)
                    && Team.TeamOfUser.Any(s => s.User.Login.Equals(HttpContext.Current.User.Identity.Name)
                    && (s.RoleType.Name.Equals("Организатор") || s.RoleType.Name.Equals("Помощник"))
                    && s.TeamId == Team.Id);
            }
        }

        public bool ICanChange
        {
            get
            {
                return User.Login.Equals(HttpContext.Current.User.Identity.Name) || Team.TeamOfUser
                    .Any(s => s.User.Login.Equals(HttpContext.Current.User.Identity.Name)
                    && (s.RoleType.Name.Equals("Организатор") || s.RoleType.Name.Equals("Помощник"))
                    && s.TeamId == Team.Id);
            }
        }

        public string BanUserText
        {
            get
            {
                bool isBanned = Team.TeamOfUser.Any(s => s.User.Login.Equals(User.Login)
                                   && s.RoleType.Name.Equals("Забанен")
                                       && s.TeamId == Team.Id
                                       && s.TeamId == Team.Id);
                if (isBanned)
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