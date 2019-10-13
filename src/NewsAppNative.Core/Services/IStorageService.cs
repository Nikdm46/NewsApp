using System.Linq;
using System.Threading.Tasks;
using NewsAppNative.Core.Models;
using Realms;

namespace NewsAppNative.Core.Services
{
    public interface IStorageService
    {
        void SaveOrUpdateNews(NewsModel newsToSave);
        void RemoveNews(NewsModel newsToRemove);
        Task<IQueryable<T>> GetFromStorage<T>() where T : RealmObject;
    }
}
