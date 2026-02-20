using System;

namespace EggCentric.ValueProviders.DataContainers
{
    public class AutomatedDataCache<TValue> : IDataCache<TValue>
    {
        public TValue Value => GetValue();
        public bool IsValid => cache.IsValid;

        protected ManualDataCache<TValue> cache;
        protected Func<TValue> valueGetter;
        
        private readonly object _recalculationLock = new object();

        public event Action<TValue> OnValueChanged;
        public event Action OnValueChangedNoArgs;
        public event Action OnCacheInvalidated;

        public AutomatedDataCache(ManualDataCache<TValue> cache, Func<TValue> valueGetter)
        {
            this.cache = cache;
            this.valueGetter = valueGetter;

            cache.OnValueChanged += value => OnValueChanged?.Invoke(value);
            cache.OnValueChangedNoArgs += () => OnValueChangedNoArgs?.Invoke();
            cache.OnCacheInvalidated += () => OnCacheInvalidated?.Invoke();
        }

        public static implicit operator TValue(AutomatedDataCache<TValue> obj) => obj.Value;

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

        public void Invalidate() => cache.Invalidate();
    }
}