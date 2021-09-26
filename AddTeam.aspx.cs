using StartUpWebAPI.Entities;
using StartUpWebAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StartUpWebAPI
{
    public partial class AddTeam : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadBgImg();
                ViewState["image"] = null;
                ViewState["imageDelete"] = false;
                ViewState["currentTeam"] = new Team();
                string idString = Request.QueryString.Get("id");

                if (idString != null)
                {
                    int id = int.Parse(idString);

                    if (id != 0)
                    {
                        Team nullableTeam = AppData.Context.Team.Find(id);

                        if (nullableTeam != null)
                        {
                            ViewState["currentTeam"] = nullableTeam;

                            TBoxName.Text = ((Team)ViewState["currentTeam"]).Name;
                            TBoxDescription.Text = ((Team)ViewState["currentTeam"]).Description;
                            TBoxMaxMembers.Text = ((Team)ViewState["currentTeam"]).MaxMembersCount.ToString();

                            ViewState["image"] = nullableTeam.Image;
                            InsertImageIntoTeam();
                        }
                    }
                }
            }
        }

        private void InsertImageIntoTeam()
        {
            if ((byte[])ViewState["image"] != null)
            {
                TeamImage.ImageUrl = NativeImageUtils.ConvertFromBytes((byte[])ViewState["image"]);
            }
        }

        private void LoadBgImg()
        {
            BgImage.ImageUrl = NativeImageUtils.ConvertFromBitmap(Properties.Resources.commonBg);
        }

        protected void BtnAddImage_Click(object sender, EventArgs e)
        {
            var input = FileUploadImage.PostedFile;

            if (input.ContentLength == 0)
            {
                return;
            }

            bool isNotImage = !input.ContentType.Contains("image");

            if (isNotImage)
            {
                return;
            }

            Stream blob = input.InputStream;
            System.Drawing.Image image = NativeImageUtils.ConvertFromStream(blob);

            ViewState["image"] = NativeImageUtils.ConvertImageToBytes(image);
            ViewState["imageDelete"] = false;

            InsertImageIntoTeam();
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            UpdateTeam();
        }

        private void UpdateTeam()
        {
            string errors = "";

            if (string.IsNullOrWhiteSpace(TBoxName.Text))
            {
                errors += "Имя не должно быть пустым; ";
            }

            if (int.TryParse(TBoxMaxMembers.Text, out _))
            {
                if (int.Parse(TBoxMaxMembers.Text) < 0
                || string.IsNullOrWhiteSpace(TBoxMaxMembers.Text) || TBoxMaxMembers.Text.Length > 4)
                {
                    errors += "Количество участников - положительное число длиной от 1 до 4 цифр; ";
                }
            }
            else
            {
                errors += "Количество участников должно быть положительным числом, а не буквенным представлением.";
            }

            if (errors.Length > 0)
            {
                Response.Redirect("~/AddTeam?id="
                    + ((Team)ViewState["currentTeam"]).Id
                    + "&reason=" + HttpUtility.UrlEncode(errors), false);
                return;
            }

            ((Team)ViewState["currentTeam"]).Name = TBoxName.Text;
            ((Team)ViewState["currentTeam"]).Description = TBoxDescription.Text;
            ((Team)ViewState["currentTeam"]).MaxMembersCount = int.Parse(TBoxMaxMembers.Text);

            if (((Team)ViewState["currentTeam"]).Id == 0)
            {
                ((Team)ViewState["currentTeam"]).CreationDate = DateTime.Now;
                ((Team)ViewState["currentTeam"]).TeamOfUser.Add(new TeamOfUser
                {
                    User = AppData.Context.User.First(u => u.Login.Equals(User.Identity.Name)),
                    RoleType = AppData.Context.RoleType.First(r => r.Name.Equals("Организатор"))
                });

                AppData.Context.Team.Add((Team)ViewState["currentTeam"]);
            }
            else
            {
                int teamId = ((Team)ViewState["currentTeam"]).Id;
                Team updatingTeam = AppData.Context.Team.Find(teamId);

                Team insertingTeam = (Team)ViewState["currentTeam"];
                AppData.Context.Entry(updatingTeam).CurrentValues.SetValues(insertingTeam);
            }

            Team addedTeam = null;
            int id;

            try
            {
                AppData.Context.SaveChanges();

                id = ((Team)ViewState["currentTeam"]).Id;

                addedTeam = AppData.Context.Team.First(s => s.Id == id);

                string reason = HttpUtility.UrlEncode("Команда успешно изменена!");

                Response.Redirect("~/TeamInfo?id=" + ((Team)ViewState["currentTeam"]).Id + "&reason=" + reason, false);
            }
            catch (Exception)
            {
                string reason = HttpUtility.UrlEncode("Команда не была изменена или добавлена." +
                    "Пожалуйста, попробуйте изменить команду ещё раз. ");

                Response.Redirect("~/TeamInfo?id=" + ((Team)ViewState["currentTeam"]).Id + "&reason=" + reason, false);
            }

            var image = ViewState["image"] as byte[];

            if ((bool)ViewState["imageDelete"] == false)
            {
                addedTeam.Image = image;
            }
            else
            {
                addedTeam.Image = null;
            }

            try
            {
                AppData.Context.SaveChanges();
            }
            catch (Exception)
            {
                Response
                    .Redirect(Request.RawUrl + "&reason=" + HttpUtility.UrlEncode(
                    "Не удалось добавить изображения в команду. " +
                    "Попробуйте ещё раз"), false);
            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            string reason = HttpUtility.UrlEncode("Создание или удаление стартапа было отменено!");

            Response.Redirect("~/TeamInfo.aspx?id=" + ((Team)ViewState["currentTeam"]).Id + "&reason=" + reason);
        }

        protected void BtnRemoveImage_Click(object sender, EventArgs e)
        {
            TeamImage.ImageUrl = NativeImageUtils.ConvertFromBitmap(Properties.Resources.noPicture);
            BtnRemoveImage.Visible = false;
        }
    }
}