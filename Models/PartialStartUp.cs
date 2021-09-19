using System;
using System.Linq;

namespace StartUpWebAPI.Entities
{
    /// <summary>
    /// Public partial class for the StartUp entity.
    /// </summary>
    public partial class StartUp
    {
        public string ImagePreview
        {
            get
            {
                StartUpImage imagePreview = StartUpImage.ToList().FirstOrDefault();
                string result = "data:image/jpg;base64,";

                if (imagePreview != null)
                {
                    result += Convert.ToBase64String(imagePreview.Image);
                }

                return result;
            }
        }
    }
}