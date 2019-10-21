using System.Linq;
using CoreGraphics;
using Foundation;
using MvvmCross.Platforms.Ios.Binding.Views;
using NewsAppNative.Core.ViewModels.News;
using NewsAppNative.iOS.Views.Cells;
using UIKit;

namespace NewsAppNative.iOS.Views.Source
{
    public class NewsTableViewSource : MvxTableViewSource
    {
        NewsViewModel ViewModel { get; set; }
        UITableView TableView { get; set; }
        public NewsTableViewSource(NewsViewModel viewModel, UITableView tableView) : base(tableView)
        {
            tableView.RegisterClassForCellReuse(typeof(NewsCell), new NSString("NewsCell"));
            ViewModel = viewModel;
            TableView = tableView;
        }

        public override UISwipeActionsConfiguration GetLeadingSwipeActionsConfiguration(UITableView tableView, NSIndexPath indexPath)
        {
            var toFavoriteAction = ContextualToFavoriteAction(indexPath.Row);
            
            var leadingSwipe = UISwipeActionsConfiguration.FromActions(new UIContextualAction[] { toFavoriteAction });

            leadingSwipe.PerformsFirstActionWithFullSwipe = true;

            return leadingSwipe;
        }

        public override UISwipeActionsConfiguration GetTrailingSwipeActionsConfiguration(UITableView tableView, NSIndexPath indexPath)
        {
            var deleteAction = ContextualDeleteAction(indexPath.Row);

            var TrailingSwipe = UISwipeActionsConfiguration.FromActions(new UIContextualAction[] { deleteAction });

            TrailingSwipe.PerformsFirstActionWithFullSwipe = true;

            return TrailingSwipe;
        }
        public override void WillDisplay(UITableView tableView, UITableViewCell cell, NSIndexPath indexPath)
        {

            if (indexPath.Row == ViewModel.News.Count - 1)
            {
                ViewModel.LoadMoreNewsCommand.ExecuteAsync();
            }
        }

        public UIContextualAction ContextualToFavoriteAction(int row)
        {
            var news = ViewModel.News[row];
            var action = UIContextualAction.FromContextualActionStyle
                            (UIContextualActionStyle.Normal,
                                "",
                                (toFavorieAction, view, success) =>
                                {
                                    var item = ViewModel.News[row];
                                    ViewModel.AddToFavoriteCommand.Execute(item);
                                    success(true);
                                });
            if(news.IsInFavorite)
            {
                action.Image = UIImage.FromBundle("baseline_favorite_border_white_24");
            }
            else
            {
                action.Image = UIImage.FromBundle("baseline_favorite_white_24");
            }
            action.BackgroundColor = UIColor.Blue;

            return action;
        }

        public UIContextualAction ContextualDeleteAction(int row)
        {
            var action = UIContextualAction.FromContextualActionStyle
                            (UIContextualActionStyle.Destructive,
                                "",
                                (deleteAction, view, success) =>
                                {
                                    var item = ViewModel.News[row];
                                    ViewModel.RemoveNewsCommand.Execute(item);
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
