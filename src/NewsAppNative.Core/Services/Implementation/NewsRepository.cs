using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using MvvmCross;
using NewsAppNative.Core.Models;
using NewsAppNative.Core.Models.Storage;
using NewsAppNative.Core.Extensions;
using NewsAppNative.Core.Rest;
using System.Net.Http;
using System;
using MvvmCross.Logging;
using NewsAppNative.Core.DTO;

namespace NewsAppNative.Core.Services.Implementation
{
    public class NewsRepository : INewsRepositoryService
    {
        private const string BaseUrl = "http://frontappapi.dock7.66bit.ru/api/";
        private IStorageService storage => Mvx.IoCProvider.Resolve<IStorageService>();
        private IRestClient restClient => Mvx.IoCProvider.Resolve<IRestClient>();

        private readonly IMvxLog _mvxLog;

        public NewsRepository(IMvxLog mvxLog)
        {
            _mvxLog = mvxLog;
        }

        public async Task<List<NewsModel>> GetNews(int count, int page)
        {
            try
            {
                var url = BaseUrl + $"news/get?count={count}&page={page}";
                ResponseDTO<NewsModel> result = await restClient.MakeRequestAsync<NewsModel>(url, HttpMethod.Get);

                if(!result.IsSuccess)
                {
                    throw new Exception();
                }

                return await SortNewsAsync(result.Content);                
            }
            catch(Exception)
            {                
                throw new NetworkErrorException("Не удалсь обновить список новостей.");
            }
        }

        public async Task SaveOrUpdateNews(NewsModel newsToSave)
        {
            try
            { 
                await storage.SaveOrUpdate(newsToSave.ToRealmObject());
            }
            catch (Exception ex)
            {
                _mvxLog.ErrorException("Save news failed", ex);
            }
        }

        public async Task RemoveNews(NewsModel newsToRemove)
        {
            try
            {
                RealmNews realmNews = await GetNewsFromStorageByIdAsync(newsToRemove.Id);
                await storage.Remove(realmNews);
            }
            catch (Exception ex)
            {
                _mvxLog.ErrorException("Remove news from storage failed", ex);
            }
        }        

        public async Task<List<NewsModel>> GetNewsFromStorage()
        {
            try
            { 
                var savedNewsList = new List<NewsModel>();
                IQueryable<RealmNews> realmNews = await storage.GetAll<RealmNews>();
                foreach(RealmNews item in realmNews)
                {
                    savedNewsList.Add(item.ToModel());
                }
                return savedNewsList;
            }
            catch (Exception ex)
            {
                _mvxLog.ErrorException("Get news from storage failed", ex);
                throw new Exception(ex.GetLastMessage());
            }
        }

        private async Task<List<NewsModel>> SortNewsAsync(List<NewsModel> news)
        {
            List<NewsModel> savedNews = await GetNewsFromStorage();
            if (savedNews.Count > 0)
            {
                foreach (NewsModel item in savedNews)
                {
                    NewsModel selectedNewsItem = news.FirstOrDefault(i => i.Id == item.Id);
                    if (selectedNewsItem != null)
                    {
                        selectedNewsItem.IsInFavorite = item.IsInFavorite;
                        selectedNewsItem.IsMuted = item.IsMuted;
                    }
                }
            }
            return news.Where(n => n.IsMuted == false).ToList();
        }

        private async Task<RealmNews> GetNewsFromStorageByIdAsync(long id)
        {
            return await storage.GetById<RealmNews>(id);
        }
    }
}
