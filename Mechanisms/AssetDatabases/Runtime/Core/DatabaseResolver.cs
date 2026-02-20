using UnityEngine;
using EggCentric.ValueProviders.DataContainers;


namespace EggCentric.AssetDatabases
{
    public class DatabaseResolver<TAsset> : IDatabaseResolver<TAsset> where TAsset : IAssetEntry
    {
        private readonly IAssetDatabase<TAsset> _assetDatabase;
        private IDataCache<TAsset[]> _lookupTable;

        public DatabaseResolver(IAssetDatabase<TAsset> assetDatabase)
        {
            _assetDatabase = assetDatabase;

            _lookupTable = new AutomatedDataCache<TAsset[]>(new PersistentDataCache<TAsset[]>(), GenerateCollection);
        }

        public bool ResolveFor(int id, out TAsset result)
        {
            result = default;
            if (_lookupTable == null)
                return false;

            if (id < 0 || id >= _lookupTable.Value.Length)
                return false;

            result = _lookupTable.Value[id];
            return true;
        }

        public TAsset[] GenerateCollection()
        {
            var lookupTable = new TAsset[_assetDatabase.Root.TotalEntryCount];
            int startAssetId = 0;
            AddCategoryEntries(lookupTable, _assetDatabase.Root, ref startAssetId);

            return lookupTable;
        }

        private void AddCategoryEntries(TAsset[] lookupTable, IAssetCategory<TAsset> category, ref int assetId)
        {
            if (category == null)
            {
                Debug.LogError($"Null category found");
                return;
            }

            foreach (var entry in category.Entries)
                AddEntry(lookupTable, entry, ref assetId);

            foreach (var subCategory in category.SubCategories)
                AddCategoryEntries(lookupTable, subCategory, ref assetId);
        }

        private void AddEntry(TAsset[] lookupTable, TAsset entry, ref int assetId)
        {
            if (entry == null)
                Debug.LogWarning($"Null entry found");
            lookupTable[assetId] = entry;
            assetId++;
        }
    }
}