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

            bool isStartUp = id != 0;
            if (!isStartUp)
            {
                Response.Redirect("~/Default.aspx");
            }

            startUp = AppData.Context.StartUp.Find(id);

            InsertComments();
            InsertStartUp();
        }

        private void InsertComments()
        {
            LViewStartUpComments.DataSource = startUp.StartUpComment.ToList();
            LViewStartUpComments.DataBind();
        }

        private void InsertStartUp()
        {
            Name.Text = MainName.Text = startUp.Name;
            CountOfMembers.Text = "Участников: " + startUp.StartUpOfUser.Count.ToString();
            CountOfTeams.Text = "Команд: " + startUp.StartUpOfTeam.Count.ToString();

            string creator = startUp.StartUpOfUser.FirstOrDefault(u => u.RoleType.Name == "Организатор").User.Name;

            Creator.Text = creator ?? "Неизвестен";
            IsActual.Text = startUp.IsDone ? "Завершён" : "Актуален";
            DateOfCreation.Text = startUp.CreationDate.ToString();
            Category.Text = startUp.Category.Name;
            Description.Text = startUp.Description == null ? "Организатор не предоставил описание. Можете подать ему идею!" : startUp.Description;
            CommentsCount.Text = "Комментарии (" + startUp.StartUpComment.Count + "):";
        }

        protected void BtnSendComment_Click(object sender, EventArgs e)
        {

        }
    }
}