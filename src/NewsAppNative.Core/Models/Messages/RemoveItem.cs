using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.Plugin.Messenger;

namespace NewsAppNative.Core.Models.Messages
{
    public class RemoveItem : MvxMessage
    {
        public NewsModel News
        {
            get;
            private set;
        }
        public RemoveItem(object sender, NewsModel news) : base(sender)
        {
            News = news;
        }
    }
}
