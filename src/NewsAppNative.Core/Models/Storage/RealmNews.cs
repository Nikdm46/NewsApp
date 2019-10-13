using System;
using System.Collections.Generic;
using System.Text;
using Realms;

namespace NewsAppNative.Core.Models.Storage
{
    public class RealmNews : RealmObject
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string CreatedAt { get; set; }
        public bool IsMuted { get; set; }
        public bool IsInFavorite { get; set; }
        public bool IsExpanded { get; set; }
    }
}
