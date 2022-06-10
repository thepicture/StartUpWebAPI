using StartUpWebAPI.Models;
using System.Drawing;
using System.Linq;
using System.Web;

namespace StartUpWebAPI.Entities
{
    public partial class User
    {

        public User Self => this;
        public string UserImageOrDefault
        {
            get
            {
                bool isUserHasImage = UserImage != null;

                if (isUserHasImage)
                {
                    Image image = RawNativeImageUtils.ConvertBytesToImage(UserImage);
                    Image thumbnailImage = ProportionalNativeImageUtils.ResizeImageProportionally(image, 400);

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

        public Message LastMessage
        {
            get
            {
                if (Message1 != null && Message != null && Message.Union(Message1).Count() > 0)
                {
                    return Message
                        .Union(Message1)
                        .OrderBy(m => m.Id)
                        .Last();
                }
                else
                {
                    return new Message { Text = "Сообщений нет" };
                }
            }
        }

        public bool IsLastMessageMine
        {
            get
            {
                if (LastMessage == null || LastMessage.Id == 0)
                {
                    return false;
                }
                else
                {
                    return LastMessage.Sender.Login == HttpContext.Current.User.Identity.Name;
                }
            }
        }
    }
}