using StartUpWebAPI.Entities;
using System;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StartUpWebAPI
{
    public partial class Teams : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
            }
            else
            {
                UpdateTeamsView();
            }
        }

        /// <summary>
        /// Updates the teams view.
        /// </summary>
        private void UpdateTeamsView()
        {
            var currentTeams = AppData.Context.Team.ToList();

            currentTeams.RemoveAll(s => s.TeamOfUser.Any(e => e.User.Name.Equals(User.Identity.Name) && e.RoleType.Name.Equals("Забанен")));

            TeamsView.DataSource = currentTeams;
            TeamsView.DataBind();
        }

        /// <summary>
        /// Redirects to the info about the clicked team.
        /// </summary>
        protected void TeamsView_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "TeamClicked")
            {
                Response.Redirect("~/TeamInfo.aspx?id=" + e.CommandArgument);
            }
        }
    }
}