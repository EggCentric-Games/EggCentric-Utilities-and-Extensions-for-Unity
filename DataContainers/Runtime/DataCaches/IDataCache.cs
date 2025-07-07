namespace EggCentric.DataContainers
{
    public interface IDataCache<T>
    {
        public T Data { get; }
        public bool IsValid { get; }
    }
}