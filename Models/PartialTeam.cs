using System;
using System.Drawing;

namespace StartUpWebAPI.Entities
{
    public partial class Team
    {
        public string ImagePreview
        {
            get
            {
                string result = "data:image/jpg;base64,";

                if (Image != null)
                {
                    result += Convert.ToBase64String(Image);
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