using StartUpWebAPI.Entities;
using System;
using System.Collections.Generic;
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
                CheckOrSetMaxMembers();
                UpdateTeamsView();
            }
        }

        /// <summary>
        /// Checks if the page was reloaded, if not then it inserts max members values into ComboBox.
        /// </summary>
        private void CheckOrSetMaxMembers()
        {
            if (!IsPostBack)
            {
                InsertComboMaxMembers();
            }
        }

        /// <summary>
        /// Inserts max members count into the ComboBox.
        /// </summary>
        private void InsertComboMaxMembers()
        {
            List<string> values = new List<string>
            {
                "Любое кол-во участников",
                "1-5",
                "6-10",
                "11-15",
                "15-20"
            };

            ComboMaxMembers.DataSource = values;
            ComboMaxMembers.DataBind();
            ComboMaxMembers.SelectedIndex = 0;
        }

        /// <summary>
        /// Updates the teams view.
        /// </summary>
        private void UpdateTeamsView()
        {
            var currentTeams = AppData.Context.Team.ToList();

            currentTeams.RemoveAll(s => s.TeamOfUser.Any(e => e.User.Name.Equals(User.Identity.Name) && e.RoleType.Name.Equals("Забанен")));

            if (ComboMaxMembers.SelectedIndex != 0)
            {
                string[] values = ComboMaxMembers.SelectedValue.Split('-');
                int from = int.Parse(values[0]);
                int to = int.Parse(values[1]);

                currentTeams = currentTeams.Where(s => s.MaxMembersCount > from && s.MaxMembersCount < to).ToList();
            }

            if (!string.IsNullOrWhiteSpace(NameBox.Text))
            {
                currentTeams = currentTeams.Where(s => s.Name.ToLower().Contains(NameBox.Text.ToLower())).ToList();
            }

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

        /// <summary>
        /// Search startups with given filters.
        /// </summary>
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            UpdateTeamsView();
        }

        /// <summary>
        /// Clears filtration's filters and updates teams ListView.
        /// </summary>
        protected void BtnClear_Click(object sender, EventArgs e)
        {
            ClearElements();
            UpdateTeamsView();
        }

        /// <summary>
        /// Clears the filtration.
        /// </summary>
        private void ClearElements()
        {
            NameBox.Text = null;
            ComboMaxMembers.SelectedIndex = 0;
        }
    }
}