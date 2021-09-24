using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace StartUpWebAPI.Models
{
    public class NativeImageUtils
    {
        public static string ConvertFromBitmap(Bitmap image)
        {
            string result = "data:image/jpg;charset=utf-8;base64,";

            ImageConverter converter = new ImageConverter();
            result += Convert.ToBase64String(ConvertImageToBytes(image));

            return result;
        }

        public static Image ConvertFromStream(Stream source)
        {
            return Image.FromStream(source);
        }

        public static byte[] ConvertImageToBytes(Image source)
        {
            return (byte[])new ImageConverter().ConvertTo(source, typeof(byte[]));
        }
    }
}