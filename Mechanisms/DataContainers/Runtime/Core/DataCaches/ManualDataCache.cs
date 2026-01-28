using System;

namespace EggCentric.DataContainers
{
    public abstract class ManualDataCache<TValue> : IDataCache<TValue>
    {
        public abstract TValue Value { get; }
        public abstract bool IsValid { get; }

        public event Action<TValue> OnValueChanged;
        public event Action OnValueChangedNoArgs;
        public event Action OnCacheInvalidated;

        public virtual void SetValue(TValue newValue)
        {
            if (Value.Equals(newValue))
                return;

            OnValueChanged?.Invoke(newValue);
            OnValueChangedNoArgs?.Invoke();
        }

        public virtual void Invalidate() => OnCacheInvalidated?.Invoke();

        public static implicit operator TValue(ManualDataCache<TValue> obj) => obj.Value;
    }
}