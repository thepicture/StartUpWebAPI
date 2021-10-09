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
                    return NativeImageUtils.ConvertFromBytes(UserImage);
                }
                else
                {
                    return NativeImageUtils.ConvertFromBitmap(Properties.Resources.noPicture);
                }
            }
        }
    }
}