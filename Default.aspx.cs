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
                AppData.Context.ChangeTracker.Entries().ToList().ForEach(s => s.Reload());
                LoadStartups();
                LoadTeams();
            }
        }

        private void LoadStartups()
        {
            List<StartUp> startUps = AppData.Context.StartUp.ToList();

            if (startUps == null)
            {
                return;
            }

            if (startUps.Count == 0)
            {
                (RecursiveControlFinder.FindControlRecursive(this, "EmptyStartupsPanel") as Panel).Visible = true;
                return;
            }

            startUps.RemoveAll(s => s.StartUpOfUser.Any(e => e.User.Login.Equals(User.Identity.Name) && e.RoleType.Name.Equals("Забанен")));

            (RecursiveControlFinder.FindControlRecursive(this, "LViewMyStartups") as ListView).DataSource = startUps;
            (RecursiveControlFinder.FindControlRecursive(this, "LViewMyStartups") as ListView).DataBind();
        }



        private void LoadTeams()
        {
            List<Team> teams = AppData.Context.Team.ToList();

            if (teams == null)
            {
                return;
            }

            if (teams.Count == 0)
            {
                (RecursiveControlFinder.FindControlRecursive(this, "EmptyTeamsPanel") as Panel).Visible = true;
                return;
            }

            teams.RemoveAll(s => s.TeamOfUser.Any(e => e.User.Login.Equals(User.Identity.Name) && e.RoleType.Name.Equals("Забанен")));

            (RecursiveControlFinder.FindControlRecursive(this, "LViewMyTeams") as ListView).DataSource = teams;
            (RecursiveControlFinder.FindControlRecursive(this, "LViewMyTeams") as ListView).DataBind();
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

        protected void BtnCreateStartUp_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AddStartUp.aspx");
        }

        protected void BtnCreateTeam_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AddTeam.aspx");
        }
    }
}