using UnityEngine;

namespace EggCentric.AssetDatabases
{
    public abstract class AssetDatabase<TAsset> : ScriptableObject, IAssetDatabase<TAsset> where TAsset : IAssetEntry
    {
        public IAssetCategory<TAsset> Root => _root;

        [SerializeField] private AssetCategory<TAsset> _root;
    }
}