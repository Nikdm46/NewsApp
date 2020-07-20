using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<IQueryable<T>> GetAll<T>() where T : RealmObject
        {
           return _storage.All<T>();
        }

        public async Task<T> GetById<T>(long? id) where T : RealmObject
        {
            var result = _storage.Find<T>(id);
            return result;
        }

        public async Task SaveOrUpdate<T>(IEnumerable<T> itemsToSave) where T : RealmObject
        {
            foreach (RealmObject obj in itemsToSave)
            {
                await SaveOrUpdate(obj);
            }
        }

        public async Task SaveOrUpdate<T>(T itemToSave) where T : RealmObject
        {
            _storage.Write(() =>
            {
                _storage.Add(itemToSave, true);
            });
        }

        public async Task Remove<T>(T itemToRemove) where T : RealmObject
        {
            _storage.Write(() =>
            {
                _storage.Remove(itemToRemove);
            });            
        }
    }
}
