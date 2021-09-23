using StartUpWebAPI.Entities;
using StartUpWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Windows.Threading;

namespace StartUpWebAPI
{
    public partial class Startups : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!UserAuthorizeObserver.IsAuthorized(Request))
            {
                FormsAuthentication.RedirectToLoginPage();
            }
            else
            {
                UpdateLView();
                if (!IsPostBack)
                {
                    InsertCategoriesBox();
                }

            }
        }

        /// <summary>
        /// Inserts categories into DropDownList.
        /// </summary>
        private void InsertCategoriesBox()
        {
            var categories = AppData.Context.Category.Select(c => c.Name).ToList();
            categories.Insert(0, "Все категории");
            ComboCategories.DataSource = categories;
            ComboCategories.DataBind();
            ComboCategories.SelectedIndex = 0;
        }

        /// <summary>
        /// Redirect to the selected startup.
        /// </summary>
        protected void StartupsView_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("StartUpClicked"))
            {
                Response.Redirect("~/StartUpInfo.aspx?id=" + e.CommandArgument);
            }
        }

        /// <summary>
        /// Inserts startups into ListView.
        /// </summary>
        public void UpdateLView()
        {
            StartupsView.DataSource = AppData.Context.StartUp.ToList();
            StartupsView.DataBind();
        }

        /// <summary>
        /// Startups filtration by categories and names.
        /// </summary>
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            var currentStartups = AppData.Context.StartUp.ToList();

            UpdateFiltration.Update();

            if (ComboCategories.SelectedIndex != 0)
            {
                currentStartups = currentStartups.Where(s => s.Category.Name.Equals(ComboCategories.SelectedValue)).ToList();
            }
            if (!string.IsNullOrWhiteSpace(NameBox.Text))
            {
                currentStartups = currentStartups.Where(s => s.Name.ToLower().Contains(NameBox.Text.ToLower())).ToList();
            }

            StartupsView.DataSource = currentStartups;
            StartupsView.DataBind();
        }
    }
}