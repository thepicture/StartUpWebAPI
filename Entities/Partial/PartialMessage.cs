namespace StartUpWebAPI.Entities
{
    public partial class Message
    {
        public User Sender => User;
        public User Receiver => User1;
    }
}