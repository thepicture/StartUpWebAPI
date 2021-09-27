using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace StartUpWebAPI
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Control footer = Master.FindControl("FooterIdentity");
            footer.Visible = false;
        }

        /// <summary>
        /// Redirects the user to the register page.
        /// </summary>
        protected void BtnRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Account/Register.aspx");
        }
    }
}