using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace StartUpWebAPI.Models
{
    public class NativeImageUtils
    {
        public static string ConvertFromBytes(Bitmap image)
        {
            string result = "data:image/jpg;base64,";

            ImageConverter converter = new ImageConverter();
            result += Convert.ToBase64String((byte[])converter.ConvertTo(image, typeof(byte[])));

            return result;
        }
    }
}