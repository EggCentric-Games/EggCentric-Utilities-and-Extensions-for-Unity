using System.Collections.Generic;

namespace EggCentric.AssetDatabases
{
    public interface IRecursiveNode<TCategory, TAsset> where TCategory : IRecursiveNode<TCategory, TAsset>
    {
        public IReadOnlyCollection<TAsset> Entries { get; }
        public IReadOnlyCollection<TCategory> SubCategories { get; }
        public int TotalEntryCount => GetTotalCount();

        private int GetTotalCount()
        {
            int totalEntryCount = Entries.Count;
            foreach (var subCategory in SubCategories)
                totalEntryCount += subCategory.TotalEntryCount;

            return totalEntryCount;
        }
    }
}