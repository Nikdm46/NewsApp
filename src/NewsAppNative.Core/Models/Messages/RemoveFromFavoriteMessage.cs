using MvvmCross.Plugin.Messenger;

namespace NewsAppNative.Core.Models.Messages
{
    public class RemoveFromFavoriteMessage : MvxMessage
    {
        public NewsModel News
        {
            get;
            private set;
        }
        public RemoveFromFavoriteMessage(object sender, NewsModel news) : base(sender)
        {
            News = news;
        }
    }
}
