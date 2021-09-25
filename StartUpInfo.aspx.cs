using StartUpWebAPI.Entities;
using System;
using System.Linq;
using System.Web;

namespace StartUpWebAPI
{
    public partial class StartUpInfo : System.Web.UI.Page
    {
        public StartUp startUp;

        protected void Page_Load(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Request.QueryString.Get("id"));

            bool isStartUp = AppData.Context.StartUp.Count(s => s.Id == id) != 0;

            if (!isStartUp)
            {
                string reason = HttpUtility.UrlEncode("Стартап не существует или был удалён. Пожалуйста, найдите другой стартап.");
                Response.Redirect("~/Default.aspx?reason=" + reason);
            }

            startUp = AppData.Context.StartUp.Find(id);

            bool userIsCreator = startUp.StartUpOfUser.Any(u => u.User.Login.ToLower().Equals(User.Identity.Name.ToLower())
                && u.RoleType.Name.Equals("Организатор"));

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
            CountOfMembers.Text = "Участников: " + startUp.StartUpOfUser.Count.ToString();
            CountOfTeams.Text = "Команд: " + startUp.StartUpOfTeam.Count.ToString();

            string creator = startUp.StartUpOfUser.FirstOrDefault(u => u.RoleType.Name == "Организатор")?.User.Name;

            Creator.Text = "Стартапер: " + creator ?? "Неизвестен";
            IsActual.Text = startUp.IsDone ? "Завершён" : "Актуален";
            DateOfCreation.Text = "Дата создания: " + startUp.CreationDate.ToString();
            Category.Text = "Категория: " + startUp.Category.Name;
            Description.Text = startUp.Description ?? "Организатор не предоставил описание. Можете подать ему идею!";
            CommentsCount.Text = "Комментарии (" + startUp.StartUpComment.Count + "):";
            MainImage.ImageUrl = startUp.ImagePreview;
            MaxMembersCount.Text = "Максимум участников: " + startUp.MaxMembersCount.ToString();
        }

        protected void BtnSendComment_Click(object sender, EventArgs e)
        {
            string username = User.Identity.Name;
            User currentUser = AppData.Context.User.First(u => u.Login.Equals(username));

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
                InsertComments();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            MaintainScrollPositionOnPostBack = true;
        }

        protected void LinkButtonModifyStartUp_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AddStartUp?id=" + startUp.Id);
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
                Response.Redirect("~/StartUpInfo?id=" + startUp.Id + "&reason=" + reason);
            }
            catch (Exception)
            {
                reason = HttpUtility.UrlEncode("Не удалось покинуть стартап. Попробуйте, пожалуйста, ещё раз.");
                Response.Redirect("~/StartUpInfo?id=" + startUp.Id + "&reason=" + reason);

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
                Response.Redirect("~/StartUpInfo?id=" + startUp.Id + "&reason=" + reason);
            }
            catch (Exception)
            {
                reason = HttpUtility.UrlEncode("Не удалось вступить в стартап. Попробуйте, пожалуйста, ещё раз.");
                Response.Redirect("~/StartUpInfo?id=" + startUp.Id + "&reason=" + reason);

                return;
            }
        }
    }
}