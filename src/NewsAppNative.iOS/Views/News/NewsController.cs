using CoreGraphics;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
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
