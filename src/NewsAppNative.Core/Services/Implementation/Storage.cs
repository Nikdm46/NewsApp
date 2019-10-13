using System.Linq;
using System.Threading.Tasks;
using NewsAppNative.Core.Models;
using NewsAppNative.Core.Models.Storage;
using Realms;

namespace NewsAppNative.Core.Services.Implementation
{
    public class Storage : IStorageService
    {
        private Realm _storage;
        public Storage()
        {
            _storage = Realm.GetInstance();
        }

        public void SaveOrUpdateNews(NewsModel newsToSave)
        {
            _storage.Write(() =>
            {
                var objectToSave = new RealmNews()
                {
                    Id = newsToSave.Id,
                    Title = newsToSave.Title,
                    Content = newsToSave.Content,
                    CreatedAt = newsToSave.CreatedAt.ToString(),
                    IsInFavorite = newsToSave.IsInFavorite,
                    IsMuted = newsToSave.IsMuted,
                };
                _storage.Add(objectToSave, true);
            });
        }

        public void RemoveNews(NewsModel newsToRemove)
        {
            var news = _storage.All<RealmNews>().FirstOrDefault(n => n.Id == newsToRemove.Id);
            _storage.Write(() =>
            {
                _storage.Remove(news);
            });            
        }

        public async Task<IQueryable<T>> GetFromStorage<T>() where T : RealmObject
        {
            return _storage.All<T>();
        }
    }
}
