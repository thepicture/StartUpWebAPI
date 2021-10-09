using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace StartUpWebAPI.Entities
{
    public partial class StartUpComment
    {
        public string IsMemberText
        {
            get
            {
                using (StartUpBaseEntities context = new StartUpBaseEntities())
                {
                    bool isMember = context.StartUpOfUser.Any(s => s.UserId == User.Id
                    && s.RoleType.Name.Equals("Участник")
                    && s.StartUpId == StartUp.Id);
                    bool isNotBanned = !context.StartUpOfUser.Any(s => s.UserId == User.Id
                    && s.RoleType.Name.Equals("Забанен")
                    && s.StartUpId == StartUp.Id);

                    if (!isNotBanned)
                    {
                        return "Забанен";
                    }

                    return isMember && isNotBanned ? "Участник" : "Гость";
                }
            }
        }
        public string ChangeUserRoleTypeText
        {
            get
            {
                bool isHelper = StartUp.StartUpOfUser.Any(s => s.User.Login.Equals(User.Login)
                                   && s.RoleType.Name.Equals("Помощник")
                                   && s.StartUpId == StartUp.Id);
                if (isHelper)
                {
                    return "Убрать статус помощника";
                }
                else
                {
                    return "Назначить помощником";
                }
            }
        }
        public bool IsNotSelfAndIAmOrganizerAndUserIsMember
        {
            get
            {
                using (StartUpBaseEntities context = new StartUpBaseEntities())
                {
                    bool isNotSelf = !User.Login.Equals(HttpContext.Current.User.Identity.Name);
                    bool iAmAOrganizer = StartUp
                        .StartUpOfUser.Any(s => s.User.Login.Equals(HttpContext.Current.User.Identity.Name)
                        && s.RoleType.Name.Equals("Организатор")
                        && s.StartUpId == StartUp.Id);
                    bool userIsMember = context.StartUpOfUser.Any(s => s.UserId == User.Id
                    && s.RoleType.Name.Equals("Участник")
                    && s.StartUpId == StartUp.Id);

                    return isNotSelf && iAmAOrganizer && userIsMember;
                }
            }
        }
        public bool IsNotSelfCommentAndICanChange
        {
            get
            {
                return !User.Login.Equals(HttpContext.Current.User.Identity.Name)
                    && StartUp
                    .StartUpOfUser.Any(s => s.User.Login.Equals(HttpContext.Current.User.Identity.Name)
                    && (s.RoleType.Name.Equals("Организатор") || s.RoleType.Name.Equals("Помощник"))
                    && s.StartUpId == StartUp.Id);
            }
        }

        public bool ICanChange
        {
            get
            {
                return User.Login.Equals(HttpContext.Current.User.Identity.Name) || StartUp
                    .StartUpOfUser.Any(s => s.User.Login.Equals(HttpContext.Current.User.Identity.Name)
                    && (s.RoleType.Name.Equals("Организатор") || s.RoleType.Name.Equals("Помощник"))
                    && s.StartUpId == StartUp.Id);
            }
        }

        public string BanUserText
        {
            get
            {
                bool userIsBanned = StartUp.StartUpOfUser.Any(s => s.User.Login.Equals(User.Login)
                                   && s.RoleType.Name.Equals("Забанен")
                                    && s.StartUpId == StartUp.Id);

                if (userIsBanned)
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
                return User.UserImageInCommentOrDefault;
            }
        }
    }
}