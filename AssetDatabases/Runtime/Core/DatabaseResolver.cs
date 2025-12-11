using UnityEngine;

namespace EggCentric.AssetDatabases
{
    public abstract class DatabaseResolver<TAsset> : IDatabaseResolver<TAsset> where TAsset : IAssetEntry
    {
        private readonly AssetDatabase<TAsset> _assetDatabase;
        private TAsset[] _lookupTable;

        public DatabaseResolver(AssetDatabase<TAsset> assetDatabase) => _assetDatabase = assetDatabase;

        public bool ResolveFor(int id, out TAsset result)
        {
            result = default;
            if (_lookupTable == null)
                return false;

            if (id < 0 || id >= _lookupTable.Length)
                return false;

            result = _lookupTable[id];
            return true;
        }

        public void GenerateCollection()
        {
            _lookupTable = new TAsset[_assetDatabase.Root.TotalEntryCount];
            int startAssetId = 0;
            AddCategoryEntries(_assetDatabase.Root, ref startAssetId);
        }

        private void AddCategoryEntries(IAssetCategory<TAsset> category, ref int assetId)
        {
            if (category == null)
            {
                Debug.LogError($"Null category found");
                return;
            }

            foreach (var entry in category.Entries)
                AddEntry(entry, ref assetId);

            foreach (var subCategory in category.SubCategories)
                AddCategoryEntries(subCategory, ref assetId);
        }

        private void AddEntry(TAsset entry, ref int assetId)
        {
            if (entry == null)
                Debug.LogWarning($"Null entry found");

            _lookupTable[assetId] = entry;
            assetId++;
        }
    }
}