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
    public partial class AddStartUp : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (PageIsLoadedForTheFirstTime())
            {
                GetWebFormThingsDone();
            }
        }

        private void GetWebFormThingsDone()
        {
            bool isNoIdPresented = !Request.RawUrl.Contains("id=");

            CheckIfNoIdIsPresented(isNoIdPresented);

            DoPrepareActions();

            string idString = Request.QueryString.Get("id");

            if (IdDoesNotExist(idString))
            {
                CheckIfStartUpIsNewAndPrepareIt(idString);
            }
        }

        private static bool IdDoesNotExist(string idString)
        {
            return idString != null;
        }

        private void CheckIfNoIdIsPresented(bool noIdIsPresented)
        {
            if (noIdIsPresented)
            {
                Response.Redirect(Request.RawUrl + "?id=0");
            }
        }

        private void DoPrepareActions()
        {
            PrepareViewState();

            if (!IsPostBack)
            {
                LoadBgImage();
                InsertCategoriesBox();
                InsertRegionsBox();
            }
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

        private void CheckIfStartUpIsNewAndPrepareIt(string idString)
        {
            int id = int.Parse(idString);

            bool isStartUpExists = id != 0;

            if (isStartUpExists)
            {
                CreateContextForStartUpAndPrepareStartUp(id);
            }
        }

        private void CreateContextForStartUpAndPrepareStartUp(int id)
        {
            using (StartUpBaseEntities context = new StartUpBaseEntities())
            {
                DoActionsForPreparingStartUp(id, context);
            }
        }

        private void DoActionsForPreparingStartUp(int id, StartUpBaseEntities context)
        {
            StartUp nullableStartUp = context.StartUp.Find(id);
            TryToPrepareStartUp(nullableStartUp);
        }

        private void TryToPrepareStartUp(StartUp nullableStartUp)
        {
            if (HasStartUp(nullableStartUp))
            {
                PrepareStartUp(nullableStartUp);
            }
        }

        private static bool HasStartUp(StartUp nullableStartUp)
        {
            return nullableStartUp != null;
        }

        private void PrepareStartUp(StartUp nullableStartUp)
        {
            ViewState["currentStartUp"] = nullableStartUp;

            if (!IsPostBack)
            {
                FillStartUpAttributes(nullableStartUp);
            }

            InsertObjectsInStartUp();
        }

        private void FillStartUpAttributes(StartUp nullableStartUp)
        {
            TBoxName.Text = ((StartUp)ViewState["currentStartUp"]).Name;
            TBoxDescription.Text = ((StartUp)ViewState["currentStartUp"]).Description;
            TBoxMaxMembers.Text = ((StartUp)ViewState["currentStartUp"]).MaxMembersCount.ToString();
            CheckBoxDone.Checked = ((StartUp)ViewState["currentStartUp"]).IsDone;
            ((List<StartUpImage>)ViewState["images"]).AddRange(nullableStartUp.StartUpImage.ToList());
            ((List<DocumentOfStartUp>)ViewState["documents"]).AddRange(nullableStartUp.DocumentOfStartUp.ToList());
        }

        private void InsertObjectsInStartUp()
        {
            InsertCategory();
            InsertRegion();
            InsertDocumentsIntoStartUp();
            InsertImagesIntoStartUp();
        }

        private void InsertRegion()
        {
            DropDownRegions.Items.FindByValue(((StartUp)ViewState["currentStartUp"]).Region.Name).Selected = true;
        }

        private void PrepareViewState()
        {
            ViewState["images"] = new List<StartUpImage>();
            ViewState["documents"] = new List<DocumentOfStartUp>();
            ViewState["currentStartUp"] = new StartUp();
        }

        private bool PageIsLoadedForTheFirstTime()
        {
            return !Page.IsPostBack;
        }

        /// <summary>
        /// Loads background image.
        /// </summary>
        private void LoadBgImage()
        {
            BgImage.ImageUrl = NativeImageUtils.ConvertFromBitmap(Properties.Resources.commonBg);
        }

        /// <summary>
        /// Inserts the attached documents into the startup.
        /// </summary>
        private void InsertDocumentsIntoStartUp()
        {
            LViewDocuments.DataSource = ((List<DocumentOfStartUp>)ViewState["documents"]).Where(i => i.Id >= 0).ToList();
            LViewDocuments.DataBind();
        }

        /// <summary>
        /// Insert the attached images into the startup.
        /// </summary>
        private void InsertImagesIntoStartUp()
        {
            LViewImages.DataSource = ((List<StartUpImage>)ViewState["images"]).Where(i => i.Id >= 0).ToList();
            LViewImages.DataBind();
        }

        /// <summary>
        /// Inserts a category type into the startup.
        /// </summary>
        private void InsertCategory()
        {
            ComboCategories.Items.FindByValue(((StartUp)ViewState["currentStartUp"]).Category.Name).Selected = true;
        }


        /// <summary>
        /// Insert category names into categories box.
        /// </summary>
        private void InsertCategoriesBox()
        {
            using (StartUpBaseEntities context = new StartUpBaseEntities())
            {
                var categories = context.Category.Select(c => c.Name).ToList();
                ComboCategories.DataSource = categories;
                ComboCategories.DataBind();
            }
        }

        /// <summary>
        /// Actions for saving the new or modified startup.
        /// </summary>
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            UpdateStartUp();
        }

        /// <summary>
        /// Updates the startup.
        /// </summary>
        private void UpdateStartUp()
        {
            string errors = "";
            errors = CheckIfNameIsBad(errors);
            errors = CheckIsMemberCountIsBad(errors);

            bool hasAnyErrors = errors.Length > 0;

            if (hasAnyErrors)
            {
                Response.Redirect("~/AddStartUp?id="
                    + ((StartUp)ViewState["currentStartUp"]).Id
                    + "&reason=" + HttpUtility.UrlEncode(errors));
                return;
            }

            using (StartUpBaseEntities context = new StartUpBaseEntities())
            {
                ((StartUp)ViewState["currentStartUp"]).Name = TBoxName.Text;
                ((StartUp)ViewState["currentStartUp"]).Description = TBoxDescription.Text;
                ((StartUp)ViewState["currentStartUp"]).MaxMembersCount = int.Parse(TBoxMaxMembers.Text);

                ((StartUp)ViewState["currentStartUp"]).CategoryId = context
                    .Category
                    .First(c => c.Name.Equals(ComboCategories.SelectedValue))
                    .Id;

                ((StartUp)ViewState["currentStartUp"]).RegionId = context
                    .Region
                    .First(c => c.Name.Equals(DropDownRegions.SelectedValue))
                    .Id;

                ((StartUp)ViewState["currentStartUp"]).IsDone = CheckBoxDone.Checked;

                bool startUpIsNew = ((StartUp)ViewState["currentStartUp"]).Id == 0;

                if (startUpIsNew)
                {
                    ((StartUp)ViewState["currentStartUp"]).CreationDate = DateTime.Now;
                    ((StartUp)ViewState["currentStartUp"]).StartUpOfUser.Add(new StartUpOfUser
                    {
                        User = context.User.First(u => u.Login.Equals(User.Identity.Name)),
                        RoleType = context.RoleType.First(r => r.Name.Equals("Организатор"))
                    });

                    context.StartUp.Add((StartUp)ViewState["currentStartUp"]);
                }
                else
                {
                    int startUpId = ((StartUp)ViewState["currentStartUp"]).Id;
                    StartUp updatingStartUp = context.StartUp.Find(startUpId);

                    StartUp insertingStartUp = (StartUp)ViewState["currentStartUp"];
                    context.Entry(updatingStartUp).CurrentValues.SetValues(insertingStartUp);
                }

                StartUp addedStartUp = null;
                int id;

                try
                {
                    context.SaveChanges();
                    id = ((StartUp)ViewState["currentStartUp"]).Id;
                    addedStartUp = context.StartUp.First(s => s.Id == id);

                    string reason = HttpUtility.UrlEncode("Стартап успешно изменён!");

                    Response.Redirect("~/StartUpInfo?id=" + ((StartUp)ViewState["currentStartUp"]).Id + "&reason=" + reason, false);
                }
                catch (Exception)
                {
                    string reason = HttpUtility.UrlEncode("Стартап не был изменен или добавлен. " +
                        "Пожалуйста, попробуйте изменить стартап ещё раз. ");

                    Response.Redirect("~/StartUpInfo?id=" + ((StartUp)ViewState["currentStartUp"]).Id + "&reason=" + reason);
                }

                foreach (var image in (List<StartUpImage>)ViewState["images"])
                {
                    image.StartUpId = addedStartUp.Id;

                    bool anyImages = !(image.Id < 0 || addedStartUp.StartUpImage.Any(i => i.Name.Equals(image.Name)));

                    bool anyImagesInDeleteState = image.Id < 0 && addedStartUp.StartUpImage.Any(i => i.Name.Equals(image.Name));

                    if (anyImages)
                    {
                        context.StartUpImage.Add(image);
                    }
                    else if (anyImagesInDeleteState)
                    {
                        context.StartUpImage.Remove(context.StartUpImage.First(i => i.Name.Equals(image.Name)));
                        addedStartUp.StartUpImage.Remove(context.StartUpImage.First(i => i.Name.Equals(image.Name)));
                    }

                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception)
                    {
                        Response
                            .Redirect(Request.RawUrl + "&reason=" + HttpUtility.UrlEncode(
                            "Не удалось добавить изображения в стартап. " +
                            "Попробуйте ещё раз"));
                    }
                }

                foreach (var doc in (List<DocumentOfStartUp>)ViewState["documents"])
                {
                    doc.StartUpId = addedStartUp.Id;

                    bool anyDocs = doc.Id >= 0 && !addedStartUp.DocumentOfStartUp.Any(i => i.FileName.Equals(doc.FileName));
                    bool anyDocsInDeleteState = doc.Id < 0 && context.DocumentOfStartUp.Any(i => i.FileName.Equals(doc.FileName));

                    if (anyDocs)
                    {
                        context.DocumentOfStartUp.Add(doc);
                    }
                    else if (anyDocsInDeleteState)
                    {
                        context.DocumentOfStartUp.Remove(context.DocumentOfStartUp.First(i => i.FileName.Equals(doc.FileName)));
                        addedStartUp.DocumentOfStartUp.Remove(context.DocumentOfStartUp.First(i => i.FileName.Equals(doc.FileName)));
                    }

                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception)
                    {
                        Response
                             .Redirect(Request.RawUrl + "&reason=" + HttpUtility.UrlEncode(
                             "Не удалось добавить документы в стартап. " +
                             "Попробуйте ещё раз"));
                        return;
                    }
                }

                // Will uncomment if it does not crash the web form.
                context.ChangeTracker.Entries().ToList().ForEach(s => s.Reload());
            }

            RemoveSession();
        }

        private string CheckIsMemberCountIsBad(string errors)
        {
            bool badMembersCountFormat = int.TryParse(TBoxMaxMembers.Text, out _);

            if (badMembersCountFormat)
            {
                bool badMembersCountNumber = int.Parse(TBoxMaxMembers.Text) < 0
                                || string.IsNullOrWhiteSpace(TBoxMaxMembers.Text) || TBoxMaxMembers.Text.Length > 4;
                if (badMembersCountNumber)
                {
                    errors += "Количество участников - положительное число длиной от 1 до 4 цифр; \n";
                }
            }
            else
            {
                errors += "Количество участников должно быть положительным числом, а не буквенным представлением; \n";
            }

            return errors;
        }

        private string CheckIfNameIsBad(string errors)
        {
            bool badName = string.IsNullOrWhiteSpace(TBoxName.Text);

            if (badName)
            {
                errors += "Имя не должно быть пустым; \n";
            }

            return errors;
        }

        /// <summary>
        /// Disposes the current session.
        /// </summary>
        private void RemoveSession()
        {
            ViewState["currentStartUp"] = null;
        }

        /// <summary>
        /// Cancels editing/creating of the startup.
        /// </summary>
        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            string reason = HttpUtility.UrlEncode("Создание или удаление стартапа было отменено!");

            RemoveSession();

            Response.Redirect("~/StartUpInfo.aspx?id=" + ((StartUp)ViewState["currentStartUp"]).Id + "&reason=" + reason);
        }

        /// <summary>
        /// Pre-actions for removing images.
        /// </summary>
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

        /// <summary>
        /// Pre-actions for adding images.
        /// </summary>
        protected void BtnAddImages_Click(object sender, EventArgs e)
        {
            bool imageIsEmpty = FileUploadImages.PostedFiles[0].ContentLength == 0;
            if (imageIsEmpty)
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

        /// <summary>
        /// Pre-actions for removing the attached documents.
        /// </summary>
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

        /// <summary>
        /// Pre-actions for adding documents to the startup.
        /// </summary>
        protected void BtnAddDocuments_Click(object sender, EventArgs e)
        {
            bool documentIsEmpty = DocumentUpload.PostedFiles[0].ContentLength == 0;
            if (documentIsEmpty)
            {
                return;
            }

            List<HttpPostedFile> docs = DocumentUpload.PostedFiles.ToList();

            docs.ForEach(p =>
            {
                bool contentIsGreaterThanFiveMbOrExists = p.ContentLength > 1024 * 1024 * 25
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

            Session["reservedReason"] = "Документы успешно изменены. Вирусы не обнаружены";
            InsertDocumentsIntoStartUp();
        }
    }
}