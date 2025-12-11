using System.Collections.Generic;
using UnityEngine;

namespace EggCentric.AssetDatabases
{
    public abstract class AssetDatabase<TAsset> : ScriptableObject, IAssetLibrary<TAsset> where TAsset : IAssetEntry
    {
        public string Name => _name;
        public IReadOnlyCollection<TAsset> Entries => _entries;
        public IReadOnlyCollection<IAssetCategory<TAsset>> SubCategories => _subcategories;
        public int TotalEntryCount => ((IAssetCategory<TAsset>)this).TotalEntryCount;

        [SerializeField] private string _name;
        [SerializeField] private TAsset[] _entries;
        [SerializeField] private AssetCategory<TAsset>[] _subcategories;

        private TAsset[] _lookupTable;

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

        private void OnEnable() => GenerateCollection();

        private void OnValidate() => GenerateCollection();

        private void GenerateCollection()
        {
            _lookupTable = new TAsset[TotalEntryCount];
            int startAssetId = 0;
            AddCategoryEntries(this, ref startAssetId);
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