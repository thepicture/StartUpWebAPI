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
    public partial class _Default : Page
    {
        private string username;
        protected void Page_Load(object sender, EventArgs e)
        {
            username = CookieUtils.GetUserNameOrNullOf(Context.Request);
            string isAuthorized = Request.Cookies[".ASPXAUTH"]?.Value;

            if (isAuthorized != null)
            {
                AnonContent.Visible = false;
                LoggedInContent.Visible = true;
                LoadStartups();
                LoadTeams();
            }
            else
            {
                AnonContent.Visible = true;
                LoggedInContent.Visible = false;
            }

        }

        private void LoadStartups()
        {
            LViewMyStartups.DataSource = AppData.Context.User.First(u => u.Login.Equals(username)).StartUpOfUser.Select(s => s.StartUp).ToList();
            LViewMyStartups.DataBind();
        }

        private void LoadTeams()
        {
            
        }
    }
}