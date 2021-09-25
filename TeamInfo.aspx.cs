using StartUpWebAPI.Entities;
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

          

            int id = Convert.ToInt32(Request.QueryString.Get("id"));

            bool isTeam = id != 0;
            if (!isTeam)
            {
                string reason = HttpUtility.UrlEncode("Команда не существует или была удалена. Пожалуйста, найдите другую команду.");

                Response.Redirect("~/Default.aspx?reason=" + reason);
            }

            team = AppData.Context.Team.Find(id);

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

        private void ShowSubscribeButtonIfNotMaxMembersCount()
        {
            if (team.MaxMembersCount <= team.TeamOfUser.Count)
            {
                return;
            }

            BtnSubscribe.Visible = true;
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
            CountOfStartUps.Text = "Количество связанных стартапов: " + team.StartUpOfTeam.Count.ToString();

            string creator = "Организатор: " + team.TeamOfUser.FirstOrDefault(u => u.RoleType.Name == "Организатор")?.User.Name;
            MaxMembersCount.Text = "Максимум участников: " + team.MaxMembersCount.ToString();
            Creator.Text = creator ?? "Неизвестен";
            DateOfCreation.Text = "Дата создания: " + team.CreationDate.ToString();
            CommentsCount.Text = "Комментарии (" + team.TeamComment.Count + "):";
            MainImage.ImageUrl = team.ImagePreview;
        }

        protected void BtnSendComment_Click(object sender, EventArgs e)
        {

        }

        protected void LinkButtonModifyTeam_Click(object sender, EventArgs e)
        {

        }

        protected void BtnUnsubscribe_Click(object sender, EventArgs e)
        {

        }

        protected void BtnSubscribe_Click(object sender, EventArgs e)
        {

        }
    }
}