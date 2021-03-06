using StartUpWebAPI.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace StartUpWebAPI
{
    public partial class Support : System.Web.UI.Page
    {
        public User Me
        {
            get
            {
                using (StartUpBaseEntities entities = new StartUpBaseEntities())
                {
                    var user = entities.User
                        .Include(u => u.Message)
                        .Include(u => u.Message1)
                        .Include(u => u.TypeOfUser)
                        .First(u => u.Login == User.Identity.Name);
                    return user;
                }
            }
        }

        public User Receiver { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/Register.aspx");
                return;
            }
            if (Page.IsPostBack)
            {
                string text = textBox.Text;
                int senderId = (int)Session[nameof(Me)];
                int receiverId = (int)Session[nameof(Receiver)];
                if (text != null && !string.IsNullOrWhiteSpace(text))
                {
                    using (StartUpBaseEntities entities = new StartUpBaseEntities())
                    {
                        Message message = new Message
                        {
                            ReceiverId = receiverId,
                            SenderId = senderId,
                            Text = text
                        };
                        entities.Message.Add(message);
                        entities.SaveChanges();
                    }
                }
            }
            textBox.Text = string.Empty;
            LoadContacts();
            LoadMessages();
        }

        private void LoadContacts()
        {
            using (StartUpBaseEntities entities = new StartUpBaseEntities())
            {
                IEnumerable<User> contacts;
                if (Me.TypeOfUser.Name == "Админ")
                {
                    contacts = entities.User
                        .Include(u => u.Message)
                        .Include(u => u.Message1)
                        .Where(u => u.Id != Me.Id);
                }
                else
                {
                    contacts = entities.User

                        .Include(u => u.Message)
                        .Include(u => u.Message1)
                        .Where(u => u.TypeOfUser.Name == "Админ");
                }
                ContactsView.DataSource = contacts.ToList();
                ContactsView.DataBind();
            }
        }

        private void LoadMessages()
        {
            using (StartUpBaseEntities entities = new StartUpBaseEntities())
            {
                int receiverId;
                if (Request.QueryString["receiverId"] != null)
                {
                    receiverId = int.Parse(Request.QueryString["receiverId"]);
                }
                else
                {
                    if (Me.TypeOfUser.Name == "Админ")
                    {
                        receiverId = entities.User.First(u => u.TypeOfUser.Name != "Админ").Id;
                    }
                    else
                    {
                        receiverId = entities.User.First(u => u.TypeOfUser.Name == "Админ").Id;
                    }
                    Response.Redirect("~/Support.aspx?receiverId=" + receiverId);
                }
                var messages = entities.Message
                    .Include(m => m.User)
                    .Include(m => m.User1)
                    .Where(m => m.SenderId == Me.Id
                                && m.ReceiverId == receiverId
                                || m.SenderId == receiverId
                                && m.ReceiverId == Me.Id)
                    .ToList();
                MessagesView.DataSource = messages;
                MessagesView.DataBind();
                Receiver = entities.User.Find(receiverId);
                ReceiverImage.ImageUrl = Receiver.UserImageInCommentOrDefault;

                Session[nameof(Me)] = Me.Id;
                Session[nameof(Receiver)] = Receiver.Id;
            }
        }
    }
}