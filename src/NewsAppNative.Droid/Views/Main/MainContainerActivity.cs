using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using NewsAppNative.Core.ViewModels.Main;
using NewsAppNative.Droid.Views.FavoriteNews;
using NewsAppNative.Droid.Views.News;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace NewsAppNative.Droid.Views.Main
{
    [Activity(Theme = "@style/AppTheme", WindowSoftInputMode = SoftInput.AdjustResize | SoftInput.StateHidden)]
    public class MainContainerActivity : BaseActivity<MainContainerViewModel>
    {
        private NewsFragment _newsFragment;
        private FavoriteNewsFragment _favoriteNewsFragment;
        private BottomNavigationView _bottomNavigation;
        private TextView _toolbarTitle;
        protected override int ActivityLayoutId => Resource.Layout.activity_main_container;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            _bottomNavigation = FindViewById<BottomNavigationView>(Resource.Id.bottom_navigation);
            _bottomNavigation.NavigationItemSelected += BottomNavigation_NavigationItemSelected;

            var toolbarView = FindViewById(Resource.Id.layout_toolbar);
            _toolbarTitle = toolbarView.FindViewById<TextView>(Resource.Id.textview_toolbar_title);

            _newsFragment = new NewsFragment { Arguments = new Bundle() };
            _favoriteNewsFragment = new FavoriteNewsFragment { Arguments = new Bundle() };

            LoadFragment(Resource.Id.action_news);
        }
        private void BottomNavigation_NavigationItemSelected(object sender, BottomNavigationView.NavigationItemSelectedEventArgs e)
        {
            LoadFragment(e.Item.ItemId);
        }
        void LoadFragment(int id)
        {
            Android.Support.V4.App.Fragment fragment = null;
            switch (id)
            {
                case Resource.Id.action_news:
                    fragment = _newsFragment;
                    _toolbarTitle.Text = "Новости";
                    break;
                case Resource.Id.action_favorites:
                    fragment = _favoriteNewsFragment;
                    _toolbarTitle.Text = "Избранное";
                    break;
            }

            if (fragment == null)
                return;

            SupportFragmentManager.BeginTransaction()
                .Replace(Resource.Id.content_frame, fragment)
                .Commit();
        }

        public override void OnBackPressed()
        {

        }
    }
}
