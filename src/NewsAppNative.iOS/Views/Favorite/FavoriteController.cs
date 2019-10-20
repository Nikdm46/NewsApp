using MvvmCross.Platforms.Ios.Presenters.Attributes;
using NewsAppNative.Core.ViewModels.FavoriteNews;

namespace NewsAppNative.iOS.Views.Favorite
{
    [MvxTabPresentation(TabName = "Избранное", TabAccessibilityIdentifier = "FavoriteNewsController", TabIconName = "round_turned_in_not_white_24")]
    public class FavoriteController : BaseViewController<FavoriteNewsViewModel>
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            NavigationController.NavigationBar.Hidden = false;
            Title = "Избранное";
        }
        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();
        }
    }
}
