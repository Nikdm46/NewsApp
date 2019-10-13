using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using MvvmCross;
using NewsAppNative.Core.Models;
using NewsAppNative.Core.Models.Storage;
using NewsAppNative.Core.PlatformSpecific;
using System;

namespace NewsAppNative.Core.Services.Implementation
{
    public class Repository : IRepositoryService
    {
        private const string Address = "http://frontappapi.dock7.66bit.ru/api/";
        private IHttpClient httpClient => Mvx.IoCProvider.Resolve<IHttpClient>();
        private IStorageService storage => Mvx.IoCProvider.Resolve<IStorageService>();

        public async Task<List<NewsModel>> GetNews(int count, int page)
        {
            var result = new List<NewsModel>();
            result = await httpClient.GetAsync<List<NewsModel>>(Address + $"news/get?count={count}&page={page}");
            List<NewsModel> savedNews = await GetNewsFromStorage();
            if(savedNews.Count > 0)
            {
                foreach(NewsModel item in savedNews)
                {
                    var selectedNewsItem = result.FirstOrDefault(i => i.Id == item.Id);
                    if (selectedNewsItem != null)
                    {
                        selectedNewsItem.IsInFavorite = item.IsInFavorite;
                        selectedNewsItem.IsMuted = item.IsMuted;
                    }
                }
            }
            return result.Where(n=>n.IsMuted == false).ToList();
        }

        public void SaveOrUpdateNews(NewsModel newsToSave)
        {
            storage.SaveOrUpdateNews(newsToSave);
        }
        public void RemoveNews(NewsModel newsToRemove)
        {
            storage.RemoveNews(newsToRemove);
        }
        public async Task<List<NewsModel>> GetNewsFromStorage()
        {
            var savedNewsList = new List<NewsModel>();
            IQueryable<RealmNews> realmNews = await storage.GetFromStorage<RealmNews>();
            foreach(RealmNews item in realmNews)
            {
                savedNewsList.Add(new NewsModel()
                {
                    Id = item.Id,
                    Title = item.Title,
                    CreatedAt = DateTime.Parse(item.CreatedAt),
                    Content = item.Content,
                    IsInFavorite = item.IsInFavorite,
                    IsMuted = item.IsMuted,
                });
            }
            return savedNewsList;
        }
    }
}
