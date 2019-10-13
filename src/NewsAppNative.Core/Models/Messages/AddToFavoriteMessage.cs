using MvvmCross.Plugin.Messenger;

namespace NewsAppNative.Core.Models.Messages
{
    public class AddToFavoriteMessage : MvxMessage
    {
        public NewsModel News
        {
            get;
            private set;
        }
        public AddToFavoriteMessage(object sender, NewsModel news) : base(sender)
        {
            News = news;
        }
    }
}
