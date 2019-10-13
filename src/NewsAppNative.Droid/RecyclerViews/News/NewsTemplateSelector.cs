using MvvmCross.Droid.Support.V7.RecyclerView.ItemTemplates;

namespace NewsAppNative.Droid.RecyclerViews.News
{
    public class NewsTemplateSelector : MvxDefaultTemplateSelector
    {
        public NewsTemplateSelector() : base(Resource.Layout.news_item)
        {
        }
    }
}
