using System;
using Foundation;
using MvvmCross;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.ViewModels;
using NewsAppNative.Core.ViewModels.FavoriteNews;
using NewsAppNative.Core.ViewModels.Main;
using NewsAppNative.Core.ViewModels.News;
using UIKit;

namespace NewsAppNative.iOS.Views.Main
{
    [Register("MainViewController")]
   // [MvxRootPresentation()]
    public class MainViewController : MvxTabBarViewController<MainViewModel>
    {
        private bool _isPresentedFirstTime = true;
        private bool _constructed;
        public MainViewController()
        {
            _constructed = true;
            ViewDidLoad();
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            NavigationController?.SetNavigationBarHidden(true, false);
            TabBarItem.SetTitleTextAttributes(new UITextAttributes
            {
                TextColor = UIColor.DarkGray,
                Font = UIFont.SystemFontOfSize(16, UIFontWeight.Regular)
            }, UIControlState.Normal);

            //TabBar.SelectedImageTintColor = Colors.SelectedTabColor;
            //TabBar.TintColor = Colors.SelectedTabColor;

            if (ViewModel != null && _isPresentedFirstTime)
            {
                _isPresentedFirstTime = false;
                ViewModel.ShowInitialViewModelsCommand.ExecuteAsync(null);
            }

            if (NavigationController?.NavigationBar != null)
                NavigationController.NavigationBar.Hidden = true;

            if (NavigationController != null)
            {
                NavigationController.InteractivePopGestureRecognizer.Enabled = false;
                TabBar.Translucent = false;
            }
        }
        protected override void SetTitleAndTabBarItem(UIViewController viewController, MvxTabPresentationAttribute attribute)
        {
            // you can override this method to set title or iconName
            if (!string.IsNullOrEmpty(attribute.TabName))
                attribute.TabName = attribute.TabName;

            base.SetTitleAndTabBarItem(viewController, attribute);
        }
    }
}
