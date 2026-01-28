using UnityEngine;

namespace EggCentric.AssetDatabases
{
    public abstract class AssetDatabase<TAsset> : ScriptableObject, IAssetDatabase<TAsset> where TAsset : IAssetEntry
    {
        public IAssetCategory<TAsset> Root => _root;

        [SerializeField] private AssetCategory<TAsset> _root;
        private DatabaseResolver<TAsset> _resolver;

        private void OnEnable() => _resolver = new DatabaseResolver<TAsset>(this);

        public bool ResolveFor(int assetId, out TAsset result) => _resolver.ResolveFor(assetId, out result);
    }
}