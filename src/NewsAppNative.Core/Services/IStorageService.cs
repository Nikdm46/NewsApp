using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Realms;

namespace NewsAppNative.Core.Services
{
    public interface IStorageService
    {
        Task<IQueryable<T>> GetAll<T>() where T : RealmObject;
        Task<T> GetById<T>(long? id) where T : RealmObject;
        Task SaveOrUpdate<T>(IEnumerable<T> itemsToSave) where T : RealmObject;
        Task SaveOrUpdate<T>(T itemToSave) where T : RealmObject;
        Task Remove<T>(T itemToRemove) where T : RealmObject;
    }
}
