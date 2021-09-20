using StartUpWebAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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
            StartupsView.DataSource = AppData.Context.StartUp.ToList();
            StartupsView.DataBind();

            // TODO: SmoothlyAddStartups();
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
    }
}