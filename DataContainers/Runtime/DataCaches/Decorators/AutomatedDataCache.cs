using System;


namespace EggCentric.DataContainers
{
    public class AutomatedDataCache<TValue> : IDataCache<TValue>
    {
        public TValue Data => GetValue();
        public bool IsValid => cache.IsValid;

        protected ManualDataCache<TValue> cache;
        protected Func<TValue> valueGetter;
        
        private readonly object _recalculationLock = new object();

        public AutomatedDataCache(ManualDataCache<TValue> cache, Func<TValue> valueGetter)
        {
            this.cache = cache;
            this.valueGetter = valueGetter;
        }

        public static implicit operator TValue(AutomatedDataCache<TValue> obj)
        {
            return obj.Data;
        }

        public TValue GetValue()
        {
            if (cache.IsValid)
                return cache;

            lock (_recalculationLock)
            {
                TValue newValue = valueGetter();
                cache.SetValue(newValue);

                return cache;
            }
        }
    }
}