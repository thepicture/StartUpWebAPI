using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using StartUpWebAPI.Models;

namespace StartUpWebAPI.Account
{
    public partial class Register : Page
    {
        protected void CreateUser_Click(object sender, EventArgs e)
        {
           if (IsValid)
            {
                FullNameBox.Text = "Success";
            }
        }
    }
}