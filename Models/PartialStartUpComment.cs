using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace StartUpWebAPI.Entities
{
    public partial class StartUpComment
    {
        public string GetCommentImage
        {
            get
            {
                string result = "data:image/jpg;base64,";

                if (User.UserImage != null)
                {
                    result += Convert.ToBase64String(User.UserImage);
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