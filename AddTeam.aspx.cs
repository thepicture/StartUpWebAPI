using StartUpWebAPI.Entities;
using StartUpWebAPI.Models;
using System;
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
                TeamImage.ImageUrl = NativeImageUtils.ConvertFromBitmap(Properties.Resources.noPicture);

                LoadBgImg();
                ViewState["image"] = null;
                ViewState["imageDelete"] = false;
                ViewState["currentTeam"] = new Team();
                string idString = Request.QueryString.Get("id");
                InsertRegionsBox();

                if (idString != null)
                {
                    int id = int.Parse(idString);

                    bool teamIsNotNew = id != 0;

                    if (teamIsNotNew)
                    {
                        using (StartUpBaseEntities context = new StartUpBaseEntities())
                        {
                            Team nullableTeam = context.Team.Find(id);

                            if (nullableTeam != null)
                            {
                                ViewState["currentTeam"] = nullableTeam;

                                bool isTeamHasImage = nullableTeam.Image != null;

                                if (isTeamHasImage)
                                {
                                    ViewState["image"] = nullableTeam.Image;
                                }

                                TBoxName.Text = ((Team)ViewState["currentTeam"]).Name;
                                ContactBox.Text = ((Team)ViewState["currentTeam"]).ContactLink;
                                TBoxDescription.Text = ((Team)ViewState["currentTeam"]).Description;
                                TBoxMaxMembers.Text = ((Team)ViewState["currentTeam"]).MaxMembersCount.ToString();
                                InsertCurrentRegion();
                            }
                        }
                    }
                }
            }
            InsertImageIntoTeam();
        }

        private void InsertCurrentRegion()
        {
            Team team = (Team)ViewState["currentTeam"];
            DropDownRegions.Items.FindByValue(team.Region.Name).Selected = true;
        }

        private void InsertRegionsBox()
        {
            using (StartUpBaseEntities context = new StartUpBaseEntities())
            {
                var regions = context.Region.Select(c => c.Name).ToList();
                DropDownRegions.DataSource = regions;
                DropDownRegions.DataBind();
            }
        }

        /// <summary>
        /// Inserts the attached images into the team.
        /// </summary>
        private void InsertImageIntoTeam()
        {
            bool isImage = (byte[])ViewState["image"] != null;

            if (isImage)
            {
                TeamImage.ImageUrl = NativeImageUtils.ConvertFromBytes((byte[])ViewState["image"]);
                BtnRemoveImage.Visible = true;
            }
        }

        /// <summary>
        /// Loads background image.
        /// </summary>
        private void LoadBgImg()
        {
            BgImage.ImageUrl = NativeImageUtils.ConvertFromBitmap(Properties.Resources.commonBg);
        }

        /// <summary>
        /// Pre-actions for adding the attached images.
        /// </summary>
        protected void BtnAddImage_Click(object sender, EventArgs e)
        {
            var input = FileUploadImage.PostedFile;

            bool imageIsEmpty = input.ContentLength == 0;

            if (imageIsEmpty)
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

        /// <summary>
        /// Pre-actions for updating teams.
        /// </summary>
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            UpdateTeam();
        }

        /// <summary>
        /// Updates the team in DbContext.
        /// </summary>
        private void UpdateTeam()
        {
            string errors = "";

            bool badTeamName = string.IsNullOrWhiteSpace(TBoxName.Text);

            if (badTeamName)
            {
                errors += "Имя не должно быть пустым; ";
            }

            bool badMembersCountFormat = int.TryParse(TBoxMaxMembers.Text, out _);

            if (badMembersCountFormat)
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

            bool hasAnyErrors = errors.Length > 0;

            if (hasAnyErrors)
            {
                Response.Redirect("~/AddTeam?id="
                    + ((Team)ViewState["currentTeam"]).Id
                    + "&reason=" + HttpUtility.UrlEncode(errors));
                return;
            }

            using (StartUpBaseEntities context = new StartUpBaseEntities())
            {
                ((Team)ViewState["currentTeam"]).Name = TBoxName.Text;
                ((Team)ViewState["currentTeam"]).ContactLink = ContactBox.Text;
                ((Team)ViewState["currentTeam"]).Description = TBoxDescription.Text;
                ((Team)ViewState["currentTeam"]).MaxMembersCount = int.Parse(TBoxMaxMembers.Text);
                ((Team)ViewState["currentTeam"]).RegionId = context
                   .Region
                   .First(c => c.Name.Trim().Equals(DropDownRegions.SelectedValue.Trim()))
                   .Id;

                bool teamIsNew = ((Team)ViewState["currentTeam"]).Id == 0;
                if (teamIsNew)
                {
                    ((Team)ViewState["currentTeam"]).CreationDate = DateTime.Now;
                    ((Team)ViewState["currentTeam"]).TeamOfUser.Add(new TeamOfUser
                    {
                        User = context.User.First(u => u.Login.Equals(User.Identity.Name)),
                        RoleType = context.RoleType.First(r => r.Name.Equals("Организатор"))
                    });

                    context.Team.Add((Team)ViewState["currentTeam"]);
                }
                else
                {
                    int teamId = ((Team)ViewState["currentTeam"]).Id;
                    Team updatingTeam = context.Team.Find(teamId);

                    Team insertingTeam = (Team)ViewState["currentTeam"];
                    context.Entry(updatingTeam).CurrentValues.SetValues(insertingTeam);
                }

                Team addedTeam = null;
                int id;

                try
                {
                    context.SaveChanges();

                    id = ((Team)ViewState["currentTeam"]).Id;

                    addedTeam = context.Team.First(s => s.Id == id);

                    string reason = HttpUtility.UrlEncode("Команда успешно изменена!");

                    Response.Redirect("~/TeamInfo?id=" + ((Team)ViewState["currentTeam"]).Id + "&reason=" + reason, false);
                }
                catch (Exception)
                {
                    string reason = HttpUtility.UrlEncode("Команда не была изменена или добавлена." +
                        "Пожалуйста, попробуйте изменить команду ещё раз. ");

                    Response.Redirect("~/TeamInfo?id=" + ((Team)ViewState["currentTeam"]).Id + "&reason=" + reason);
                }

                var image = ViewState["image"] as byte[];

                bool isImageInDeleteState = (bool)ViewState["imageDelete"] == true;

                if (isImageInDeleteState)
                {
                    context.Entry(addedTeam).Entity.Image = null;
                    ViewState["image"] = null;
                    ViewState["imageDelete"] = false;
                }
                else
                {
                    addedTeam.Image = (byte[])ViewState["image"];
                }

                try
                {
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    Response
                        .Redirect(Request.RawUrl + "&reason=" + HttpUtility.UrlEncode(
                        "Не удалось добавить изображения в команду. " +
                        "Попробуйте ещё раз"));
                }
            }
        }

        /// <summary>
        /// Cancels editing/creating of a team.
        /// </summary>
        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            string reason = HttpUtility.UrlEncode("Создание или удаление команды было отменено!");

            Response.Redirect("~/TeamInfo.aspx?id=" + ((Team)ViewState["currentTeam"]).Id + "&reason=" + reason);
        }

        /// <summary>
        /// Remove image from the team.
        /// </summary>
        protected void BtnRemoveImage_Click(object sender, EventArgs e)
        {
            TeamImage.ImageUrl = NativeImageUtils.ConvertFromBitmap(Properties.Resources.noPicture);
            BtnRemoveImage.Visible = false;
            ViewState["image"] = null;
            InsertImageIntoTeam();
        }
    }
}