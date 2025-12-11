namespace EggCentric.AssetDatabases
{
    public interface IAssetLibrary<TAsset> : IAssetCategory<TAsset> where TAsset : IAssetEntry
    {
        public bool ResolveFor(int id, out TAsset result);
    }
}