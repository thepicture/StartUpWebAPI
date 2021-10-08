using System;
using System.Web.UI;

namespace StartUpWebAPI
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (PageLoadedForTheFirstTime())
            {
                HideFooter();
            }
        }

        private bool PageLoadedForTheFirstTime()
        {
            return !Page.IsPostBack;
        }

        private void HideFooter()
        {
            Control footer = Master.FindControl("FooterIdentity");
            footer.Visible = false;
        }

        protected void BtnRegister_Click(object sender, EventArgs e)
        {
            RedirectUserToRegisterPage();
        }

        private void RedirectUserToRegisterPage()
        {
            Response.Redirect("~/Account/Register.aspx");
        }
    }
}