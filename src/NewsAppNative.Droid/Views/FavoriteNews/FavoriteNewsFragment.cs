using Android.OS;
using Android.Support.V4.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Com.H6ah4i.Android.Widget.Advrecyclerview.Swipeable;
using MvvmCross.AdvancedRecyclerView;
using MvvmCross.AdvancedRecyclerView.Adapters.NonExpandable;
using MvvmCross.AdvancedRecyclerView.ViewHolders;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using NewsAppNative.Core.ViewModels.FavoriteNews;

namespace NewsAppNative.Droid.Views.FavoriteNews
{
    public class FavoriteNewsFragment : MvxFragment<FavoriteNewsViewModel>
    {
        public View _view;
        private MvxNonExpandableAdapter _mAdapter;
        private MvxAdvancedNonExpandableRecyclerView _mRecyclerView;
        private LinearLayoutManager _mLayoutManager;

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);            
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            _view = this.BindingInflate(Resource.Layout.fragment_favorite_news, container, false);
            _mRecyclerView = _view.FindViewById<MvxAdvancedNonExpandableRecyclerView>(Resource.Id.favorite_RecyclerView);
            
            _mLayoutManager = new LinearLayoutManager(_view.Context, LinearLayoutManager.Vertical, reverseLayout: true)
            {
                ReverseLayout = true,          
            };
            _mRecyclerView.SetLayoutManager(_mLayoutManager);
            var dividerItemDecoration = new DividerItemDecoration(_view.Context, _mLayoutManager.Orientation);
            dividerItemDecoration.SetDrawable(ContextCompat.GetDrawable(_view.Context, Resource.Drawable.recycler_view_divider));
            _mRecyclerView.AddItemDecoration(dividerItemDecoration);

            _mAdapter = _mRecyclerView.AdvancedRecyclerViewAdapter as MvxNonExpandableAdapter;
            _mAdapter.SwipeItemPinnedStateController.ForLeftSwipe().Pinned += RemoveItem;            

            _mAdapter.MvxViewHolderBound += (args) =>
            {
                var swipeHolder = args.Holder as MvxAdvancedRecyclerViewHolder;
                var swipeState = swipeHolder.SwipeStateFlags;

                swipeHolder.ProportionalSwipeAmountModeEnabled = true;

                swipeHolder.MaxLeftSwipeAmount = -0.5f;

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
                }

                if (bgRes != 0)
                    args.ViewHolder.ItemView.SetBackgroundResource(bgRes);
            };

            return _view;
        }
        private void RemoveItem(object obj)
        {
            ViewModel.RemoveFromFavoriteCommand.Execute(obj);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            _mAdapter.SwipeItemPinnedStateController.ForLeftSwipe().Pinned -= RemoveItem;
        }
    }
}
