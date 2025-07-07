namespace EggCentric.DataContainers
{
    public abstract class ManualDataCache<TValue> : IDataCache<TValue>
    {
        public abstract TValue Data { get; }
        public abstract bool IsValid { get; }

        public abstract void SetValue(TValue newValue);

        public static implicit operator TValue(ManualDataCache<TValue> obj)
        {
            return obj.Data;
        }
    }
}