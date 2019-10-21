using System;
using System.Threading.Tasks;
using System.Linq;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using NewsAppNative.Core.Models;
using NewsAppNative.Core.Extensions;
using NewsAppNative.Core.Models.Messages;
using MvvmCross.Plugin.Messenger;
using System.Collections.Generic;

namespace NewsAppNative.Core.ViewModels.News
{
    public class NewsViewModel : BaseViewModel
    {
        private MvxObservableCollection<NewsModel> _news = new MvxObservableCollection<NewsModel>();
        private MvxAsyncCommand _refreshNewsCommand;
        private MvxAsyncCommand _loadMoreNewsCommand;
        private MvxCommand<NewsModel> _addToFavoriteCommand;
        private MvxCommand<NewsModel> _removeNewsCommand;
        private MvxAsyncCommand _redrawCommand;
        private int _currentNewLoadedPage = 0;
        private bool _isBusy;
        private readonly MvxSubscriptionToken token;
        public MvxObservableCollection<NewsModel> News
        {
            get => _news;
            set => SetProperty(ref _news, value);
        }
        public MvxAsyncCommand RedrawCommand
        {
            get
            {
                _redrawCommand = _redrawCommand ?? new MvxAsyncCommand(Redraw);
                return _redrawCommand;
            }
        }

        private Task Redraw()
        {
            throw new NotImplementedException();
        }

        public MvxAsyncCommand RefreshNewsCommand
        {
            get
            {
                _refreshNewsCommand = _refreshNewsCommand ?? new MvxAsyncCommand(RefreshNewsAsync);
                return _refreshNewsCommand;
            }
        }
        private async Task RefreshNewsAsync()
        {
            try
            {
                if (!IsBusy)
                {
                    IsBusy = true;
                    List<NewsModel> newsList = await RepositoryService.GetNews(20, 1);
                    News = new MvxObservableCollection<NewsModel>(newsList);
                    _currentNewLoadedPage = 1;
                    IsBusy = false;         
                }
            }
            catch(Exception e)
            {
                DialogService.Alert("Ошибка", e.GetLastMessage());
            }
        }

        public MvxAsyncCommand LoadMoreNewsCommand
        {
            get
            {
                _loadMoreNewsCommand = _loadMoreNewsCommand ?? new MvxAsyncCommand(LoadMoreNewsAsync);
                return _loadMoreNewsCommand;
            }
        }
        private async Task LoadMoreNewsAsync()
        {
            try
            {
                if (!IsBusy)
                {
                    IsBusy = true;
                    _currentNewLoadedPage += 1;
                    List<NewsModel> newsList = await RepositoryService.GetNews(20, _currentNewLoadedPage);
                    News.AddRange(newsList);
                    IsBusy = false;
                }
            }
            catch (Exception e)
            {
                DialogService.Alert("Ошибка", e.GetLastMessage());
            }
        }
        public MvxCommand<NewsModel> AddToFavoriteCommand
        {
            get
            {
                _addToFavoriteCommand = _addToFavoriteCommand ?? new MvxCommand<NewsModel>(AddToFavorite);
                return _addToFavoriteCommand;
            }
        }
        private void AddToFavorite(NewsModel obj)
        {
            try
            {
                if (obj.IsInFavorite)
                {
                    obj.IsInFavorite = false;
                    var message = new RemoveItem(this, obj);
                    Messenger.Publish(message);
                }
                else
                {
                    obj.IsInFavorite = true;
                    var message = new AddToFavoriteMessage(this, obj);
                    Messenger.Publish(message);
                }
                RepositoryService.SaveOrUpdateNews(obj);
            }
            catch (Exception e)
            {
                DialogService.Alert("Ошибка", e.GetLastMessage());
            }
        }        
        public MvxCommand<NewsModel> RemoveNewsCommand
        {
            get
            {
                _removeNewsCommand = _removeNewsCommand ?? new MvxCommand<NewsModel>(RemoveNewsItem);
                return _removeNewsCommand;
            }
        }
        private void RemoveNewsItem(NewsModel obj)
        {
            try
            {
                News.Remove(obj);
                obj.IsMuted = true;
                obj.IsInFavorite = false;
                RepositoryService.SaveOrUpdateNews(obj);
                var message = new RemoveItem(this, obj);
                Messenger.Publish(message);
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
        public NewsViewModel()
        {
            RefreshNewsCommand.Execute();
            token = Messenger.Subscribe<RemoveFromFavoriteMessage>(RemoveFromFavorite);
        }

        private void RemoveFromFavorite(RemoveFromFavoriteMessage obj)
        {
            NewsModel changedNews = News.FirstOrDefault(n => n.Id == obj.News.Id);
            if (changedNews != null)
            {
                changedNews.IsInFavorite = false;
            }
        }
    }
}
