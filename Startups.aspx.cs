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
        private List<StartUp> startups;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!UserAuthorizeObserver.IsAuthorized(Request))
            {
                FormsAuthentication.RedirectToLoginPage();
            }
            else
            {
                StartupsView.DataSource = AppData.Context.StartUp.ToList();
                StartupsView.DataBind();
                //SmoothlyAddStartups();
            }
        }

        private void SmoothlyAddStartups()
        {
            startups = AppData.Context.StartUp.ToList();

            Timer timer = new Timer
            {
                Interval = Convert.ToInt32(Properties.Resources.SmoothAddInterval)
            };
            timer.Tick += AddNewItem;

            timer.Start();
        }

        public object RedirectToPage(object sender, EventArgs e, string id)
        {
            Response.Redirect("~/StartUpInfo.aspx?id=" + Convert.ToInt32(id));

            return new object();
        }

        private void AddNewItem(object sender, EventArgs e)
        {
            MessageBox.Show(startups.Count().ToString());
            if (startups.Count() == 0)
            {
                (sender as DispatcherTimer).Stop();
                return;
            }
            else
            {
                Dispatcher.CurrentDispatcher.Invoke(() =>
                {
                    StartupsView.DataSource = startups.Take(1);
                    StartupsView.DataBind();
                    startups = startups.Skip(1).ToList();
                });
            }
        }

        protected void StartupsView_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("StartUpClicked"))
            {
                Response.Redirect("~/StartUpInfo.aspx?id=" + e.CommandArgument);
            }
        }

        public void UpdateLView()
        {

        }

        protected void NameBox_TextChanged(object sender, EventArgs e)
        {
            StartupsView.DataSource = AppData.Context.StartUp.ToList().Where(s => s.Name.Contains(NameBox.Text)).ToList();
            StartupsView.DataBind();
           
        }
    }
}