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
        protected void Page_Load(object sender, EventArgs e)
        {
            string cookie = Request.Cookies[".ASPXAUTH"]?.Value;

            if (cookie != null)
            {
                AnonContent.Visible = false;
                LoggedInContent.Visible = true;
            }
            else
            {
                AnonContent.Visible = true;
                LoggedInContent.Visible = false;
            }
        }
    }
}