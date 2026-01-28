namespace EggCentric.AssetDatabases
{
    public interface IDatabaseResolver<TAsset> where TAsset : IAssetEntry
    {
        public bool ResolveFor(int id, out TAsset result);
    }
}