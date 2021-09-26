using StartUpWebAPI.Entities;
using StartUpWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StartUpWebAPI
{
    public partial class AddStartUp : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadBgImage();

            if (!Page.IsPostBack)
            {
                ViewState["images"] = new List<StartUpImage>();
                ViewState["documents"] = new List<DocumentOfStartUp>();
                ViewState["currentStartUp"] = new StartUp();
                string idString = Request.QueryString.Get("id");

                InsertCategoriesBox();

                if (idString != null)
                {
                    int id = int.Parse(idString);

                    if (id != 0)
                    {
                        StartUp nullableStartUp = AppData.Context.StartUp.Find(id);

                        if (nullableStartUp != null)
                        {
                            ViewState["currentStartUp"] = nullableStartUp;

                            TBoxName.Text = ((StartUp)ViewState["currentStartUp"]).Name;
                            TBoxDescription.Text = ((StartUp)ViewState["currentStartUp"]).Description;
                            TBoxMaxMembers.Text = ((StartUp)ViewState["currentStartUp"]).MaxMembersCount.ToString();
                            CheckBoxDone.Checked = ((StartUp)ViewState["currentStartUp"]).IsDone;
                            ((List<StartUpImage>)ViewState["images"]).AddRange(nullableStartUp.StartUpImage.ToList());
                            ((List<DocumentOfStartUp>)ViewState["documents"]).AddRange(nullableStartUp.DocumentOfStartUp.ToList());
                            InsertCategory();
                            InsertDocumentsIntoStartUp();
                            InsertImagesIntoStartUp();
                        }
                    }
                }
            }
        }

        private void LoadBgImage()
        {
            BgImage.ImageUrl = NativeImageUtils.ConvertFromBitmap(Properties.Resources.commonBg);
        }

        private void InsertDocumentsIntoStartUp()
        {
            LViewDocuments.DataSource = ((List<DocumentOfStartUp>)ViewState["documents"]).Where(i => i.Id >= 0).ToList();
            LViewDocuments.DataBind();
        }

        private void InsertImagesIntoStartUp()
        {
            LViewImages.DataSource = ((List<StartUpImage>)ViewState["images"]).Where(i => i.Id >= 0).ToList();
            LViewImages.DataBind();
        }

        private void InsertCategory()
        {
            ComboCategories.Items.FindByValue(((StartUp)ViewState["currentStartUp"]).Category.Name).Selected = true;
        }


        /// <summary>
        /// Insert category names into categories box.
        /// </summary>
        private void InsertCategoriesBox()
        {
            var categories = AppData.Context.Category.Select(c => c.Name).ToList();
            ComboCategories.DataSource = categories;
            ComboCategories.DataBind();
        }

        /// <summary>
        /// Actions for saving the new or modified startup.
        /// </summary>
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            UpdateStartUp();
        }

        private void UpdateStartUp()
        {
            string errors = "";

            if (string.IsNullOrWhiteSpace(TBoxName.Text))
            {
                errors += "Имя не должно быть пустым\n";
            }

            if (int.TryParse(TBoxMaxMembers.Text, out _))
            {
                if (int.Parse(TBoxMaxMembers.Text) < 0
                || string.IsNullOrWhiteSpace(TBoxMaxMembers.Text) || TBoxMaxMembers.Text.Length > 4)
                {
                    errors += "Количество участников - положительное число длиной от 1 до 4 цифр\n";
                }
            }
            else
            {
                errors += "Количество участников должно быть положнительным числом, а не буквенным представлением\n";
            }



            if (errors.Length > 0)
            {
                Response.Redirect("~/AddStartUp?id="
                    + ((StartUp)ViewState["currentStartUp"]).Id
                    + "&reason=" + HttpUtility.UrlEncode(errors), false);
                return;
            }

            ((StartUp)ViewState["currentStartUp"]).Name = TBoxName.Text;
            ((StartUp)ViewState["currentStartUp"]).Description = TBoxDescription.Text;
            ((StartUp)ViewState["currentStartUp"]).MaxMembersCount = int.Parse(TBoxMaxMembers.Text);
            ((StartUp)ViewState["currentStartUp"]).Category = AppData.Context.Category.First(c => c.Name.Equals(ComboCategories.SelectedValue));
            ((StartUp)ViewState["currentStartUp"]).IsDone = CheckBoxDone.Checked;

            if (((StartUp)ViewState["currentStartUp"]).Id == 0)
            {
                ((StartUp)ViewState["currentStartUp"]).CreationDate = DateTime.Now;
                ((StartUp)ViewState["currentStartUp"]).StartUpOfUser.Add(new StartUpOfUser
                {
                    User = AppData.Context.User.First(u => u.Login.Equals(User.Identity.Name)),
                    RoleType = AppData.Context.RoleType.First(r => r.Name.Equals("Организатор"))
                });
            }

            if (((StartUp)ViewState["currentStartUp"]).Id == 0)
            {
                AppData.Context.StartUp.Add((StartUp)ViewState["currentStartUp"]);
            }
            else
            {
                StartUp updatingStartUp = AppData.Context.StartUp.Find(((StartUp)ViewState["currentStartUp"]).Id);

                AppData.Context.Entry(updatingStartUp).CurrentValues.SetValues(((StartUp)ViewState["currentStartUp"]));
            }

            try
            {
                AppData.Context.SaveChanges();

                StartUp addedStartUp = AppData.Context.StartUp.First(s => s.CreationDate == ((StartUp)ViewState["currentStartUp"]).CreationDate);

                string reason = HttpUtility.UrlEncode("Стартап успешно изменён!");

                Response.Redirect("~/StartUpInfo?id=" + ((StartUp)ViewState["currentStartUp"]).Id + "&reason=" + reason, false);
            }
            catch (Exception ex)
            {
                string reason = HttpUtility.UrlEncode("Стартап не был изменен или добавлен." +
                    "Пожалуйста, попробуйте изменить стартап ещё раз. " + ex.Message);

                Response.Redirect("~/StartUpInfo?id=" + ((StartUp)ViewState["currentStartUp"]).Id + "&reason=" + reason, false);
            }

            foreach (var image in (List<StartUpImage>)ViewState["images"])
            {
                int id = ((StartUp)ViewState["currentStartUp"]).Id;
                StartUp addedStartUp = AppData.Context.StartUp.First(s => s.Id == id);
                image.StartUpId = addedStartUp.Id;

                if (image.Id >= 0 && !AppData.Context.StartUpImage.Any(i => i.Name.Equals(image.Name)))
                {
                    AppData.Context.StartUpImage.Add(image);
                }
                else if (image.Id < 0 && AppData.Context.StartUpImage.Any(i => i.Name.Equals(image.Name)))
                {
                    AppData.Context.StartUpImage.Remove(AppData.Context.StartUpImage.First(i => i.Name.Equals(image.Name)));
                }


                try
                {
                    AppData.Context.SaveChanges();
                }
                catch (Exception)
                {
                    Response
                        .Redirect(Request.RawUrl + "&reason=" + HttpUtility.UrlEncode(
                        "Не удалось добавить изображения в стартап. " +
                        "Попробуйте ещё раз"), false);
                }
            }

            foreach (var doc in (List<DocumentOfStartUp>)ViewState["documents"])
            {
                int id = ((StartUp)ViewState["currentStartUp"]).Id;
                StartUp addedStartUp = AppData.Context.StartUp.First(s => s.Id == id);
                doc.StartUpId = addedStartUp.Id;

                if (doc.Id >= 0 && !AppData.Context.DocumentOfStartUp.Any(i => i.FileName.Equals(doc.FileName)))
                {
                    AppData.Context.DocumentOfStartUp.Add(doc);
                }
                else if (doc.Id < 0 && AppData.Context.DocumentOfStartUp.Any(i => i.FileName.Equals(doc.FileName)))
                {
                    AppData.Context.DocumentOfStartUp.Remove(AppData.Context.DocumentOfStartUp.First(i => i.FileName.Equals(doc.FileName)));
                }


                try
                {
                    AppData.Context.SaveChanges();
                }
                catch (Exception)
                {
                    Response
                         .Redirect(Request.RawUrl + "&reason=" + HttpUtility.UrlEncode(
                         "Не удалось добавить документы в стартап. " +
                         "Попробуйте ещё раз"), false);
                }
            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            string reason = HttpUtility.UrlEncode("Создание или удаление стартапа было отменено!");

            Response.Redirect("~/StartUpInfo.aspx?id=" + ((StartUp)ViewState["currentStartUp"]).Id + "&reason=" + reason);
        }

        protected void LViewImages_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "RemoveImage")
            {
                ((List<StartUpImage>)ViewState["images"])
                    .Find(i => i.Name.Equals(e.CommandArgument))
                    .Id = -1;

                InsertImagesIntoStartUp();
            }
        }

        protected void BtnAddImages_Click(object sender, EventArgs e)
        {
            if (FileUploadImages.PostedFiles[0].ContentLength == 0)
            {
                return;
            }

            List<HttpPostedFile> photos = FileUploadImages.PostedFiles.ToList();

            photos.ForEach(p =>
            {
                bool isNotImage = !p.ContentType.Contains("image") || ((List<StartUpImage>)ViewState["images"]).Any(i => i.Name.Equals(p.FileName));

                if (isNotImage)
                {
                    return;
                }

                Stream blob = p.InputStream;
                System.Drawing.Image image = NativeImageUtils.ConvertFromStream(blob);

                StartUpImage newImage = new StartUpImage
                {
                    Name = p.FileName,
                    Image = NativeImageUtils.ConvertImageToBytes(image),
                };

                (ViewState["images"] as List<StartUpImage>).Add(newImage);
            });

            InsertImagesIntoStartUp();
        }

        protected void LViewDocuments_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "RemoveDocument")
            {
                ((List<DocumentOfStartUp>)ViewState["documents"])
                    .Find(i => i.FileName.Equals(e.CommandArgument))
                    .Id = -1;

                InsertDocumentsIntoStartUp();
            }
        }

        protected void BtnAddDocuments_Click(object sender, EventArgs e)
        {
            if (DocumentUpload.PostedFiles[0].ContentLength == 0)
            {
                return;
            }

            List<HttpPostedFile> docs = DocumentUpload.PostedFiles.ToList();

            docs.ForEach(p =>
            {
                bool contentIsGreaterThanFiveMbOrExists = p.ContentLength > 1024 * 1024 * 5
                || ((List<DocumentOfStartUp>)ViewState["documents"])
                .Any(i => i.FileName.Equals(p.FileName));

                if (contentIsGreaterThanFiveMbOrExists)
                {
                    return;
                }

                Stream blob = p.InputStream;

                DocumentOfStartUp doc = new DocumentOfStartUp
                {
                    FileName = p.FileName,
                    CreationDate = DateTime.Now,
                    Blob = StreamToBytesConverter.Convert(blob),
                    IsPublic = true,
                };

                (ViewState["documents"] as List<DocumentOfStartUp>).Add(doc);
            });

            InsertDocumentsIntoStartUp();
        }
    }
}