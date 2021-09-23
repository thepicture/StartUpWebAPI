using StartUpWebAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StartUpWebAPI
{
    public partial class TeamInfo : System.Web.UI.Page
    {
        public Team team;

        protected void Page_Load(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Request.QueryString.Get("id"));

            bool isTeam = id != 0;
            if (!isTeam)
            {
                string reason = "%D0%9A%D0%BE%D0%BC%D0%B0%D0%BD%D0%B4%D0%B0%20%D0%BD%D0%B5%20%D1%81%D1%83%D1%89%D0%B5%D1%81%D1%82%D0%B2%D1%83%D0%B5%D1%82%20%D0%B8%D0%BB%D0%B8%20%D0%B1%D1%8B%D0%BB%D0%B0%20%D1%83%D0%B4%D0%B0%D0%BB%D0%B5%D0%BD%D0%B0.%20%D0%9F%D0%BE%D0%B6%D0%B0%D0%BB%D1%83%D0%B9%D1%81%D1%82%D0%B0%2C%20%D0%BF%D0%BE%D0%BF%D1%80%D0%BE%D0%B1%D1%83%D0%B9%D1%82%D0%B5%20%D0%BD%D0%B0%D0%B9%D1%82%D0%B8%20%D0%B4%D1%80%D1%83%D0%B3%D1%83%D1%8E%20%D0%BA%D0%BE%D0%BC%D0%B0%D0%BD%D0%B4%D1%83.";

                Response.Redirect("~/Default.aspx?reason=" + reason);
            }

            team = AppData.Context.Team.Find(id);

            InsertComments();
            InsertTeams();
        }

        private void InsertComments()
        {
            LViewTeamComments.DataSource = team.TeamComment.OrderByDescending(c => c.DateTime).ToList();
            LViewTeamComments.DataBind();
        }

        private void InsertTeams()
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
    }
}