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

            bool userIsCreator = startUp.StartUpOfUser.Any(u => u.User.Login.ToLower().Equals(User.Identity.Name.ToLower()));

            if (userIsCreator)
            {
                PStartupEdit.Visible = true;
            }

            InsertComments();
            InsertStartUp();
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

            Creator.Text = creator ?? "Неизвестен";
            IsActual.Text = startUp.IsDone ? "Завершён" : "Актуален";
            DateOfCreation.Text = startUp.CreationDate.ToString();
            Category.Text = startUp.Category.Name;
            Description.Text = startUp.Description == null ? "Организатор не предоставил описание. Можете подать ему идею!" : startUp.Description;
            CommentsCount.Text = "Комментарии (" + startUp.StartUpComment.Count + "):";
            MainImage.ImageUrl = startUp.ImagePreview;
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
    }
}