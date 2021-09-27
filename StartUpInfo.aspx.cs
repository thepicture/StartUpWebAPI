using StartUpWebAPI.Entities;
using StartUpWebAPI.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace StartUpWebAPI
{
    public partial class StartUpInfo : System.Web.UI.Page
    {
        public StartUp startUp;

        protected void Page_Load(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Request.QueryString.Get("id"));

            bool isStartUp = AppData.Context.StartUp.Any(s => s.Id == id);

            if (!isStartUp)
            {
                string reason = HttpUtility.UrlEncode("Стартап не существует или был удалён. Пожалуйста, найдите другой стартап.");
                Response.Redirect("~/Default.aspx?reason=" + reason, false);
                return;
            }

            startUp = AppData.Context.StartUp.Find(id);

            if (startUp.StartUpOfUser.Any(s => s.User.Login.Equals(User.Identity.Name) && s.RoleType.Name.Equals("Забанен")))
            {
                Response.Redirect("~/Default.aspx?reason=" + HttpUtility.UrlEncode("К сожалению, доступ к данному сообществу для вас ограничен. " +
                    "Пожалуйста, найдите другие сообщества."));
                return;
            }

            bool userIsCreator = false;
            if (startUp != null)
            {
                userIsCreator = startUp
                    .StartUpOfUser
                    .Any(u => u.User.Login.Equals(User.Identity.Name)
                    && (u.RoleType.Name.Equals("Организатор")
                    || u.RoleType.Name.Equals("Помощник")));
            }

            if (userIsCreator)
            {
                PStartupEdit.Visible = true;
            }
            else
            {
                ShowNeedyButtonsForMember();
            }

            InsertComments();
            InsertStartUp();
        }

        /// <summary>
        /// If an user in the startup then show appropriate buttons for him.
        /// </summary>
        private void ShowNeedyButtonsForMember()
        {
            bool userInStartUp = startUp.StartUpOfUser.Any(u => u.User.Login.ToLower().Equals(User.Identity.Name.ToLower()));

            if (userInStartUp)
            {
                BtnUnsubscribe.Visible = true;
            }
            else
            {
                ShowSubscribeButtonIfNotMaxMembersCount();
            }
        }

        private void ShowSubscribeButtonIfNotMaxMembersCount()
        {
            if (startUp.MaxMembersCount <= startUp.StartUpOfUser.Count + startUp.StartUpOfTeam.Count)
            {
                return;
            }

            BtnSubscribe.Visible = true;
        }

        private void InsertComments()
        {
            LViewStartUpComments.DataSource = startUp.StartUpComment.OrderByDescending(c => c.DateTime).ToList();
            LViewStartUpComments.DataBind();
        }

        private void InsertStartUp()
        {
            Name.Text = MainName.Text = startUp.Name;
            CountOfMembers.Text = startUp.StartUpOfUser.Count.ToString();
            CountOfTeams.Text = startUp.StartUpOfTeam.Count.ToString();

            string creator = startUp.StartUpOfUser.FirstOrDefault(u => u.RoleType.Name == "Организатор")?.User.Name;

            Creator.Text = (string.IsNullOrWhiteSpace(creator)) ? "Неизвестен" : creator;
            IsActual.Text = startUp.IsDone ? "Завершён" : "Актуален";
            DateOfCreation.Text = startUp.CreationDate.ToString();
            Category.Text = startUp.Category.Name;
            Description.Text = string.IsNullOrWhiteSpace(startUp.Description) ? "Организатор не предоставил описание. Можете подать ему идею!" : startUp.Description;
            UpdateCommentsCount();
            MainImage.ImageUrl = startUp.ImagePreview;
            MaxMembersCount.Text = startUp.MaxMembersCount.ToString();
        }

        private void UpdateCommentsCount()
        {
            CommentsCount.Text = "Комментарии (" + startUp.StartUpComment.Count + "):";
        }

        protected void BtnSendComment_Click(object sender, EventArgs e)
        {
            string errors = "";

            if (string.IsNullOrWhiteSpace(CommentBox.Text))
            {
                errors += "Пожалуйста, введите непустой комментарий";
            }

            if (errors.Length > 0)
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

            if (lastComment != null)
            {
                if (AntiSpamChecker.IsLastCommentRecentThat(5, lastComment.Value))
                {
                    Response.Redirect("~/StartUpInfo.aspx?id=" + startUp.Id + "&reason="
                        + HttpUtility.UrlEncode("Нельзя отправлять комментарии слишком часто"));
                    return;
                }
            }

            User currentUser = AppData.Context.User.First(u => u.Login.Equals(username));

            SendComment(currentUser);
        }

        private void SendComment(User currentUser)
        {
            StartUpComment comment = new StartUpComment
            {
                CommentText = CommentBox.Text,
                DateTime = DateTime.Now,
                User = currentUser,
                StartUp = startUp,
            };

            startUp.StartUpComment.Add(comment);

            try
            {
                AppData.Context.SaveChanges();

                AppData.Context.ChangeTracker.Entries().ToList().ForEach(e => e.Reload());

                Response.Redirect("~/StartUpInfo.aspx?id=" + startUp.Id + "&reason="
                    + HttpUtility.UrlEncode("Комментарий успешно добавлен!"), false);

                CommentBox.Text = null;
                InsertComments();
                UpdateCommentsCount();
            }
            catch (Exception)
            {
                string reason = HttpUtility.UrlEncode("Не удалось написать комментарий. Попробуйте, пожалуйста, ещё раз.");
                Response.Redirect("~/StartUpInfo?id=" + startUp.Id + "&reason=" + reason);
            }

            MaintainScrollPositionOnPostBack = true;
        }

        protected void LinkButtonModifyStartUp_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AddStartUp?id=" + startUp.Id, false);
        }

        protected void BtnUnsubscribe_Click(object sender, EventArgs e)
        {
            string reason;
            StartUpOfUser currentStartup = FindStartUp();
            AppData.Context.StartUpOfUser.Remove(currentStartup);

            try
            {
                AppData.Context.SaveChanges();
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

        private StartUpOfUser FindStartUp()
        {
            return AppData.Context.StartUpOfUser.First(s => s.User.Login.Equals(User.Identity.Name)
                         && s.StartUpId == startUp.Id);
        }

        protected void BtnSubscribe_Click(object sender, EventArgs e)
        {
            string reason;

            StartUpOfUser startUpOfUser = new StartUpOfUser
            {
                StartUp = startUp,
                User = AppData.Context.User.First(u => u.Login.Equals(User.Identity.Name)),
                RoleType = AppData.Context.RoleType.First(r => r.Name.Equals("Участник"))
            };

            AppData.Context.StartUp.Find(startUp.Id).StartUpOfUser.Add(startUpOfUser);

            try
            {
                AppData.Context.SaveChanges();
                reason = HttpUtility.UrlEncode("Вы успешно вступили в стартап");
                Response.Redirect("~/StartUpInfo?id=" + startUp.Id + "&reason=" + reason, false);
            }
            catch (Exception)
            {
                reason = HttpUtility.UrlEncode("Не удалось вступить в стартап. Попробуйте, пожалуйста, ещё раз.");
                Response.Redirect("~/StartUpInfo?id=" + startUp.Id + "&reason=" + reason, false);

                return;
            }
        }

        protected void BtnDeleteStartUp_Click(object sender, EventArgs e)
        {
            AppData.Context.StartUpImage.RemoveRange(startUp.StartUpImage);
            AppData.Context.DocumentOfStartUp.RemoveRange(startUp.DocumentOfStartUp);
            AppData.Context.StartUpComment.RemoveRange(startUp.StartUpComment);
            AppData.Context.StartUpOfUser.RemoveRange(startUp.StartUpOfUser);
            AppData.Context.StartUpOfTeam.RemoveRange(startUp.StartUpOfTeam);

            AppData.Context.StartUp.Remove(startUp);

            try
            {
                AppData.Context.SaveChanges();

                Response.Redirect("~/Default.aspx?reason=" + HttpUtility.UrlEncode("Стартап успешно удалён!"), false);
            }
            catch (Exception ex)
            {
                Response.Redirect("~/StartUpInfo.aspx?id=" + startUp.Id + "&reason=" + HttpUtility.UrlEncode("Стартап не был удалён! Ошибка: " + ex.Message + "."
                    + "\nПожалуйста, попробуйте удалить стартап ещё раз"), false);
            }
        }
    }
}