using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace StartUpWebAPI.Models
{
    /// <summary>
    /// Utils for converting the image to many representations.
    /// </summary>
    public class NativeImageUtils
    {
        /// <summary>
        /// Converts the bitmap image to the string base64 representation.
        /// </summary>
        /// <param name="image">The bitmap image.</param>
        /// <returns>The base64 string.</returns>
        public static string ConvertFromBitmap(Bitmap image)
        {
            string result = "data:image/jpg;charset=utf-8;base64,";

            result += Convert.ToBase64String(ConvertImageToBytes(image));

            return result;
        }

        /// <summary>
        /// Converts the image from stream to Image.
        /// </summary>
        /// <param name="source">The given stream source.</param>
        /// <returns>The image in Image type.</returns>
        public static Image ConvertFromStream(Stream source)
        {
            return Image.FromStream(source);
        }

        /// <summary>
        /// Converts the image from Image to byte array.
        /// </summary>
        /// <param name="source">The image source.</param>
        /// <returns>A byte array of the image.</returns>
        public static byte[] ConvertImageToBytes(Image source)
        {
            return (byte[])new ImageConverter().ConvertTo(source, typeof(byte[]));
        }

        /// <summary>
        /// Converts the image from byte array to base64 string representation.
        /// </summary>
        /// <param name="source">The source of a byte array</param>
        /// <returns>The base64 string representation</returns>
        public static string ConvertFromBytes(byte[] source)
        {
            string result = "data:image/jpg;base64,";

            result += Convert.ToBase64String(source);

            return result;
        }
    }
}