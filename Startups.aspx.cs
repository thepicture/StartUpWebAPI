using StartUpWebAPI.Entities;
using StartUpWebAPI.Models;
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
                CheckOrSetDropDownBoxes();
                UpdateLView();
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
                FillRegionsBox();
                FillCategoriesBox();
            }
        }

        /// <summary>
        /// Inserts the categories into dropdown list.
        /// </summary>
        private void FillCategoriesBox()
        {
            using (StartUpBaseEntities context = new StartUpBaseEntities())
            {
                CategoriesView.DataSource = context.Category.ToList();
                CategoriesView.DataBind();
                AssignAnyValueForCategories();
            }
        }

        /// <summary>
        /// Sets the checkbox state of items to false in CategoriesView.
        /// </summary>
        private void AssignAnyValueForCategories()
        {
            ListViewTupleGetter.Get(CategoriesView).ToList().ForEach(t => t.Item2.Checked = false);
        }

        /// <summary>
        /// Inserts the regions into dropdown list.
        /// </summary>
        private void FillRegionsBox()
        {
            using (StartUpBaseEntities context = new StartUpBaseEntities())
            {
                RegionsView.DataSource = context.Region.ToList();
                RegionsView.DataBind();
                AssignAnyValueForRegions();
            }
        }

        /// <summary>
        /// Sets the checkbox state of items to false in RegionsView.
        /// </summary>
        private void AssignAnyValueForRegions()
        {
            ListViewTupleGetter.Get(RegionsView).ToList().ForEach(t => t.Item2.Checked = false);
        }

        /// <summary>
        /// Inserts members parameters into the ComboBox.
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
        /// Inserts startups into ListView.
        /// </summary>
        public void UpdateLView()
        {
            using (StartUpBaseEntities context = new StartUpBaseEntities())
            {
                var currentStartups = context.StartUp.ToList();

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

                #region WorkWithDropDownBoxes
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

                List<string> categoriesSelectedValues = TupleValueGetter.GetValues(
                                                      TupleToTextAndBoolConverter.ConvertToTextAndBoolTuple(
                                                          ListViewTupleGetter.Get(CategoriesView)
                                                          )
                                                      )
                .ToList();

                bool memberSelectedValueIsNonStandard = membersSelectedValues.Count != 0;
                bool regionSelectedValueIsNonStandard = regionsSelectedValues.Count != 0;
                bool categorySelectedValueIsNonStandard = categoriesSelectedValues.Count != 0;

                if (memberSelectedValueIsNonStandard)
                {
                    List<StartUp> startupsToUnion = new List<StartUp>();

                    foreach (string value in membersSelectedValues)
                    {
                        string[] values = value.Split('-');
                        int from = int.Parse(values[0]);
                        int to = int.Parse(values[1].Replace("и больше", int.MaxValue.ToString()));

                        startupsToUnion
                            .AddRange(currentStartups.Where(s => s.MaxMembersCount > from
                                        && s.MaxMembersCount < to)
                                        .ToList());
                    }
                    currentStartups = startupsToUnion.Distinct().ToList();
                }

                if (regionSelectedValueIsNonStandard)
                {
                    currentStartups = currentStartups
                        .Where(s => regionsSelectedValues.Contains(s.Region.Name))
                        .ToList();
                }

                if (categorySelectedValueIsNonStandard)
                {
                    currentStartups = currentStartups
                        .Where(s => categoriesSelectedValues.Contains(s.Category.Name))
                        .ToList();
                }
                #endregion

                UpdateFiltration.Update();

                if (!string.IsNullOrWhiteSpace(NameBox.Text))
                {
                    currentStartups = currentStartups.Where(s => s.Name.ToLower().Contains(NameBox.Text.ToLower())).ToList();
                }

                StartupsView.DataSource = currentStartups;
                StartupsView.DataBind();
            }
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
            AssignAnyValueForCategories();
            AssignAnyValueForMembers();
            AssignAnyValueForRegions();
        }
    }
}