using StartUpWebAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StartUpWebAPI
{
    public partial class Startups : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            StartupsView.DataSource = AppData.Context.StartUp.ToList();
            StartupsView.DataBind();
        }
    }
}