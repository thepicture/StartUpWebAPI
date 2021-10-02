using StartUpWebAPI.Entities;
using StartUpWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace StartUpWebAPI
{
    public partial class TeamInfo : System.Web.UI.Page
    {
        public Team team;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadBackgroundImage();
            }

            int id = Convert.ToInt32(Request.QueryString.Get("id"));

            bool isTeam = id != 0;

            if (!isTeam)
            {
                string reason = HttpUtility.UrlEncode("Команда не существует или была удалена. Пожалуйста, найдите другую команду.");

                Response.Redirect("~/Default.aspx?reason=" + reason);
            }

            team = AppData.Context.Team.Find(id);

            if (team.TeamOfUser.Any(t => t.User.Login.Equals(User.Identity.Name) && t.RoleType.Name.Equals("Забанен")))
            {
                Response.Redirect("~/Default.aspx?reason=" +
                    HttpUtility.UrlEncode("К сожалению, доступ к данному сообществу для вас ограничен. " +
                    "Пожалуйста, найдите другие сообщества."));
                return;
            }

            bool userIsCreator = team.TeamOfUser.Any(u => u.User.Login.ToLower().Equals(User.Identity.Name.ToLower())
                && u.RoleType.Name.Equals("Организатор"));

            if (userIsCreator)
            {
                PTeamEdit.Visible = true;
            }
            else
            {
                ShowNeedyButtonsForMember();
            }

            InsertComments();
            InsertTeams();
        }
        /// <summary>
        /// Redirects to the edit team page.
        /// </summary>
        protected void LinkButtonModifyTeam_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AddTeam?id=" + team.Id, false);
        }

        /// <summary>
        /// Loads background image.
        /// </summary>
        private void LoadBackgroundImage()
        {
            BgImage.ImageUrl = NativeImageUtils.ConvertFromBitmap(Properties.Resources.myAccountBg);
        }

        /// <summary>
        /// Show appropriate control elements for the current user depending on his role.
        /// </summary>
        private void ShowNeedyButtonsForMember()
        {
            bool userInTeam = team.TeamOfUser.Any(u => u.User.Login.ToLower().Equals(User.Identity.Name.ToLower()));

            if (userInTeam)
            {
                BtnUnsubscribe.Visible = true;
            }
            else
            {
                ShowSubscribeButtonIfNotMaxMembersCount();
            }
        }

        /// <summary>
        /// Shows subscribe button for the user.
        /// </summary>
        private void ShowSubscribeButtonIfNotMaxMembersCount()
        {
            if (team.MaxMembersCount <= team.TeamOfUser.Count)
            {
                return;
            }

            BtnSubscribe.Visible = true;
        }

        /// <summary>
        /// Inserts comments into ListView.
        /// </summary>
        private void InsertComments()
        {
            LViewTeamComments.DataSource = team.TeamComment.OrderByDescending(c => c.CreationDate).ToList();
            LViewTeamComments.DataBind();
        }

        /// <summary>
        /// Updates the comments count.
        /// </summary>
        private void UpdateCommentsCount()
        {
            CommentsCount.Text = "Комментарии (" + team.TeamComment.Count + "):";
        }

        /// <summary>
        /// Inserts team into view.
        /// </summary>
        private void InsertTeams()
        {
            Name.Text = MainName.Text = team.Name;
            CountOfMembers.Text = team.TeamOfUser.Count.ToString();
            CountOfStartUps.Text = team.StartUpOfTeam.Count.ToString();

            string creator = team.TeamOfUser.FirstOrDefault(u => u.RoleType.Name == "Организатор")?.User.Name;
            MaxMembersCount.Text = team.MaxMembersCount.ToString();
            Creator.Text = creator ?? "Неизвестен";
            DateOfCreation.Text = team.CreationDate.ToString();
            UpdateCommentsCount();
            MainImage.ImageUrl = team.ImagePreview;
            Region.Text = team.RegionText;
        }

        /// <summary>
        /// Pre-actions for sending comments.
        /// </summary>
        protected void BtnSendComment_Click(object sender, EventArgs e)
        {
            string errors = "";

            if (string.IsNullOrWhiteSpace(CommentBox.Text))
            {
                errors += "Пожалуйста, введите непустой комментарий";
            }

            if (errors.Length > 0)
            {
                Response.Redirect("~/TeamInfo.aspx?id=" + team.Id + "&reason="
                      + HttpUtility.UrlEncode(errors));
                return;
            }

            string username = User.Identity.Name;

            DateTime? lastComment = team.TeamComment
                .Where(c => c.User.Login.Equals(username))?
                .OrderByDescending(c => c.CreationDate)
                .FirstOrDefault()?
                .CreationDate;

            if (lastComment != null)
            {
                if (AntiSpamChecker.IsLastCommentRecentThat(5, lastComment.Value))
                {
                    Response.Redirect("~/TeamInfo.aspx?id=" + team.Id + "&reason="
                        + HttpUtility.UrlEncode("Нельзя отправлять комментарии слишком часто"));
                    return;
                }
            }

            User currentUser = AppData.Context.User.First(u => u.Login.Equals(username));

            SendComment(currentUser);
        }

        /// <summary>
        /// Sends the user's comment.
        /// </summary>
        /// <param name="currentUser">Who is sending the comment.</param>
        private void SendComment(User currentUser)
        {
            TeamComment comment = new TeamComment
            {
                CommentText = CommentBox.Text,
                CreationDate = DateTime.Now,
                User = currentUser,
                Team = team,
            };

            team.TeamComment.Add(comment);

            try
            {
                AppData.Context.SaveChanges();

                AppData.Context.ChangeTracker.Entries().ToList().ForEach(e => e.Reload());

                Response.Redirect("~/TeamInfo.aspx?id=" + team.Id + "&reason="
                    + HttpUtility.UrlEncode("Комментарий успешно добавлен!"), false);

                CommentBox.Text = null;
                InsertComments();
                UpdateCommentsCount();
            }
            catch (Exception ex)
            {
                string reason = HttpUtility.UrlEncode("Не удалось написать комментарий. Попробуйте, пожалуйста, ещё раз.");
                Response.Redirect("~/TeamInfo?id=" + team.Id + "&reason=" + reason);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }

            MaintainScrollPositionOnPostBack = true;
        }

        /// <summary>
        /// Unsubscribes the user from the team.
        /// </summary>
        protected void BtnUnsubscribe_Click(object sender, EventArgs e)
        {
            string reason;
            TeamOfUser currentTeamOfUser = FindStartUp();
            AppData.Context.TeamOfUser.Remove(currentTeamOfUser);

            try
            {
                AppData.Context.SaveChanges();
                reason = HttpUtility.UrlEncode("Вы успешно покинули команду");
                Response.Redirect("~/TeamInfo?id=" + team.Id + "&reason=" + reason, false);
            }
            catch (Exception ex)
            {
                reason = HttpUtility.UrlEncode("Не удалось покинуть команду. Попробуйте, пожалуйста, ещё раз.");
                Response.Redirect("~/TeamInfo?id=" + team.Id + "&reason=" + reason);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);

                return;
            }
        }

        /// <summary>
        /// Finds the start up and returns it.
        /// </summary>
        /// <returns>The found startup.</returns>
        private TeamOfUser FindStartUp()
        {
            return AppData.Context.TeamOfUser.First(s => s.User.Login.Equals(User.Identity.Name)
                        && s.TeamId == team.Id);
        }

        /// <summary>
        /// Subscribes the user to the team.
        /// </summary>
        protected void BtnSubscribe_Click(object sender, EventArgs e)
        {
            string reason;

            TeamOfUser teamOfUser = new TeamOfUser
            {
                Team = team,
                User = AppData.Context.User.First(u => u.Login.Equals(User.Identity.Name)),
                RoleType = AppData.Context.RoleType.First(r => r.Name.Equals("Участник"))
            };

            AppData.Context.Team.Find(team.Id).TeamOfUser.Add(teamOfUser);

            try
            {
                AppData.Context.SaveChanges();
                reason = HttpUtility.UrlEncode("Вы успешно вступили в команду");
                Response.Redirect("~/TeamInfo?id=" + team.Id + "&reason=" + reason, false);
            }
            catch (Exception ex)
            {
                reason = HttpUtility.UrlEncode("Не удалось вступить в команлу. Попробуйте, пожалуйста, ещё раз.");
                Response.Redirect("~/TeamInfo?id=" + team.Id + "&reason=" + reason, false);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);

                return;
            }
        }

        /// <summary>
        /// Deletes the team.
        /// </summary>
        protected void BtnDeleteTeam_Click(object sender, EventArgs e)
        {
            AppData.Context.TeamComment.RemoveRange(team.TeamComment);
            AppData.Context.TeamOfUser.RemoveRange(team.TeamOfUser);
            AppData.Context.StartUpOfTeam.RemoveRange(team.StartUpOfTeam);

            AppData.Context.Team.Remove(team);

            try
            {
                AppData.Context.SaveChanges();

                Response.Redirect("~/Default.aspx?reason=" + HttpUtility.UrlEncode("Команда успешно удалена!"), false);
            }
            catch (Exception ex)
            {
                Response.Redirect("~/TeamInfo.aspx?id=" + team.Id + "&reason="
                    + HttpUtility.UrlEncode("Стартап не был удалён! "
                    + "\nПожалуйста, попробуйте удалить стартап ещё раз"));
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
        }

        /// <summary>
        /// Command handling depending on the selected item in the listview of team comments.
        /// </summary>
        protected void LViewTeamComments_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("DeleteCommentById"))
            {
                TeamComment comment = AppData.Context.TeamComment.Find(Convert.ToInt32(e.CommandArgument));

                AppData.Context.TeamComment.Remove(comment);

                try
                {
                    AppData.Context.SaveChanges();

                    AppData.Context.ChangeTracker.Entries().ToList().ForEach(i => i.Reload());

                    Response.Redirect("~/TeamInfo.aspx?id=" + team.Id + "&reason="
                        + HttpUtility.UrlEncode("Комментарий успешно удалён!"), false);
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/TeamInfo.aspx?id=" + team.Id + "&reason="
                       + HttpUtility.UrlEncode("Комментарий не удалён! Пожалуйста, попробуйте ещё раз"));

                    System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                }
                return;
            }

            if (e.CommandName.Equals("BanUserByCommentId"))
            {
                TeamComment comment = AppData
                    .Context
                    .TeamComment
                    .Find(Convert.ToInt32(e.CommandArgument));
                User user = comment.User;

                BanUtils.BanOrUnban(user, comment.Team);

                try
                {
                    AppData.Context.SaveChanges();
                    AppData.Context.ChangeTracker.Entries().ToList().ForEach(i => i.Reload());

                    Response.Redirect("~/TeamInfo.aspx?id=" + team.Id + "&reason="
                        + HttpUtility.UrlEncode("Роль комментатора успешно изменена!"), false);
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/TeamInfo.aspx?id=" + team.Id + "&reason="
                       + HttpUtility.UrlEncode("Роль комментатора не изменена! Пожалуйста, попробуйте ещё раз"));

                    System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                }
                return;
            }

            if (e.CommandName.Equals("ChangeUserRoleType"))
            {
                TeamComment comment = AppData.Context.TeamComment.Find(Convert.ToInt32(e.CommandArgument));

                User user = comment.User;

                TeamOfUser nullableTeam = team.TeamOfUser.FirstOrDefault(s => s.UserId == user.Id
                && s.RoleType.Name.Equals("Помощник"));
                bool isTeamOfUserExists = nullableTeam != null && nullableTeam.UserId != 0;

                if (isTeamOfUserExists)
                {
                    AppData.Context.Entry(nullableTeam).State = System.Data.Entity.EntityState.Deleted;
                }
                else
                {
                    TeamOfUser helperOfUser = new TeamOfUser
                    {
                        RoleTypeId = AppData.Context.RoleType.First(r => r.Name.Equals("Помощник")).Id,
                        UserId = user.Id,
                        TeamId = comment.Team.Id
                    };
                    AppData.Context.TeamOfUser.Add(helperOfUser);
                }

                try
                {
                    AppData.Context.SaveChanges();
                    AppData.Context.ChangeTracker.Entries().ToList().ForEach(i => i.Reload());

                    Response.Redirect("~/TeamInfo.aspx?id=" + team.Id + "&reason="
                        + HttpUtility.UrlEncode("Роль помощника у участника изменена!"), false);
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/TeamInfo.aspx?id=" + team.Id + "&reason="
                       + HttpUtility.UrlEncode("Роль помощника комментатора не изменена! Пожалуйста, попробуйте ещё раз"));

                    System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                }
            }
        }
    }
}