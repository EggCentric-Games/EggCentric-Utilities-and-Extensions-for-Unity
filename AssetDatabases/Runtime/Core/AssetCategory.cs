using System.Collections.Generic;
using UnityEngine;

namespace EggCentric.AssetDatabases
{
    [System.Serializable]
    public class AssetCategory<TAsset> : IAssetCategory<TAsset> where TAsset : IAssetEntry
    {
        public string Name => _name;

        public IReadOnlyCollection<TAsset> Entries => _entries;
        public IReadOnlyCollection<IAssetCategory<TAsset>> SubCategories => _subcategories;

        [SerializeField] private string _name;
        [SerializeField] private TAsset[] _entries;
        [SerializeField] private AssetCategory<TAsset>[] _subcategories;
    }
}