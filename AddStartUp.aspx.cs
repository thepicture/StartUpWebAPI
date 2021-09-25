﻿using StartUpWebAPI.Entities;
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
        public StartUp currentStartUp = new StartUp();
        public int id;

        protected void Page_Load(object sender, EventArgs e)
        {
            string maybeId = Request.QueryString.Get("id");

            if (maybeId != null)
            {
                CheckIfStartupIsNewAndPrepare(maybeId);
            }
            else
            {
                id = 0;
            }

            if (!Page.IsPostBack)
            {
                InsertCategoriesBox();
                TryToFindStartUp();
                InsertCategory();
            }
        }

        private void CheckIfStartupIsNewAndPrepare(string maybeId)
        {
            if (int.Parse(maybeId) != 0)
            {
                PrepareStartUp(maybeId);
            }
        }

        private void PrepareStartUp(string maybeId)
        {
            id = int.Parse(maybeId);

            currentStartUp = AppData.Context.StartUp.Find(id);

            if (!IsPostBack || 0 == 0)
            {
                InsertImagesIntoStartUp();
                InsertDocumentsIntoStartUp();
            }

        }

        private void InsertImagesIntoStartUp()
        {
            if (currentStartUp != null)
            {
                LViewImages.DataSource = currentStartUp.StartUpImage.ToList();
                LViewImages.DataBind();
            }
        }

        private void InsertCategory()
        {
            if (currentStartUp.Category?.Name != null)
            {
                ComboCategories.Items.FindByValue(currentStartUp.Category.Name).Selected = true;
            }
        }

        private void TryToFindStartUp()
        {
            bool startUpIsNew = id != 0;

            if (startUpIsNew)
            {
                TryToAssignValuesInStartUp();
            }
        }

        private void TryToAssignValuesInStartUp()
        {
            currentStartUp = AppData.Context.StartUp.Find(id);

            if (currentStartUp != null)
            {
                CheckBoxDone.Visible = true;
                TBoxName.Text = currentStartUp.Name;
                TBoxDescription.Text = currentStartUp.Description;
                TBoxMaxMembers.Text = currentStartUp.MaxMembersCount.ToString();
            }
            else
            {
                string reason = HttpUtility.UrlEncode("Ошибка 404. Стартап не был найден." +
                    "Пожалуйста, попробуйте найти другой стартап.");

                Response.Redirect("~/Default?reason=" + reason);
            }
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
            bool isStartUpNew = id == 0;

            if (isStartUpNew)
            {
                CreateStartUp();
            }
            else
            {
                UpdateStartUp();
            }
        }

        private void UpdateStartUp()
        {
            currentStartUp.Name = TBoxName.Text;
            currentStartUp.Description = TBoxDescription.Text;
            currentStartUp.MaxMembersCount = int.Parse(TBoxMaxMembers.Text);
            currentStartUp.Category = AppData.Context.Category.First(c => c.Name.Equals(ComboCategories.SelectedValue));
            currentStartUp.IsDone = CheckBoxDone.Checked;

            AppData.Context.StartUp.AddOrUpdate(currentStartUp);

            try
            {
                AppData.Context.SaveChanges();

                string reason = HttpUtility.UrlEncode("Стартап успешно изменён!");

                Response.Redirect("~/Default?reason=" + reason, false);
            }
            catch (Exception ex)
            {
                string reason = HttpUtility.UrlEncode("Стартап не был изменен или добавлен." +
                    "Пожалуйста, попробуйте изменить стартап ещё раз");

                Response.Redirect("~/Default?reason=" + reason, false);
            }
        }

        /// <summary>
        /// Creates a new startup or modify existing.
        /// </summary>
        private void CreateStartUp()
        {
            currentStartUp = new StartUp
            {
                Name = TBoxName.Text,
                Description = TBoxDescription.Text,
                Category = AppData.Context.Category.First(c => c.Name.Equals(ComboCategories.SelectedValue)),
                CreationDate = DateTime.Now,
                IsDone = false,
                MaxMembersCount = int.Parse(TBoxMaxMembers.Text)
            };

            User user = AppData.Context.User.First(u => u.Login.ToLower().Equals(User.Identity.Name.ToLower()));
            RoleType role = AppData.Context.RoleType.First(r => r.Name.Equals("Организатор"));

            StartUpOfUser userStartUp = new StartUpOfUser
            {
                User = user,
                RoleType = role,
            };

            currentStartUp.StartUpOfUser.Add(userStartUp);

            AppData.Context.StartUp.Add(currentStartUp);

            try
            {
                AppData.Context.SaveChanges();

                int newStartupId = AppData.Context.StartUp.Where(s => s.CreationDate.Equals(currentStartUp.CreationDate)).First().Id;

                string reason = HttpUtility.UrlEncode("Стартап был успешно сохранён!");

                Response.Redirect("~/StartUpInfo.aspx?id=" + newStartupId + "$&reason=" + reason, false);
            }
            catch (Exception ex)
            {
                string reason = HttpUtility.UrlEncode("Стартап не был сохранен." +
                    "Ошибка: " + ex.Message + ".\n" +
                    "Пожалуйста, попробуйте добавить или изменить стартап ещё раз.");

                Response.Redirect("~/Default.aspx?reason" + reason);
            }
        }

        /// <summary>
        /// Cancels the creation or modification of the startup.
        /// </summary>
        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            string reason = HttpUtility.UrlEncode("Создание или удаление стартапа было отменено!");

            Response.Redirect("~/Default.aspx?reason=" + reason);
        }

        protected void LViewImages_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "RemoveImage")
            {
                currentStartUp.StartUpImage.Remove(AppData
                    .Context
                    .StartUpImage
                    .Find(int.Parse((string)e.CommandArgument)));

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
                    StartUp = currentStartUp
                };

                currentStartUp.StartUpImage.Add(newImage);
            });

            InsertImagesIntoStartUp();
        }

        protected void LViewDocuments_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "RemoveDocument")
            {
                currentStartUp.DocumentOfStartUp.Remove(AppData
                    .Context
                    .DocumentOfStartUp
                    .Find(int.Parse((string)e.CommandArgument)));

                InsertDocumentsIntoStartUp();
            }
        }

        private void InsertDocumentsIntoStartUp()
        {
            if (currentStartUp != null)
            {
                LViewDocuments.DataSource = currentStartUp.DocumentOfStartUp.ToList();
                LViewDocuments.DataBind();
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
                    StartUp = currentStartUp,
                    Blob = StreamToByteArray(blob),
                    IsPublic = true
                };

                currentStartUp.DocumentOfStartUp.Add(doc);
            });

            InsertDocumentsIntoStartUp();
        }

        private byte[] StreamToByteArray(Stream source)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                source.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}