namespace EggCentric.AssetDatabases
{
    public interface IAssetDatabase<TAsset> where TAsset : IAssetEntry
    {
        public IAssetCategory<TAsset> Root { get; }
    }
}