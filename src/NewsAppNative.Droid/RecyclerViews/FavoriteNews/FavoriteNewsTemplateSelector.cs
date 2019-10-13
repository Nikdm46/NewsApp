using MvvmCross.Droid.Support.V7.RecyclerView.ItemTemplates;

namespace NewsAppNative.Droid.RecyclerViews.FavoriteNews
{
    class FavoriteNewsTemplateSelector : MvxDefaultTemplateSelector
    {
        public FavoriteNewsTemplateSelector() : base(Resource.Layout.favorite_news_item)
        {
        }
    }
}
