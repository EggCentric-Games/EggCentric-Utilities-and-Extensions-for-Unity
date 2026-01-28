namespace EggCentric.DataContainers
{
    public interface IDataCache<T> : IReadOnlyDataCache<T>
    {
        public void Invalidate();
    }
}