using StartUpWebAPI.Entities;
using StartUpWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StartUpWebAPI
{
    public partial class _Default : Page
    {
        private string username;
        protected void Page_Load(object sender, EventArgs e)
        {
            username = CookieUtils.GetUserNameOrNullOf(Context.Request);
            string isAuthorized = Request.Cookies[".ASPXAUTH"]?.Value;

            if (isAuthorized != null)
            {
                AnonContent.Visible = false;
                LoggedInContent.Visible = true;
                LoadStartups();
                LoadTeams();
            }
            else
            {
                AnonContent.Visible = true;
                LoggedInContent.Visible = false;
            }

        }

        private void LoadStartups()
        {
            List<StartUp> startUps = AppData.Context.User.First(u => u.Login.Equals(username)).StartUpOfUser.Select(s => s.StartUp).ToList();
            LViewMyStartups.DataSource = startUps;

            if (startUps.Count == 0)
            {
                EmptyStartupsPanel.Visible = true;
            }

            LViewMyStartups.DataBind();
        }

        private void LoadTeams()
        {
            List<TeamOfUser> teams = AppData.Context.User.First(u => u.Login.Equals(username)).TeamOfUser.ToList();
            LViewMyTeams.DataSource = teams;

            if (teams.Count == 0)
            {
                EmptyTeamsPanel.Visible = true;
            }

            LViewMyTeams.DataBind();
        }

        protected void BtnStartUpInfo_Click(object sender, EventArgs e)
        {
            string text = (sender as LinkButton).Text;
            int id = AppData.Context.StartUp.First(s => s.Name.Equals(text)).Id;

            Response.Redirect("~/StartUpInfo.aspx?id=" + id);
        }

        protected void BtnTeamInfo_Click(object sender, EventArgs e)
        {
            string text = (sender as LinkButton).Text;
            int id = AppData.Context.Team.First(s => s.Name.Equals(text)).Id;

            Response.Redirect("~/TeamInfo.aspx?id=" + id);
        }
    }
}