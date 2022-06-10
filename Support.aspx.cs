using StartUpWebAPI.Entities;
using System;
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
            textBox.Text = String.Empty;
            LoadContacts();
            LoadMessages();
        }

        private void LoadContacts()
        {

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


                }
                var messages = entities.Message.Where(m => m.SenderId == Me.Id
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