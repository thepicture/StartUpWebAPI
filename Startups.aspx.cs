using StartUpWebAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace StartUpWebAPI
{
    public partial class Startups : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
            }
            else
            {
                CheckOrSetCategories();
                CheckOrSetMaxMembers();
                UpdateLView();
            }
        }

        /// <summary>
        /// Check if the page was not reloaded, otherwise loads member parameter values into ComboBox.
        /// </summary>
        private void CheckOrSetMaxMembers()
        {
            if (!IsPostBack)
            {
                InsertComboMaxMembers();
            }
        }

        /// <summary>
        /// Inserts members parameters into the ComboBox.
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
        /// Checks if the page loaded for the first time, otherwise inserts categories.
        /// </summary>
        private void CheckOrSetCategories()
        {
            if (!IsPostBack)
            {
                InsertComboCategories();
                InsertComboCountries();
            }
        }

        /// <summary>
        /// Inserts the regions into the combobox.
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
        /// Inserts categories into DropDownList.
        /// </summary>
        private void InsertComboCategories()
        {
            var categories = AppData.Context.Category.Select(c => c.Name).ToList();
            categories.Insert(0, "Все категории");
            ComboCategories.DataSource = categories;
            ComboCategories.DataBind();
            ComboCategories.SelectedIndex = 0;
        }

        /// <summary>
        /// Inserts startups into ListView.
        /// </summary>
        public void UpdateLView()
        {
            var currentStartups = AppData.Context.StartUp.ToList();

            currentStartups.RemoveAll(s => s.StartUpOfUser.Any(e => e.User.Name.Equals(User.Identity.Name) && e.RoleType.Name.Equals("Забанен")));

            if (ActualBox.Checked && !DoneBox.Checked)
            {
                currentStartups = currentStartups.Where(s => s.IsDone == false).ToList();
            }
            else if (!ActualBox.Checked && DoneBox.Checked)
            {
                currentStartups = currentStartups.Where(s => s.IsDone == true).ToList();
            }
            else if (!DoneBox.Checked && !ActualBox.Checked)
            {
                ActualBox.Checked = true;
                currentStartups = currentStartups.Where(s => s.IsDone == false).ToList();
            }

            UpdateFiltration.Update();

            if (ComboCategories.SelectedIndex != 0)
            {
                List<string> selectedValues = ComboCategories
                    .Items
                    .Cast<ListItem>()
                    .Where(i => i.Selected)
                    .Select(i => i.Value)
                    .ToList();
                currentStartups = currentStartups
                    .Where(s => selectedValues.Contains(s.Category.Name))
                    .ToList();
            }

            if (ComboCountries.SelectedIndex != 0)
            {
                List<string> selectedValues = ComboCountries
                    .Items
                    .Cast<ListItem>()
                    .Where(i => i.Selected)
                    .Select(i => i.Value)
                    .ToList();
                currentStartups = currentStartups
                    .Where(s => selectedValues.Contains(s.Region.Name))
                    .ToList();
            }

            if (ComboMaxMembers.SelectedIndex != 0)
            {
                List<string> selectedValues = ComboMaxMembers
                   .Items
                   .Cast<ListItem>()
                   .Where(i => i.Selected)
                   .Select(i => i.Value)
                   .ToList();

                List<StartUp> startUpsToUnion = new List<StartUp>();
                foreach (string value in selectedValues)
                {
                    string[] values = value.Split('-');
                    int from = int.Parse(values[0]);
                    int to = int.Parse(values[1]);

                    startUpsToUnion
                        .AddRange(currentStartups.Where(s => s.MaxMembersCount > from && s.MaxMembersCount < to)
                        .ToList());
                }

                currentStartups = startUpsToUnion.Distinct().ToList();
            }

            if (!string.IsNullOrWhiteSpace(NameBox.Text))
            {
                currentStartups = currentStartups.Where(s => s.Name.ToLower().Contains(NameBox.Text.ToLower())).ToList();
            }

            StartupsView.DataSource = currentStartups;
            StartupsView.DataBind();
        }

        /// <summary>
        /// Startups filtration by categories and names.
        /// </summary>
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            UpdateLView();
        }

        /// <summary>
        /// Pre-actions for clearing filtration values.
        /// </summary>
        protected void BtnClear_Click(object sender, EventArgs e)
        {
            ClearElements();
            UpdateLView();
        }

        /// <summary>
        /// Clears filtration values.
        /// </summary>
        private void ClearElements()
        {
            NameBox.Text = null;
            ActualBox.Checked = true;
            DoneBox.Checked = false;
            ComboCategories.SelectedIndex = 0;
            ComboMaxMembers.SelectedIndex = 0;
        }
    }
}