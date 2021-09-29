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
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                LoadStartups();
                LoadTeams();
            }
        }

        /// <summary>
        /// Inserts the startups from db into the ListView.
        /// </summary>
        private void LoadStartups()
        {
            List<StartUp> startUps = AppData.Context.StartUp.ToList();

            if (startUps == null)
            {
                return;
            }


            startUps.RemoveAll(s => s.StartUpOfUser.Any(e => e.User.Login.Equals(User.Identity.Name) && e.RoleType.Name.Equals("Забанен")));
            startUps.RemoveAll(s => !s.StartUpOfUser.Select(i => i.User.Login).Contains(User.Identity.Name));

            if (startUps.Count == 0)
            {
                (RecursiveControlFinder.FindControlRecursive(this, "EmptyStartupsPanel") as Panel).Visible = true;
                return;
            }

            (RecursiveControlFinder.FindControlRecursive(this, "LViewMyStartups") as ListView).DataSource = startUps;
            (RecursiveControlFinder.FindControlRecursive(this, "LViewMyStartups") as ListView).DataBind();
        }

        /// <summary>
        /// Loads the teams from the db into the ListView.
        /// </summary>
        private void LoadTeams()
        {
            List<Team> teams = AppData.Context.Team.ToList();

            if (teams == null)
            {
                return;
            }


            teams.RemoveAll(s => s.TeamOfUser.Any(e => e.User.Login.Equals(User.Identity.Name) && e.RoleType.Name.Equals("Забанен")));
            teams.RemoveAll(t => !t.TeamOfUser.Select(i => i.User.Login).Contains(User.Identity.Name));

            if (teams.Count == 0)
            {
                (RecursiveControlFinder.FindControlRecursive(this, "EmptyTeamsPanel") as Panel).Visible = true;
                return;
            }

            (RecursiveControlFinder.FindControlRecursive(this, "LViewMyTeams") as ListView).DataSource = teams;
            (RecursiveControlFinder.FindControlRecursive(this, "LViewMyTeams") as ListView).DataBind();
        }

        /// <summary>
        /// Redirects the user to the clicked startup.
        /// </summary>
        protected void LViewMyStartups_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("StartUpClicked"))
            {
                Response.Redirect("~/StartUpInfo.aspx?id=" + e.CommandArgument);
            }
        }

        /// <summary>
        /// Redirects the user to the clicked team.
        /// </summary>
        protected void LViewMyTeams_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("TeamClicked"))
            {
                Response.Redirect("~/TeamInfo.aspx?id=" + e.CommandArgument);
            }
        }

        /// <summary>
        /// Redirects the user to the add/modify startup page.
        /// </summary>
        protected void BtnCreateStartUp_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AddStartUp.aspx");
        }

        /// <summary>
        /// Redirects the user to the add/modify team page.
        /// </summary>
        protected void BtnCreateTeam_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AddTeam.aspx");
        }
    }
}