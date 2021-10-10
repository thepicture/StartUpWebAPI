using StartUpWebAPI.Models;
using System.Drawing;

namespace StartUpWebAPI.Entities
{
    public partial class User
    {
        public string UserImageOrDefault
        {
            get
            {
                bool isUserHasImage = UserImage != null;

                if (isUserHasImage)
                {
                    Image image = RawNativeImageUtils.ConvertBytesToImage(UserImage);
                    Image thumbnailImage = ProportionalNativeImageUtils.ResizeImageProportionally(image, 300);

                    return NativeImageUtils.ConvertFromBitmap(new Bitmap(thumbnailImage));
                }
                else
                {
                    return NativeImageUtils.ConvertFromBitmap(Properties.Resources.noPicture);
                }
            }
        }

        public string UserImageInCommentOrDefault
        {
            get
            {
                bool isUserHasImage = UserImage != null;

                if (isUserHasImage)
                {
                    Image image = RawNativeImageUtils.ConvertBytesToImage(UserImage);
                    Image thumbnailImage = ProportionalNativeImageUtils.ResizeImageProportionally(image, 100);

                    return NativeImageUtils.ConvertFromBitmap(new Bitmap(thumbnailImage));
                }
                else
                {
                    return NativeImageUtils.ConvertFromBitmap(Properties.Resources.noPicture);
                }
            }
        }
    }
}