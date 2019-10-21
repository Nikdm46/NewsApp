using System;
using CoreGraphics;
using Foundation;
using MvvmCross;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.Plugin.Messenger;
using NewsAppNative.Core.Models.Messages;
using NewsAppNative.Core.ViewModels.News;
using NewsAppNative.iOS.Helpers;
using NewsAppNative.iOS.Views.Source;
using UIKit;

namespace NewsAppNative.iOS.Views.News
{
    [MvxTabPresentation(TabName = "Новости", TabAccessibilityIdentifier = "NewsController", TabIconName = "outline_list_alt_white_24")]
    public class NewsController : BaseViewController<NewsViewModel>
    {
        private UITableView _tableView;
        private MvxUIRefreshControl _mvxRefresh;
        private NewsTableViewSource _newsTableViewSource;
        private MvxSubscriptionToken _token;

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            if (_token == null)
            {
                _token = Mvx.IoCProvider.Resolve<IMvxMessenger>().SubscribeOnMainThread<UpdateTableMessage>(UpdateTable);
            }
        }

        private void UpdateTable(UpdateTableMessage obj)
        {
            var itemIndex = ViewModel.News.IndexOf(obj.News);
            _tableView.BeginUpdates();
            _tableView.ReloadRows(new NSIndexPath[] { NSIndexPath.FromRowSection(itemIndex, 0) }, UITableViewRowAnimation.Automatic);
            _tableView.EndUpdates();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            NavigationController.NavigationBar.Hidden = false;
            Title = "Новости";

            _tableView = new UITableView()
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
            };

            _tableView.Transform = CGAffineTransform.MakeScale(1,-1);
            _newsTableViewSource = new NewsTableViewSource(ViewModel, _tableView);
            _mvxRefresh = new MvxUIRefreshControl();
            _tableView.AddSubview(_mvxRefresh);
            _tableView.Source = _newsTableViewSource;
            _tableView.RowHeight = UITableView.AutomaticDimension;
            _tableView.EstimatedRowHeight = 60f;
            _tableView.ReloadData();

            

            View.AddSubview(_tableView);

            var set = this.CreateBindingSet<NewsController, NewsViewModel>();
            set.Bind(_newsTableViewSource).To(vm => vm.News);
            set.Bind(_mvxRefresh).For(rf => rf.RefreshCommand).To(vm => vm.RefreshNewsCommand);
            set.Bind(_mvxRefresh).For(rf => rf.IsRefreshing).To(vm => vm.IsBusy);
            set.Apply();
        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();
            _tableView.WidthEqualTo(View, View);
            _tableView.HeightEqualTo(View, View);
            _tableView.TopEqualTo(View, View);
            _tableView.BottomEqualTo(View, View);
            _tableView.LeftEqualTo(View, View);
            _tableView.RightEqualTo(View, View);
        }
    }
}
