using System.Collections.Generic;
using System.Threading.Tasks;
using NewsAppNative.Core.Models;

namespace NewsAppNative.Core.Services
{
    public interface IRepositoryService
    {
        Task<List<NewsModel>> GetNews(int count, int page);
        void SaveOrUpdateNews(NewsModel newsToSave);
        void RemoveNews(NewsModel newsToRemove);
        Task<List<NewsModel>> GetNewsFromStorage();
    }
}
