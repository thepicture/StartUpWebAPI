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
            bool isAuthenticated = HttpContext.Current.User.Identity.IsAuthenticated;

            if (PageLoadedForTheFirstTime())
            {
                ChangeTitle();
            }

            if (isAuthenticated)
            {
                LoadStartups();
                LoadTeams();
            }
        }

        private void ChangeTitle()
        {
            Title = User.Identity.IsAuthenticated ? "Домашняя страница" : "О проекте";
        }

        private bool PageLoadedForTheFirstTime()
        {
            return !IsPostBack;
        }

        /// <summary>
        /// Inserts the startups from db into the ListView.
        /// </summary>
        private void LoadStartups()
        {
            using (StartUpBaseEntities context = new StartUpBaseEntities())
            {

                List<StartUp> startUps = context.StartUp.ToList();

                bool noAnyStartups = (startUps == null);

                if (noAnyStartups)
                {
                    return;
                }

                startUps
                    .RemoveAll(s => s.StartUpOfUser
                    .Any(e => e.User.Login.Equals(User.Identity.Name)
                && e.RoleType.Name.Equals("Забанен")));
                startUps
                    .RemoveAll(s => !s.StartUpOfUser
                    .Select(i => i.User.Login)
                    .Contains(User.Identity.Name));

                bool startupsAreEmpty = startUps.Count == 0;

                if (startupsAreEmpty)
                {
                    (RecursiveControlFinder.FindControlRecursive(this, "EmptyStartupsPanel") as Panel)
                        .Visible = true;
                    return;
                }

                (RecursiveControlFinder.FindControlRecursive(this, "LViewMyStartups") as ListView)
                    .DataSource = startUps;
                (RecursiveControlFinder.FindControlRecursive(this, "LViewMyStartups") as ListView)
                    .DataBind();

            }
        }

        /// <summary>
        /// Loads the teams from the db into the ListView.
        /// </summary>
        private void LoadTeams()
        {
            using (StartUpBaseEntities context = new StartUpBaseEntities())
            {

                List<Team> teams = context.Team.ToList();

                bool teamsAreEmpty = teams == null;
                if (teamsAreEmpty)
                {
                    return;
                }


                teams.RemoveAll(s => s.TeamOfUser.Any(e => e.User.Login.Equals(User.Identity.Name)
                && e.RoleType.Name.Equals("Забанен")));
                teams.RemoveAll(t => !t.TeamOfUser.Select(i => i.User.Login)
                .Contains(User.Identity.Name));

                bool teamsCountIsZero = teams.Count == 0;

                if (teamsCountIsZero)
                {
                    (RecursiveControlFinder.FindControlRecursive(this, "EmptyTeamsPanel") as Panel)
                        .Visible = true;
                    return;
                }

                (RecursiveControlFinder.FindControlRecursive(this, "LViewMyTeams") as ListView)
                .DataSource = teams;
                (RecursiveControlFinder.FindControlRecursive(this, "LViewMyTeams") as ListView)
                    .DataBind();
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