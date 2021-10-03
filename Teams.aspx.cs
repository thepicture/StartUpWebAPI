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
                CheckOrSetDropDownBoxes();
                UpdateTeamsView();
            }
        }

        /// <summary>
        /// Check if the page was not reloaded, otherwise loads parameter values into dropdown boxes.
        /// </summary>
        private void CheckOrSetDropDownBoxes()
        {
            if (!IsPostBack)
            {
                FillMembersBox();
                FillRegionBox();
            }
        }

        /// <summary>
        /// Inserts the regions into dropdown list.
        /// </summary>
        private void FillRegionBox()
        {
            RegionsView.DataSource = AppData.Context.Region.ToList();
            RegionsView.DataBind();

            AssignAnyValueForRegions();
        }

        /// <summary>
        /// Sets the checkbox state of items to false in RegionsView.
        /// </summary>
        private void AssignAnyValueForRegions()
        {
            ListViewTupleGetter.Get(RegionsView).ToList().ForEach(t => t.Item2.Checked = false);
        }

        /// <summary>
        /// Inserts max members count into the ComboBox.
        /// </summary>
        private void FillMembersBox()
        {
            List<string> values = new List<string>
            {
                "1-5",
                "6-10",
                "11-15",
                "15-20",
                "21-100",
                "101-1000",
                "1000-и больше"
            };

            MembersView.DataSource = values;
            MembersView.DataBind();

            AssignAnyValueForMembers();
        }

        /// <summary>
        /// Sets the checkbox state of items to false in MembersView.
        /// </summary>
        private void AssignAnyValueForMembers()
        {
            ListViewTupleGetter.Get(MembersView).ToList().ForEach(t => t.Item2.Checked = false);
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

            #region Work with dropdown boxes
            List<string> membersSelectedValues = TupleValueGetter.GetValues(
                                                    TupleToTextAndBoolConverter.ConvertToTextAndBoolTuple(
                                                        ListViewTupleGetter.Get(MembersView)
                                                        )
                                                    )
                .ToList();

            List<string> regionsSelectedValues = TupleValueGetter.GetValues(
                                                    TupleToTextAndBoolConverter.ConvertToTextAndBoolTuple(
                                                        ListViewTupleGetter.Get(RegionsView)
                                                        )
                                                    )
              .ToList();

            bool memberSelectedValueIsNonStandard = membersSelectedValues.Count != 0;
            bool regionSelectedValueIsNonStandard = regionsSelectedValues.Count != 0;

            if (memberSelectedValueIsNonStandard)
            {
                List<Team> teamsToUnion = new List<Team>();

                foreach (string value in membersSelectedValues)
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

            if (regionSelectedValueIsNonStandard)
            {
                currentTeams = currentTeams
                    .Where(s => regionsSelectedValues.Contains(s.Region.Name))
                    .ToList();
            }
            #endregion

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
            AssignAnyValueForMembers();
            AssignAnyValueForRegions();
        }
    }
}