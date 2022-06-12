using StartUpWebAPI.Entities;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StartUpWebAPI
{
    public partial class StartUpDocuments : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (StartUpBaseEntities entities = new StartUpBaseEntities())
            {
                int startUpId = int.Parse(Request.QueryString["startUpId"]);
                LViewDocuments.DataSource = entities.StartUp
                    .Find(startUpId).DocumentOfStartUp
                    .ToList();
                LViewDocuments.DataBind();
            }
        }

        protected void BtnDownloadDocument_Click(object sender, EventArgs e)
        {
            int documentId = int.Parse(((Button)sender).CommandArgument);
            using (StartUpBaseEntities entities = new StartUpBaseEntities())
            {
                DocumentOfStartUp document = entities.DocumentOfStartUp.Find(documentId);
                DownloadFile(document);
            }
        }

        private void DownloadFile(DocumentOfStartUp document)
        {
            try
            {
                SetResponseHeaders(document);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot download file: " + ex);
            }
        }

        private void SetResponseHeaders(DocumentOfStartUp document)
        {
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + document.FileName);
            Response.OutputStream.Write(document.Blob, 0, document.Blob.Length);
            Response.Flush();
        }
    }
}