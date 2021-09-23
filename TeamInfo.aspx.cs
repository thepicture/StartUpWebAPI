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
                string reason = HttpUtility.UrlEncode("Команда не существует или была удалена. Пожалуйста, найдите другую команду.");

                Response.Redirect("~/Default.aspx?reason=" + reason);
            }

            team = AppData.Context.Team.Find(id);

            InsertComments();
            InsertTeams();
        }

        private void InsertComments()
        {
            LViewTeamComments.DataSource = team.TeamComment.OrderByDescending(c => c.CreationDate).ToList();
            LViewTeamComments.DataBind();
        }

        private void InsertTeams()
        {
            Name.Text = MainName.Text = team.Name;
            CountOfMembers.Text = "Участников: " + team.TeamOfUser.Count.ToString();
            CountOfStartUps.Text = "Команд: " + team.StartUpOfTeam.Count.ToString();

            string creator = team.TeamOfUser.FirstOrDefault(u => u.RoleType.Name == "Организатор")?.User.Name;

            Creator.Text = creator ?? "Неизвестен";
            DateOfCreation.Text = team.CreationDate.ToString();
            CommentsCount.Text = "Комментарии (" + team.TeamComment.Count + "):";
            MainImage.ImageUrl = team.ImagePreview;
        }

        protected void BtnSendComment_Click(object sender, EventArgs e)
        {

        }
    }
}