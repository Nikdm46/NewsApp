using System;
using System.Linq;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using NewsAppNative.Core.Extensions;
using NewsAppNative.Core.Models;
using NewsAppNative.Core.Models.Messages;

namespace NewsAppNative.Core.ViewModels.FavoriteNews
{
    public class FavoriteNewsViewModel : BaseViewModel
    {
        private MvxObservableCollection<NewsModel> _favoriteNews = new MvxObservableCollection<NewsModel>();
        private MvxAsyncCommand<NewsModel> _removeFromFavoriteCommand;
        private MvxAsyncCommand _refreshFavoriteNewsCommand;
        private bool _isBusy;
        private readonly MvxSubscriptionToken addToken;
        private readonly MvxSubscriptionToken removeToken;
        public MvxObservableCollection<NewsModel> FavoriteNews
        {
            get => _favoriteNews;
            set => SetProperty(ref _favoriteNews, value);
        }
        public MvxAsyncCommand<NewsModel> RemoveFromFavoriteCommand
        {
            get
            {
                _removeFromFavoriteCommand = _removeFromFavoriteCommand ?? new MvxAsyncCommand<NewsModel>(RemoveFromFavorite);
                return _removeFromFavoriteCommand;
            }
        }
        private async Task RemoveFromFavorite(NewsModel obj)
        {
            try
            {
                FavoriteNews.Remove(obj);
                obj.IsInFavorite = false;
                await RepositoryService.RemoveNews(obj);
                var message = new RemoveFromFavoriteMessage(this, obj);
                Messenger.Publish(message);
            }            
            catch(Exception e)
            {
                DialogService.Alert("Ошибка", e.GetLastMessage());
            }
        }
        public MvxAsyncCommand RefreshFavoriteNewsCommand
        {
            get
            {
                _refreshFavoriteNewsCommand = _refreshFavoriteNewsCommand ?? new MvxAsyncCommand(RefreshFavoriteNewsAsync);
                return _refreshFavoriteNewsCommand;
            }
        }
        private async Task RefreshFavoriteNewsAsync()
        {
            try
            { 
                if (!IsBusy)
                {
                    IsBusy = true;
                    var newsList = await RepositoryService.GetNewsFromStorage();
                    var sortedNewsList = newsList.Where(x => x.IsMuted != true && x.IsInFavorite == true);
                    FavoriteNews = new MvxObservableCollection<NewsModel>(sortedNewsList);
                    IsBusy = false;
                }
            }
            catch (Exception e)
            {
                DialogService.Alert("Ошибка", e.GetLastMessage());
            }
        }
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }
        public FavoriteNewsViewModel()
        {
            addToken = Messenger.Subscribe<AddToFavoriteMessage>(AddToFavorite);
            removeToken = Messenger.Subscribe<RemoveItem>(Remove);
            RefreshFavoriteNewsCommand.Execute();
        }
        private void Remove(RemoveItem obj)
        {
            NewsModel item = FavoriteNews.FirstOrDefault(i => i.Id == obj.News.Id);
            if (item != null)
            {
                FavoriteNews.Remove(item);
            }
        }
        private void AddToFavorite(AddToFavoriteMessage obj)
        {
            FavoriteNews.Add(new NewsModel()
            {
                Id = obj.News.Id,
                Title = obj.News.Title,
                Content = obj.News.Content,
                CreatedAt = obj.News.CreatedAt,
                IsInFavorite = obj.News.IsInFavorite,
            });
        }
    }
}
