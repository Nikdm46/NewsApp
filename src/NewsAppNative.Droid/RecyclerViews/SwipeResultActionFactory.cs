using Com.H6ah4i.Android.Widget.Advrecyclerview.Swipeable.Action;
using MvvmCross.AdvancedRecyclerView.Swipe;
using MvvmCross.AdvancedRecyclerView.Swipe.ResultActions;
using MvvmCross.AdvancedRecyclerView.Swipe.ResultActions.ItemManager;


namespace NewsAppNative.Droid.RecyclerViews.News
{
    public class SwipeResultActionFactory : MvxSwipeResultActionFactory
    {
        public override SwipeResultAction GetSwipeLeftResultAction(IMvxSwipeResultActionItemManager itemProvider)
        {
            var item = itemProvider.GetItem();
            var pinnedStateController = itemProvider.GetAttachedPinnedStateControllerProviderWithItem();
            if (pinnedStateController.IsPinnedForAnyState(item))
            {
                return new MvxSwipeUnpinResultAction(itemProvider);
            }
            else
            {
                return new MvxSwipeToDirectionResultAction(itemProvider, SwipeDirection.FromLeft);
            }
        }

        public override SwipeResultAction GetSwipeRightResultAction(IMvxSwipeResultActionItemManager itemProvider)
        {
            var item = itemProvider.GetItem();
            var pinnedStateController = itemProvider.GetAttachedPinnedStateControllerProviderWithItem();
            if (pinnedStateController.IsPinnedForAnyState(item))
            {
                return new MvxSwipeUnpinResultAction(itemProvider);
            }
            else
            {
                return new MvxSwipeToDirectionResultAction(itemProvider, SwipeDirection.FromRight);
            }
        }

        public override SwipeResultAction GetUnpinSwipeResultAction(IMvxSwipeResultActionItemManager itemProvider)
        {
            return new MvxSwipeUnpinResultAction(itemProvider);
        }        
    }
}
