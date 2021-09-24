using StartUpWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StartUpWebAPI
{
    public partial class Contact : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadBgImage();
        }

        private void LoadBgImage()
        {
            BgImage.ImageUrl = NativeImageUtils.ConvertFromBytes(Properties.Resources.Contact_image);
        }
    }
}