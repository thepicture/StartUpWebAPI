using StartUpWebAPI.Entities;
using StartUpWebAPI.Models;
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
                InsertComboCountries();
                FillRegionBox();
            }
        }

        private void FillRegionBox()
        {
            RegionBox.InnerHtml = RegionTemplateBuilder.GetTemplate();
        }

        /// <summary>
        /// Inserts regions into the combobox.
        /// </summary>
        private void InsertComboCountries()
        {
            var countries = AppData.Context.Region.Select(c => c.Name).ToList();
            countries.Insert(0, "Все регионы");
            ComboCountries.DataSource = countries;
            ComboCountries.DataBind();
            ComboCountries.SelectedIndex = 0;
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
                "15-20",
                "21-100",
                "101-1000",
                "1000-и больше"
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

            currentTeams
                .RemoveAll(s => s.TeamOfUser
                .Any(e => e.User.Login.Equals(User.Identity.Name)
                && e.RoleType.Name.Equals("Забанен")));

            bool notAllIndexIsSelected = ComboMaxMembers.SelectedIndex != 0;

            List<Control> inputs = RecursiveControlFinder
                .FindControlRecursiveAll(this, "region-input");

            if (notAllIndexIsSelected)
            {
                List<string> selectedValues = ComboMaxMembers
                    .Items
                    .Cast<ListItem>()
                    .Where(i => i.Selected)
                    .Select(i => i.Value)
                    .ToList();


                List<Team> teamsToUnion = new List<Team>();

                foreach (string value in selectedValues)
                {
                    string[] values = value.Split('-');
                    int from = int.Parse(values[0]);
                    int to = int.Parse(values[1].Replace("и больше", int.MaxValue.ToString()));

                    teamsToUnion
                        .AddRange(currentTeams.Where(s => s.MaxMembersCount > from
                        && s.MaxMembersCount < to)
                        .ToList());
                }

                currentTeams = teamsToUnion.Distinct().ToList();
            }

            if (ComboCountries.SelectedIndex != 0)
            {
                List<string> selectedValues = ComboCountries
                    .Items
                    .Cast<ListItem>()
                    .Where(i => i.Selected)
                    .Select(i => i.Value)
                    .ToList();
                currentTeams = currentTeams
                    .Where(s => selectedValues.Contains(s.Region.Name))
                    .ToList();
            }

            if (!string.IsNullOrWhiteSpace(NameBox.Text))
            {
                currentTeams = currentTeams.Where(s => s.Name.ToLower().Contains(NameBox.Text.ToLower())).ToList();
            }

            TeamsView.DataSource = currentTeams;
            TeamsView.DataBind();
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