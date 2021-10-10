using System.Drawing;

namespace StartUpWebAPI.Models
{
    public class ProportionalNativeImageUtils : NativeImageUtils
    {
        /// <summary>
        /// Rezises the image with proportion saving. Height is computed dynamically.
        /// </summary>
        public static Image ResizeImageProportionally(Image image, int width)
        {
            int x0 = width;
            double factorOfY = image.Height / (image.Width * 1.0);
            int y0 = (int)(width * factorOfY);

            Image result = ResizingNativeImageUtils
                .ResizeImage(image, new Size(x0, y0));

            return result;
        }
    }
}