using System;
using System.ComponentModel;
using Android.OS;
using Android.Support.V4.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Com.H6ah4i.Android.Widget.Advrecyclerview.Swipeable;
using Com.Orangegangsters.Github.Swipyrefreshlayout.Library;
using MvvmCross.AdvancedRecyclerView;
using MvvmCross.AdvancedRecyclerView.Adapters.NonExpandable;
using MvvmCross.AdvancedRecyclerView.ViewHolders;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using NewsAppNative.Core.ViewModels.News;
using NewsAppNative.Droid.RecyclerViews;

namespace NewsAppNative.Droid.Views.News
{
    public class NewsFragment : MvxFragment<NewsViewModel>
    {
        public View _view;
        private MvxNonExpandableAdapter _mAdapter;
        private MvxAdvancedNonExpandableRecyclerView _mRecyclerView;
        private LinearLayoutManager _mLayoutManager;
        private SwipyRefreshLayout _mSwipyRefreshLayout;

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            _view = this.BindingInflate(Resource.Layout.fragment_news, container, false);

            _mRecyclerView = _view.FindViewById<MvxAdvancedNonExpandableRecyclerView>(Resource.Id.RecyclerView);

            _mSwipyRefreshLayout = _view.FindViewById<SwipyRefreshLayout>(Resource.Id.swipe_refresh);
            _mSwipyRefreshLayout.Refresh += RefreshNewsList;

            _mLayoutManager = new LinearLayoutManager(_view.Context, LinearLayoutManager.Vertical, reverseLayout: true);
            _mRecyclerView.SetLayoutManager(_mLayoutManager);

            var onScrollListener = new RecyclerViewOnScrollListener(_mLayoutManager);
            onScrollListener.LoadMoreEvent += (object sender, EventArgs e) =>
            {
                ViewModel.LoadMoreNewsCommand.Execute();
            };

            _mRecyclerView.AddOnScrollListener(onScrollListener);

            var dividerItemDecoration = new DividerItemDecoration(_view.Context, _mLayoutManager.Orientation);
            dividerItemDecoration.SetDrawable(ContextCompat.GetDrawable(_view.Context, Resource.Drawable.recycler_view_divider));
            _mRecyclerView.AddItemDecoration(dividerItemDecoration);

            _mAdapter = _mRecyclerView.AdvancedRecyclerViewAdapter as MvxNonExpandableAdapter;
            _mAdapter.SwipeItemPinnedStateController.ForLeftSwipe().Pinned += RemoveItem;
            _mAdapter.SwipeItemPinnedStateController.ForRightSwipe().Pinned += AddToFavorite;

            _mAdapter.MvxViewHolderBound += (args) =>
            {
                var swipeHolder = args.Holder as MvxAdvancedRecyclerViewHolder;
                var swipeState = swipeHolder.SwipeStateFlags;

                swipeHolder.ProportionalSwipeAmountModeEnabled = true;                

                swipeHolder.MaxLeftSwipeAmount = -0.5f;
                swipeHolder.MaxRightSwipeAmount = 0.5f;

                _mAdapter.SwipeItemPinnedStateController.SetPinnedForAllStates(args.DataContext, false);

                swipeHolder.SwipeItemHorizontalSlideAmount = _mAdapter.SwipeItemPinnedStateController.ForRightSwipe().IsPinned(args.DataContext) ? -0.5f : 0;
            };

            _mAdapter.SwipeBackgroundSet += (args) =>
            {
                int bgRes = 0;
                switch (args.Type)
                {
                    case SwipeableItemConstants.DrawableSwipeNeutralBackground:
                        bgRes = Resource.Drawable.bg_swipe_item_neutral;
                        break;
                    case SwipeableItemConstants.DrawableSwipeLeftBackground:
                        bgRes = Resource.Drawable.bg_item_swiping_right_state;
                        break;
                    case SwipeableItemConstants.DrawableSwipeRightBackground:
                        bgRes = Resource.Drawable.bg_item_swiping_left_state;
                        break;
                }

                if (bgRes != 0)
                    args.ViewHolder.ItemView.SetBackgroundResource(bgRes);
            };

            ViewModel.PropertyChanged += OnModelPropertyChanged;

            return _view;
        }

        private void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewModel.IsBusy))
            {
                if (!ViewModel.IsBusy)
                {
                    _mSwipyRefreshLayout.Refreshing = false;
                }
            }
        }

        private void RefreshNewsList(object sender, SwipyRefreshLayout.RefreshEventArgs e)
        {
            var swipeDirection = e.P0;
            if (swipeDirection == SwipyRefreshLayoutDirection.Bottom)
            {
                ViewModel.RefreshNewsCommand.Execute();
            }
        }

        private void AddToFavorite(object obj)
        {
            ViewModel.AddToFavoriteCommand.Execute(obj);       
        }

        private void RemoveItem(object obj)
        {
            ViewModel.RemoveNewsCommand.Execute(obj);
        }
        public override void OnDestroy()
        {
            base.OnDestroy();
            ViewModel.PropertyChanged -= OnModelPropertyChanged;
            _mAdapter.SwipeItemPinnedStateController.ForLeftSwipe().Pinned -= RemoveItem;
            _mAdapter.SwipeItemPinnedStateController.ForRightSwipe().Pinned -= AddToFavorite;
        }
    }
}
