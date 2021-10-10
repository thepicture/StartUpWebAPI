using System.Drawing;
using System.IO;

namespace StartUpWebAPI.Models
{
    public class RawNativeImageUtils : NativeImageUtils
    {
        public static Image ConvertBytesToImage(byte[] input)
        {
            using (MemoryStream stream = new MemoryStream(input))
            {
                return Image.FromStream(stream);
            }
        }
    }
}