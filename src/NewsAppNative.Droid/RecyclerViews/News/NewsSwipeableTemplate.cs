using Com.H6ah4i.Android.Widget.Advrecyclerview.Swipeable;
using MvvmCross.AdvancedRecyclerView.Swipe.ResultActions;
using MvvmCross.AdvancedRecyclerView.TemplateSelectors;

namespace NewsAppNative.Droid.RecyclerViews.News
{
    public class NewsSwipeableTemplate : MvxSwipeableTemplate
    {
        public override int SwipeContainerViewGroupId => Resource.Id.container_layout;

        public override int UnderSwipeContainerViewGroupId => Resource.Id.under_layout;

        public override int SwipeReactionType => SwipeableItemConstants.ReactionCanSwipeBothH;
        
        public override float MaxLeftSwipeAmount => -0.5f;

        public override float MaxRightSwipeAmount => 0.5f;

        public override MvxSwipeResultActionFactory SwipeResultActionFactory => new SwipeResultActionFactory();
    }
}
