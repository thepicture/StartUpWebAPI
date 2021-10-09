using StartUpWebAPI.Models;

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
                    return ResizingNativeImageUtils.CropImageAndGiveItAsBase64String(UserImage, 200, 200);
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
                    return ResizingNativeImageUtils.CropImageAndGiveItAsBase64String(UserImage, 100, 100);
                }
                else
                {
                    return NativeImageUtils.ConvertFromBitmap(Properties.Resources.noPicture);
                }
            }
        }
    }
}