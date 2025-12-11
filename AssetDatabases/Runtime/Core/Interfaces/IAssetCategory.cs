using System.Collections.Generic;

namespace EggCentric.AssetDatabases
{
    public interface IAssetCategory<TAsset> where TAsset : IAssetEntry
    {
        public string Name { get; }
        public IReadOnlyCollection<TAsset> Entries { get; }
        public IReadOnlyCollection<IAssetCategory<TAsset>> SubCategories { get; }
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