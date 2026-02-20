namespace EggCentric.ValueProviders.DataContainers
{
    public interface IDataCache<T> : IReadOnlyDataCache<T>
    {
        public void Invalidate();
    }
}