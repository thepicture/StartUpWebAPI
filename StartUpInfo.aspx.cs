using StartUpWebAPI.Entities;
using System;
using System.Linq;

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
                string reason = "%D0%A1%D1%82%D0%B0%D1%80%D1%82%D0%B0%D0%BF%20%D0%BD%D0%B5%20%D0%B1%D1%8B%D0%BB%20%D0%BD%D0%B0%D0%B9%D0%B4%D0%B5%D0%BD%20%D0%B8%D0%BB%D0%B8%20%D0%B1%D1%8B%D0%BB%20%D1%83%D0%B4%D0%B0%D0%BB%D1%91%D0%BD.%20%D0%9F%D0%BE%D0%B6%D0%B0%D0%BB%D1%83%D0%B9%D1%81%D1%82%D0%B0%2C%20%D0%BD%D0%B0%D0%B9%D0%B4%D0%B8%D1%82%D0%B5%20%D0%B4%D1%80%D1%83%D0%B3%D0%B8%D0%B5%20%D1%81%D1%82%D0%B0%D1%80%D1%82%D0%B0%D0%BF%D1%8B.";
                Response.Redirect("~/Default.aspx?reason=" + reason);
            }

            startUp = AppData.Context.StartUp.Find(id);

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
        }
    }
}