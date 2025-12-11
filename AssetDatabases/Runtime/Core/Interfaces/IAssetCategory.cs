namespace EggCentric.AssetDatabases
{
    public interface IAssetCategory<TAsset> : IRecursiveNode<IAssetCategory<TAsset>, TAsset> where TAsset : IAssetEntry
    {
        public string Name { get; }
    }
}