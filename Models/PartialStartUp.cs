using System;
using System.Drawing;
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
                else
                {
                    ImageConverter converter = new ImageConverter();
                    Bitmap noPicture = Properties.Resources.noPicture;
                    result += Convert.ToBase64String((byte[])converter.ConvertTo(noPicture, typeof(byte[])));
                }

                return result;
            }
        }
    }
}