using System;
using NewsAppNative.Core.Models;
using NewsAppNative.Core.Models.Storage;

namespace NewsAppNative.Core.Extensions
{
    public static class ModelExtensions
    {
        public static RealmNews ToRealmObject(this NewsModel model)
        {
            if (model != null)
            {
                return new RealmNews()
                {
                    Id = model.Id,
                    Title = model.Title,
                    Content = model.Content,
                    CreatedAt = model.CreatedAt.ToString(),
                    IsInFavorite = model.IsInFavorite,
                    IsMuted = model.IsMuted,
                };
            }
            else
            {
                return new RealmNews();
            }
        }

        public static NewsModel ToModel(this RealmNews realmObject)
        {
            if (realmObject != null)
            {
                return new NewsModel()
                {
                    Id = realmObject.Id,
                    Title = realmObject.Title,
                    Content = realmObject.Content,
                    CreatedAt = DateTime.Parse(realmObject.CreatedAt),
                    IsInFavorite = realmObject.IsInFavorite,
                    IsMuted = realmObject.IsMuted,
                };
            }
            else
            {
                return new NewsModel();
            }
        }
    }
}
