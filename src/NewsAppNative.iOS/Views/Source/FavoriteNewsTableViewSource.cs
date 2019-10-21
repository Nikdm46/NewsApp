using CoreGraphics;
using Foundation;
using MvvmCross.Platforms.Ios.Binding.Views;
using NewsAppNative.Core.ViewModels.FavoriteNews;
using NewsAppNative.iOS.Views.Cells;
using UIKit;

namespace NewsAppNative.iOS.Views.Source
{
    class FavoriteNewsTableViewSource : MvxTableViewSource
    {
        FavoriteNewsViewModel ViewModel { get; set; }
        UITableView TableView { get; set; }
        public FavoriteNewsTableViewSource(FavoriteNewsViewModel viewModel, UITableView tableView) : base(tableView)
        {
            tableView.RegisterClassForCellReuse(typeof(NewsCell), new NSString("NewsCell"));
            ViewModel = viewModel;
            TableView = tableView;
        }

        public override UISwipeActionsConfiguration GetTrailingSwipeActionsConfiguration(UITableView tableView, NSIndexPath indexPath)
        {
            var deleteAction = ContextualDeleteFromFavoriteAction(indexPath.Row);

            var TrailingSwipe = UISwipeActionsConfiguration.FromActions(new UIContextualAction[] { deleteAction });

            TrailingSwipe.PerformsFirstActionWithFullSwipe = true;

            return TrailingSwipe;
        }

        public UIContextualAction ContextualDeleteFromFavoriteAction(int row)
        {
            var action = UIContextualAction.FromContextualActionStyle
                            (UIContextualActionStyle.Destructive,
                                "",
                                (deleteAction, view, success) =>
                                {
                                    var item = ViewModel.FavoriteNews[row];
                                    ViewModel.RemoveFromFavoriteCommand.Execute(item);
                                    success(true);
                                });

            action.Image = UIImage.FromBundle("baseline_delete_white_24");
            action.BackgroundColor = UIColor.Red;

            return action;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            base.RowSelected(tableView, indexPath);
            tableView.DeselectRow(indexPath, true);
        }

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            var cell = tableView.DequeueReusableCell("NewsCell", indexPath) as NewsCell;
            cell.Transform = CGAffineTransform.MakeScale(1, -1);
            return cell;
        }
    }
}
