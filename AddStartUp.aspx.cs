using StartUpWebAPI.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StartUpWebAPI
{
    public partial class AddStartUp : System.Web.UI.Page
    {
        public StartUp currentStartUp = new StartUp();
        public int id;

        protected void Page_Load(object sender, EventArgs e)
        {
            string maybeId = Request.QueryString.Get("id");

            if (maybeId != null)
            {
                id = int.Parse(maybeId);

                currentStartUp = AppData.Context.StartUp.Find(id);
            }
            else
            {
                id = 0;
            }

            if (!Page.IsPostBack)
            {
                InsertCategoriesBox();
                TryToFindStartUp();

                TBoxName.Text = currentStartUp.Name;
                TBoxDescription.Text = currentStartUp.Description;
                TBoxMaxMembers.Text = currentStartUp.MaxMembersCount.ToString();
            }

            if (currentStartUp.Category?.Name != null)
            {
                ComboCategories.Items.FindByValue(currentStartUp.Category.Name).Selected = true;
            }
        }

        private void TryToFindStartUp()
        {
            if (id != 0)
            {
                currentStartUp = AppData.Context.StartUp.Find(id);

                if (currentStartUp != null)
                {
                    TBoxName.Text = currentStartUp.Name;
                    TBoxDescription.Text = currentStartUp.Description;
                    TBoxMaxMembers.Text = currentStartUp.MaxMembersCount.ToString();
                }
                else
                {
                    string reason = HttpUtility.UrlEncode("Ошибка 404. Стартап не был найден." +
                        "Пожалуйста, попробуйте найти другой стартап.");

                    Response.Redirect("~/Default?reason=" + reason);
                }

            }
        }

        /// <summary>
        /// Insert category names into categories box.
        /// </summary>
        private void InsertCategoriesBox()
        {
            var categories = AppData.Context.Category.Select(c => c.Name).ToList();
            categories.Insert(0, "Все категории");
            ComboCategories.DataSource = categories;
            ComboCategories.DataBind();
        }

        /// <summary>
        /// Actions for saving the new or modified startup.
        /// </summary>
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            bool isStartUpNew = id == 0;

            if (isStartUpNew)
            {
                CreateStartUp();
            }
            else
            {
                UpdateStartUp();
            }
        }

        private void UpdateStartUp()
        {
            currentStartUp.Name = TBoxName.Text;
            currentStartUp.Description = TBoxDescription.Text;
            currentStartUp.MaxMembersCount = int.Parse(TBoxMaxMembers.Text);
            currentStartUp.Category = AppData.Context.Category.First(c => c.Name.Equals(ComboCategories.SelectedValue));

            AppData.Context.StartUp.AddOrUpdate(currentStartUp);
            try
            {
                AppData.Context.SaveChanges();

                string reason = HttpUtility.UrlEncode("Стартап успешно изменён!");

                Response.Redirect("~/Default?reason=" + reason);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Creates a new startup or modify existing.
        /// </summary>
        private void CreateStartUp()
        {
            currentStartUp = new StartUp
            {
                Name = TBoxName.Text,
                Description = TBoxDescription.Text,
                Category = AppData.Context.Category.First(c => c.Name.Equals(ComboCategories.SelectedValue)),
                CreationDate = DateTime.Now,
                IsDone = false,
                MaxMembersCount = int.Parse(TBoxMaxMembers.Text)
            };

            User user = AppData.Context.User.First(u => u.Login.ToLower().Equals(User.Identity.Name.ToLower()));
            RoleType role = AppData.Context.RoleType.First(r => r.Name.Equals("Организатор"));

            StartUpOfUser userStartUp = new StartUpOfUser
            {
                User = user,
                RoleType = role,
                StartUp = currentStartUp
            };

            currentStartUp.StartUpOfUser.Add(userStartUp);

            AppData.Context.StartUp.Add(currentStartUp);

            try
            {
                AppData.Context.SaveChanges();

                string reason = HttpUtility.UrlEncode("Стартап был успешно сохранён!");

                Response.Redirect("~/Default.aspx?reason=" + reason, false);
            }
            catch (Exception ex)
            {
                string reason = HttpUtility.UrlEncode("Стартап не был сохранен." +
                    "Ошибка: " + ex.Message + ".\n" +
                    "Пожалуйста, попробуйте добавить или изменить стартап ещё раз.");

                Response.Redirect("~/Default.aspx?reason" + reason);
            }
        }

        /// <summary>
        /// Cancels the creation or modification of the startup.
        /// </summary>
        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            string reason = HttpUtility.UrlEncode("Создание или удаление стартапа было отменено!");

            Response.Redirect("~/Default.aspx?reason=" + reason);
        }
    }
}