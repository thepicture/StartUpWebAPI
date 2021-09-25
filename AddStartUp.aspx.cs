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
            if (!Page.IsPostBack)
            {
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
                            InsertCategory();
                            InsertDocumentsIntoStartUp();
                            InsertImagesIntoStartUp();
                        }
                    }
                }
            }
        }

        private void InsertDocumentsIntoStartUp()
        {
            LViewDocuments.DataSource = ((StartUp)ViewState["currentStartUp"]).DocumentOfStartUp.ToList();
            LViewDocuments.DataBind();
        }

        private void InsertImagesIntoStartUp()
        {
            LViewImages.DataSource = ((StartUp)ViewState["currentStartUp"]).StartUpImage.ToList();
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

            AppData.Context.StartUp.AddOrUpdate((StartUp)ViewState["currentStartUp"]);

            try
            {
                AppData.Context.SaveChanges();

                string reason = HttpUtility.UrlEncode("Стартап успешно изменён!");

                Response.Redirect("~/StartUpInfo?id=" + ((StartUp)ViewState["currentStartUp"]).Id + "&reason=" + reason, false);
            }
            catch (Exception ex)
            {
                string reason = HttpUtility.UrlEncode("Стартап не был изменен или добавлен." +
                    "Пожалуйста, попробуйте изменить стартап ещё раз. " + ex.Message);

                Response.Redirect("~/StartUpInfo?id=" + ((StartUp)ViewState["currentStartUp"]).Id + "&reason=" + reason, false);
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
                StartUpImage imageToRemove = AppData.Context.StartUpImage.Find(int.Parse((string)e.CommandArgument));

                if (imageToRemove != null)
                {
                    AppData.Context.StartUpImage.Remove(imageToRemove);

                    try
                    {
                        AppData.Context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Response.Redirect("~/AddStartUp.aspx?id=" + ((StartUp)ViewState["currentStartUp"]).Id
                            + "&reason="
                            + HttpUtility.UrlEncode("Изображение не было удален из стартапа. Попробуйте ещё раз. " + ex.Message), false);
                    }
                }

                ((StartUp)ViewState["currentStartUp"]).StartUpImage.Remove(((StartUp)ViewState["currentStartUp"])
                    .StartUpImage
                    .First(i => i.Id.Equals(int.Parse((string)e.CommandArgument))));

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
                bool isNotImage = !p.ContentType.Contains("image");

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
                    StartUp = ViewState["currentStartUp"] as StartUp
                };

                AppData.Context.StartUpImage.Add(newImage);

                try
                {
                    AppData.Context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/AddStartUp.aspx?id=" + ((StartUp)ViewState["currentStartUp"]).Id
                        + "&reason="
                        + HttpUtility.UrlEncode("Изображение не было добавлено в стартап. Попробуйте ещё раз. " + ex.Message), false);
                }

                ((StartUp)ViewState["currentStartUp"]).StartUpImage.Add(AppData.Context.Entry(newImage).Entity);
            });

            InsertImagesIntoStartUp();
        }

        protected void LViewDocuments_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "RemoveDocument")
            {
                DocumentOfStartUp doc = AppData.Context.DocumentOfStartUp.Find(int.Parse((string)e.CommandArgument));

                if (doc != null)
                {
                    AppData.Context.DocumentOfStartUp.Remove(doc);

                    try
                    {
                        AppData.Context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Response.Redirect("~/AddStartUp.aspx?id=" + ((StartUp)ViewState["currentStartUp"]).Id
                            + "&reason="
                            + HttpUtility.UrlEncode("Документ не был удален из стартапа. Попробуйте ещё раз. " + ex.Message), false);
                    }
                }

                ((StartUp)ViewState["currentStartUp"]).DocumentOfStartUp.Remove(((StartUp)ViewState["currentStartUp"])
                    .DocumentOfStartUp
                    .First(d => d.Id.Equals(int.Parse((string)e.CommandArgument))));

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
                bool contentIsGreaterThanFiveMb = p.ContentLength > 1024 * 1024 * 5;

                if (contentIsGreaterThanFiveMb)
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
                    StartUp = ViewState["currentStartUp"] as StartUp
                };

                AppData.Context.DocumentOfStartUp.Add(doc);

                try
                {
                    AppData.Context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/AddStartUp.aspx?id=" + ((StartUp)ViewState["currentStartUp"]).Id
                        + "&reason="
                        + HttpUtility.UrlEncode("Документ не был добавлен в стартап. Попробуйте ещё раз. " + ex.Message), false);
                }

                ((StartUp)ViewState["currentStartUp"]).DocumentOfStartUp.Add(AppData.Context.Entry(doc).Entity);
            });

            InsertDocumentsIntoStartUp();
        }
    }
}