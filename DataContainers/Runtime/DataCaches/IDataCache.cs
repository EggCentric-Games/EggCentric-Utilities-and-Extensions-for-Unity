namespace EggCentric.Utilities.DataContainers
{
    public interface IDataCache<T>
    {
        public T Data { get; }
        public float Timestamp { get; }

        public bool IsValid { get; }
    }
}