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
            List<StartUp> startUps = AppData.Context.User.FirstOrDefault(u => u.Login.Equals(username)).StartUpOfUser.Select(s => s.StartUp).ToList();

            if (startUps == null)
            {
                return;
            }

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

        protected void LViewMyStartups_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("StartUpClicked"))
            {
                Response.Redirect("~/StartUpInfo.aspx?id=" + e.CommandArgument);
            }
        }

        protected void LViewMyTeams_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("TeamClicked"))
            {
                Response.Redirect("~/TeamInfo.aspx?id=" + e.CommandArgument);
            }
        }
    }
}