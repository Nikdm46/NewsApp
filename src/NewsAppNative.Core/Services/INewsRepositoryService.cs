using System.Collections.Generic;
using System.Threading.Tasks;
using NewsAppNative.Core.Models;

namespace NewsAppNative.Core.Services
{
    public interface INewsRepositoryService
    {
        Task<List<NewsModel>> GetNews(int count, int page);
        Task SaveOrUpdateNews(NewsModel newsToSave);
        Task RemoveNews(NewsModel newsToRemove);
        Task<List<NewsModel>> GetNewsFromStorage();
    }
}
