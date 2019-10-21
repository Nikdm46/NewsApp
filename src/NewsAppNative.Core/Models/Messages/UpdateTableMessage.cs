using MvvmCross.Plugin.Messenger;

namespace NewsAppNative.Core.Models.Messages
{
    public class UpdateTableMessage : MvxMessage
    {
        public NewsModel News
        {
            get;
            private set;
        }
        public UpdateTableMessage(object sender, NewsModel news) : base(sender)
        {
            News = news;
        }
    }
}
