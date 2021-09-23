using StartUpWebAPI.Entities;
using StartUpWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StartUpWebAPI
{
    public partial class Teams : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
            }
            else
            {
                TeamsView.DataSource = AppData.Context.Team.ToList();
                TeamsView.DataBind();
            }
        }

        protected void TeamsView_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "TeamClicked")
            {
                Response.Redirect("~/TeamInfo.aspx?id=" + e.CommandArgument);
            }
        }
    }
}