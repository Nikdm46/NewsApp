using MvvmCross.AdvancedRecyclerView.Data.ItemUniqueIdProvider;
using NewsAppNative.Core.Models;

namespace NewsAppNative.Droid.RecyclerViews
{
    public class UniqueIdProvider : IMvxItemUniqueIdProvider
    {
        public long GetUniqueId(object fromObject)
        {
            return (fromObject as NewsModel).Id;
        }
    }
}
