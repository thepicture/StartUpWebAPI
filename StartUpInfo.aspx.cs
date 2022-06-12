using StartUpWebAPI.Entities;
using StartUpWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace StartUpWebAPI
{
    public partial class StartUpInfo : Page
    {
        public StartUp startUp;

        protected void Page_Load(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Request.QueryString.Get("id"));

            using (StartUpBaseEntities context = new StartUpBaseEntities())
            {
                bool isStartUp = context.StartUp.Any(s => s.Id == id);

                if (!isStartUp)
                {
                    string reason = HttpUtility
                        .UrlEncode("Стартап не существует или был удалён. " +
                        "Пожалуйста, найдите другой стартап.");
                    Response.Redirect("~/Default.aspx?reason=" + reason, false);
                    return;
                }

                startUp = context.StartUp.Find(id);

                bool userIsBanned = startUp.StartUpOfUser
                    .Any(s => s.User.Login.Equals(User.Identity.Name)
                               && s.RoleType.Name.Equals("Забанен")
                               && s.StartUpId == startUp.Id);

                if (userIsBanned)
                {
                    Response
                        .Redirect("~/Default.aspx?reason="
                        + HttpUtility
                        .UrlEncode("К сожалению, доступ к данному сообществу " +
                        "для вас ограничен. "
                        + "Пожалуйста, найдите другие сообщества."), false);
                    return;
                }

                bool userIsHelperOrOrganizer = false;
                bool isStartUpExists = startUp != null;

                if (isStartUpExists)
                {
                    userIsHelperOrOrganizer = startUp
                        .StartUpOfUser
                        .Any(u => u.User.Login.Equals(User.Identity.Name)
                        && (u.RoleType.Name.Equals("Организатор")
                        || u.RoleType.Name.Equals("Помощник")));
                }

                if (userIsHelperOrOrganizer)
                {
                    LinkButtonModifyStartUp.Visible = true;
                }
                else
                {
                    ShowNeedyButtonsForMember();
                }

                InsertComments();
                InsertStartUp();
                InsertUsersFlow();
                ShowDeleteStartUpButtonIfOrganizer();

                if (UserIsNotInTeamAndValueOfTeamWasChanged())
                {
                    InsertTeams();
                }
            }
        }

        private void ShowDeleteStartUpButtonIfOrganizer()
        {
            BtnDeleteStartUp.Visible = startUp.StartUpOfUser.Any(s => s.StartUpId == startUp.Id
                            && s.User.Login == User.Identity.Name
                            && s.RoleType.Name.Equals("Организатор"));
        }

        private bool UserIsNotInTeamAndValueOfTeamWasChanged()
        {
            return !IsPostBack && !IsUserInStartUp();
        }

        private void InsertTeams()
        {
            using (StartUpBaseEntities context = new StartUpBaseEntities())
            {
                SetTeamsOfUserInDropDownTeams(context);
                DropDownTeams.DataBind();

                DropDownTeams.Visible = true;
            }
        }

        private void SetTeamsOfUserInDropDownTeams(StartUpBaseEntities context)
        {
            List<string> teams = new List<string>();
            teams.Insert(0, "Вступить как пользователь");
            teams.AddRange(GetTeamsFromContext(context));

            DropDownTeams.DataSource = teams;
        }

        private List<string> GetTeamsFromContext(StartUpBaseEntities context)
        {
            return context.TeamOfUser.Where(t => t
                        .User
                        .Login
                        .Equals(User.Identity.Name))
                            .Select(t => t.Team.Name)
                            .Distinct()
                            .ToList()
                            .Except(startUp.StartUpOfTeam.Select(s => s.Team.Name))
                            .ToList();
        }

        private void InsertUsersFlow()
        {
            using (StartUpBaseEntities context = new StartUpBaseEntities())
            {
                IEnumerable<User> usersInput = new StartUpUserGetter().Get(startUp);

                List<User> users = UsersFlowPreparator
                    .PrepareAndGetInfiniteUsers(usersInput).Where(u => u.UserImage != null)
                    .ToList();

                LViewUsersFlow.DataSource = users;
                LViewUsersFlow.DataBind();
            }
        }

        /// <summary>
        /// Redirects to the edit startup page.
        /// </summary>
        protected void LinkButtonModifyStartUp_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AddStartUp?id=" + startUp.Id, false);
        }

        /// <summary>
        /// If an user in the startup, then the method shows appropriate buttons for him.
        /// </summary>
        private void ShowNeedyButtonsForMember()
        {
            bool userInStartUp = IsUserInStartUp();

            if (userInStartUp)
            {
                BtnUnsubscribe.Visible = true;
                LinkGoToContact.Visible = true;
                LinkGoToContact.PostBackUrl = startUp.ContactLink;
            }
            else
            {
                ShowSubscribeButtonIfNotMaxMembersCount();
            }
        }

        private bool IsUserInStartUp()
        {
            return startUp.StartUpOfUser
                .Any(u => u.User.Login.ToLower().Equals(User.Identity.Name.ToLower()));
        }

        /// <summary>
        /// Shows the subscribe button, if the max members count does not equal to the current members count.
        /// </summary>
        private void ShowSubscribeButtonIfNotMaxMembersCount()
        {
            bool isMaxMembersCount = startUp.MaxMembersCount <= startUp.StartUpOfUser.Count
                + startUp.StartUpOfTeam.Count;

            if (isMaxMembersCount)
            {
                return;
            }

            BtnSubscribe.Visible = true;
        }

        /// <summary>
        /// Inserts comments into the page.
        /// </summary>
        private void InsertComments()
        {
            LViewStartUpComments.DataSource = startUp.StartUpComment
                .OrderByDescending(c => c.DateTime)
                .ToList();
            LViewStartUpComments.DataBind();
        }

        /// <summary>
        /// Fulfills the current startup into the view.
        /// </summary>
        private void InsertStartUp()
        {
            Name.Text = MainName.Text = startUp.Name;
            CountOfMembers.Text = startUp.StartUpOfUser.Count.ToString();
            CountOfTeams.Text = startUp.StartUpOfTeam.Count.ToString();

            string creator = startUp.StartUpOfUser
                .FirstOrDefault(u => u.RoleType.Name == "Организатор")?
                .User.Name;

            Creator.Text = string.IsNullOrWhiteSpace(creator) ? "Неизвестен" : creator;
            IsActual.Text = startUp.IsDoneText;
            DateOfCreation.Text = startUp.CreationDate.ToString();
            Category.Text = startUp.Category.Name;
            Description.Text = startUp.SafeDescription;
            UpdateCommentsCount();
            UpdateUsersCount();
            LViewStartUpImages.DataSource = startUp.StartUpImage.ToList();
            LViewStartUpImages.DataBind();
            MaxMembersCount.Text = startUp.MaxMembersCount.ToString();
            Region.Text = startUp.RegionText;
        }

        private void UpdateUsersCount()
        {
            UsersCount.Text = "Участники ("
                + UserDistinctor.GetDistinctUsers(new StartUpUserGetter().Get(startUp))
                .Count() + "):";
        }

        /// <summary>
        /// Updates comments count.
        /// </summary>
        private void UpdateCommentsCount()
        {
            CommentsCount.Text = "Комментарии (" + startUp.StartUpComment.Count + "):";
        }

        /// <summary>
        /// Pre-actions for sending comment.
        /// </summary>
        protected void BtnSendComment_Click(object sender, EventArgs e)
        {
            string errors = "";

            bool noCommentPresented = string.IsNullOrWhiteSpace(CommentBox.Text);

            if (noCommentPresented)
            {
                errors += "Пожалуйста, введите непустой комментарий";
            }

            bool hasAnyErrors = errors.Length > 0;

            if (hasAnyErrors)
            {
                Response.Redirect("~/StartUpInfo.aspx?id=" + startUp.Id + "&reason="
                      + HttpUtility.UrlEncode(errors));
                return;
            }

            string username = User.Identity.Name;

            DateTime? lastComment = startUp.StartUpComment
                .Where(c => c.User.Login.Equals(username))?
                .OrderByDescending(c => c.DateTime)
                .FirstOrDefault()?
                .DateTime;

            bool hasLastComment = lastComment != null;

            if (hasLastComment)
            {
                if (AntiSpamChecker.IsLastCommentRecentThat(5, lastComment.Value))
                {
                    Response.Redirect("~/StartUpInfo.aspx?id=" + startUp.Id + "&reason="
                        + HttpUtility.UrlEncode("Нельзя отправлять комментарии слишком часто"));
                    return;
                }
            }

            User currentUser = null;

            using (StartUpBaseEntities context = new StartUpBaseEntities())
            {
                currentUser = context.User.First(u => u.Login.Equals(username));
            }

            if (UserExists(currentUser))
            {
                SendUserComment(currentUser);
            }
        }

        private static bool UserExists(User currentUser)
        {
            return currentUser != null;
        }

        /// <param name="currentUser">Who is sending the comment.</param>
        private void SendUserComment(User currentUser)
        {
            using (StartUpBaseEntities context = new StartUpBaseEntities())
            {
                StartUpComment comment = new StartUpComment
                {
                    CommentText = CommentBox.Text,
                    DateTime = DateTime.Now,
                    UserId = currentUser.Id,
                    StartUpId = startUp.Id,
                };

                context.StartUp.Find(startUp.Id).StartUpComment.Add(comment);

                try
                {
                    context.SaveChanges();

                    context.ChangeTracker.Entries().ToList().ForEach(e => e.Reload());

                    Response.Redirect("~/StartUpInfo.aspx?id=" + startUp.Id + "&reason="
                        + HttpUtility.UrlEncode("Комментарий успешно добавлен!"), false);

                    CommentBox.Text = null;
                    InsertComments();
                    UpdateCommentsCount();
                }
                catch (Exception ex)
                {
                    string reason = HttpUtility.UrlEncode("Не удалось написать комментарий. " +
                        "Попробуйте, пожалуйста, ещё раз.");
                    System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                    Response.Redirect("~/StartUpInfo?id=" + startUp.Id + "&reason=" + reason);
                }
            }

            MaintainScrollPositionOnPostBack = true;
        }

        /// <summary>
        /// Unsubscribes the user from the startup.
        /// </summary>
        protected void BtnUnsubscribe_Click(object sender, EventArgs e)
        {
            using (StartUpBaseEntities context = new StartUpBaseEntities())
            {
                string reason;
                StartUpOfUser currentStartup = FindStartUp();
                context.Entry(currentStartup).State = EntityState.Deleted;

                try
                {
                    context.SaveChanges();
                    reason = HttpUtility.UrlEncode("Вы успешно покинули стартап");
                    Response.Redirect("~/StartUpInfo?id=" + startUp.Id + "&reason=" + reason, false);
                }
                catch (Exception)
                {
                    reason = HttpUtility.UrlEncode("Не удалось покинуть стартап. Попробуйте, пожалуйста, ещё раз.");
                    Response.Redirect("~/StartUpInfo?id=" + startUp.Id + "&reason=" + reason, false);

                    return;
                }
            }
        }

        /// <summary>
        /// Finds a startup in the db.
        /// </summary>
        /// <returns>The found startup.</returns>
        private StartUpOfUser FindStartUp()
        {
            using (StartUpBaseEntities context = new StartUpBaseEntities())
            {
                return context.StartUpOfUser
                    .First(s => s.User.Login.Equals(User.Identity.Name)
                             && s.StartUpId == startUp.Id);
            }
        }

        /// <summary>
        /// Subscribes the user to the startup.
        /// </summary>
        protected void BtnSubscribe_Click(object sender, EventArgs e)
        {
            if (IsUserSubscribingAsTeam())
            {
                SubscribeAsTeam();
            }
            else
            {
                SubscribeAsStandaloneUser();
            }
        }

        private void SubscribeAsStandaloneUser()
        {
            using (StartUpBaseEntities context = new StartUpBaseEntities())
            {
                string reason;

                StartUpOfUser startUpOfUser = new StartUpOfUser
                {
                    StartUpId = startUp.Id,
                    UserId = context.User.First(u => u.Login.Equals(User.Identity.Name)).Id,
                    RoleTypeId = context.RoleType.First(r => r.Name.Equals("Участник")).Id
                };

                context.StartUp.Find(startUp.Id).StartUpOfUser.Add(startUpOfUser);

                try
                {
                    context.SaveChanges();
                    reason = HttpUtility.UrlEncode("Вы успешно вступили в стартап");
                    Response.Redirect("~/StartUpInfo?id=" + startUp.Id + "&reason=" + reason, false);
                }
                catch (Exception ex)
                {
                    reason = HttpUtility.UrlEncode("Не удалось вступить в стартап. Попробуйте, пожалуйста, ещё раз.");
                    Response.Redirect("~/StartUpInfo?id=" + startUp.Id + "&reason=" + reason, false);
                    System.Diagnostics.Debug.WriteLine(ex.StackTrace);

                    return;
                }
            }
        }

        private void SubscribeAsTeam()
        {
            using (StartUpBaseEntities context = new StartUpBaseEntities())
            {
                string reason;

                StartUpOfTeam startUpOfTeam = new StartUpOfTeam
                {
                    StartUpId = startUp.Id,
                    TeamId = context.Team.First(t => t.Name.Equals(DropDownTeams.SelectedValue)).Id,
                    RoleTypeId = context.RoleType.First(r => r.Name.Equals("Участник")).Id
                };

                context.StartUp.Find(startUp.Id).StartUpOfTeam.Add(startUpOfTeam);

                try
                {
                    context.SaveChanges();
                    reason = HttpUtility.UrlEncode("Вы успешно вступили в стартап от имени команды " + DropDownTeams.SelectedValue);
                    Response.Redirect("~/StartUpInfo?id=" + startUp.Id + "&reason=" + reason, false);
                }
                catch (Exception ex)
                {
                    reason = HttpUtility.UrlEncode("Не удалось вступить в стартап от имени команды. Попробуйте, пожалуйста, ещё раз.");
                    Response.Redirect("~/StartUpInfo?id=" + startUp.Id + "&reason=" + reason, false);
                    System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                }
            }
        }

        private bool IsUserSubscribingAsTeam()
        {
            return DropDownTeams.SelectedIndex != 0;
        }

        /// <summary>
        /// Delete the startup in the DbContext.
        /// </summary>
        protected void BtnDeleteStartUp_Click(object sender, EventArgs e)
        {
            using (StartUpBaseEntities context = new StartUpBaseEntities())
            {
                startUp = context.StartUp.Find(startUp.Id);

                if (startUp.StartUpImage.Count > 0)
                {
                    startUp.StartUpImage.ToList().ForEach(o => context.Entry(o).State = EntityState.Deleted);
                }

                if (startUp.DocumentOfStartUp.Count > 0)
                {
                    startUp.DocumentOfStartUp.ToList().ForEach(o => context.Entry(o).State = EntityState.Deleted);
                }

                if (startUp.StartUpComment.Count > 0)
                {
                    startUp.StartUpComment.ToList().ForEach(o => context.Entry(o).State = EntityState.Deleted);
                }

                startUp.StartUpOfUser.ToList().ForEach(o => context.Entry(o).State = EntityState.Deleted);

                if (startUp.StartUpOfTeam.Count > 0)
                {
                    startUp.StartUpOfTeam.ToList().ForEach(o => context.Entry(o).State = EntityState.Deleted);
                }

                context.StartUp.Remove(startUp);

                try
                {
                    context.SaveChanges();
                    Response
                        .Redirect("~/Default.aspx?reason=" + HttpUtility.UrlEncode("Стартап успешно удалён!"), false);
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/StartUpInfo.aspx?id="
                        + startUp.Id
                        + "&reason="
                        + HttpUtility.UrlEncode("Стартап не был удалён! Ошибка: "
                        + ex.Message + "."
                        + "\nПожалуйста, попробуйте удалить стартап ещё раз"));
                }
            }
        }

        /// <summary>
        /// Commands handling, behaviour depends on a button's type.
        /// </summary>
        protected void LViewStartUpComments_ItemCommand(object sender, System.Web.UI.WebControls.ListViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("DeleteCommentById"))
            {
                using (StartUpBaseEntities context = new StartUpBaseEntities())
                {
                    StartUpComment comment = context.StartUpComment.Find(Convert.ToInt32(e.CommandArgument));

                    context.StartUpComment.Remove(comment);

                    try
                    {
                        context.SaveChanges();

                        context.ChangeTracker.Entries().ToList().ForEach(i => i.Reload());

                        Response.Redirect("~/StartUpInfo.aspx?id=" + startUp.Id + "&reason="
                            + HttpUtility.UrlEncode("Комментарий успешно удалён!"), false);
                    }
                    catch (Exception ex)
                    {
                        Response.Redirect("~/StartUpInfo.aspx?id=" + startUp.Id + "&reason="
                           + HttpUtility.UrlEncode("Комментарий не удалён! Пожалуйста, попробуйте ещё раз"));

                        System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                    }
                    return;
                }
            }

            if (e.CommandName.Equals("BanUserByCommentId"))
            {
                using (StartUpBaseEntities context = new StartUpBaseEntities())
                {
                    StartUpComment comment = context
                        .StartUpComment
                        .Find(Convert.ToInt32(e.CommandArgument));
                    User user = context.StartUpComment.Find(comment.Id).User;

                    #region Ban

                    StartUp startUpOfBan = context.StartUp.Find(startUp.Id);

                    List<StartUpOfUser> startUpsOfUser = context.StartUpOfUser
                            .Where(s => s.User.Login.Equals(user.Login)
                                        && s.StartUpId == startUpOfBan.Id).ToList();

                    bool isTryingToUnbanUser = startUpsOfUser.Select(s => s.RoleType.Name).Contains("Забанен");

                    List<StartUpOfUser> startUpsWhereIsNotBanned = startUpsOfUser
                        .Where(s => !s.RoleType.Name.Equals("Забанен")).ToList();

                    if (isTryingToUnbanUser)
                    {
                        context.StartUpOfUser.RemoveRange(startUpsOfUser);
                    }
                    else
                    {
                        StartUpOfUser bannedOfStartUp = new StartUpOfUser();

                        bannedOfStartUp = new StartUpOfUser
                        {
                            RoleTypeId = context.RoleType.First(r => r.Name.Equals("Забанен")).Id,
                            UserId = user.Id,
                            StartUpId = startUpOfBan.Id
                        };

                        startUpOfBan.StartUpOfUser.Add(bannedOfStartUp);

                        bool hasAnyTuples = startUpsWhereIsNotBanned.Count != 0;

                        if (hasAnyTuples)
                        {
                            context.StartUpOfUser.RemoveRange(startUpsWhereIsNotBanned);
                        }
                    }

                    #endregion

                    try
                    {
                        context.SaveChanges();
                        context.ChangeTracker.Entries().ToList().ForEach(i => i.Reload());

                        Response.Redirect("~/StartUpInfo.aspx?id=" + startUp.Id + "&reason="
                                    + HttpUtility.UrlEncode("Роль комментатора успешно изменена!"), false);
                    }
                    catch (Exception ex)
                    {
                        Response.Redirect("~/StartUpInfo.aspx?id=" + startUp.Id + "&reason="
                           + HttpUtility.UrlEncode("Роль комментатора не изменена! Пожалуйста, попробуйте ещё раз"));

                        System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                    }

                    return;
                }
            }

            if (e.CommandName.Equals("ChangeUserRoleType"))
            {
                using (StartUpBaseEntities context = new StartUpBaseEntities())
                {
                    StartUpComment comment = context.StartUpComment.Find(Convert.ToInt32(e.CommandArgument));

                    User user = comment.User;

                    StartUpOfUser nullableStartUp = context.StartUpOfUser.FirstOrDefault(s => s.UserId == user.Id
                    && s.RoleType.Name.Equals("Помощник")
                    && s.StartUpId == startUp.Id);
                    bool isStartUpOfUserExists = nullableStartUp != null && nullableStartUp.UserId != 0;

                    if (isStartUpOfUserExists)
                    {
                        context.Entry(nullableStartUp).State = EntityState.Deleted;
                    }
                    else
                    {
                        StartUpOfUser helperOfUser = new StartUpOfUser
                        {
                            RoleTypeId = context.RoleType.First(r => r.Name.Equals("Помощник")).Id,
                            UserId = user.Id,
                            StartUpId = comment.StartUp.Id
                        };
                        context.StartUpOfUser.Add(helperOfUser);
                    }

                    try
                    {
                        context.SaveChanges();
                        context.ChangeTracker.Entries().ToList().ForEach(i => i.Reload());

                        Response.Redirect("~/StartUpInfo.aspx?id=" + startUp.Id + "&reason="
                            + HttpUtility.UrlEncode("Роль помощника у участника изменена!"), false);
                    }
                    catch (Exception ex)
                    {
                        Response.Redirect("~/StartUpInfo.aspx?id=" + startUp.Id + "&reason="
                           + HttpUtility.UrlEncode("Роль помощника комментатора не изменена! Пожалуйста, попробуйте ещё раз"));

                        System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                    }
                }
            }

            MaintainScrollPositionOnPostBack = true;
        }

        protected void LinkGoToStartUpFiles_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StartUpDocuments.aspx?startUpId=" + startUp.Id);
        }
    }
}