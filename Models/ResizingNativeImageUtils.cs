using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace StartUpWebAPI.Models
{
    public class ResizingNativeImageUtils : NativeImageUtils
    {
        public static string CropImageAndGiveItAsBase64String(byte[] source, int newWidth, int newHeight)
        {
            using (MemoryStream stream = new MemoryStream(source))
            {
                Image image = Image.FromStream(stream);
                image = image.GetThumbnailImage(newWidth, newHeight, null, IntPtr.Zero);

                var bitmap = new Bitmap(image);
                var result = ConvertFromBitmap(bitmap);

                return result;
            }
        }
    }
}