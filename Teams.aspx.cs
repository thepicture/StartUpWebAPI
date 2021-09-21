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
            if (!UserAuthorizeObserver.IsAuthorized(Request))
            {
                FormsAuthentication.RedirectToLoginPage();
            }
            else
            {
                //SmoothlyAddStartups();
            }
        }
    }
}