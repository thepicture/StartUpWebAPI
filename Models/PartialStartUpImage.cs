using StartUpWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace StartUpWebAPI.Entities
{
    public partial class StartUpImage
    {
        public string ImageInBase64
        {
            get
            {
                Image image = RawNativeImageUtils.ConvertBytesToImage(Image);
                Image thumbnailImage = ProportionalNativeImageUtils.ResizeImageProportionally(image, 300);

                return NativeImageUtils.ConvertFromBitmap(new Bitmap(thumbnailImage));
            }
        }
    }
}