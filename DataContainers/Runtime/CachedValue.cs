using System;

namespace EggCentric.DataContainers
{
    public class CachedValue<T>
    {
        private Func<T> _valueGetter;

        private bool _hasValue;
        private T _value;

        public CachedValue(Func<T> valueGetter)
        {
            _valueGetter = valueGetter;
        }

        public CachedValue(Func<T> valueGetter, T initialValue)
        {
            _valueGetter = valueGetter;

            SetValue(initialValue);
        }

        public static implicit operator T(CachedValue<T> obj)
        {
            return obj.GetValue();
        }

        public T GetValue()
        {
            if (_hasValue)
                return _value;

            _value = _valueGetter();
            return _value;
        }

        public void SetValue(T value)
        {
            _value = value;
            _hasValue = true;
        }

        public void Clear()
        {
            _hasValue = false;
            _value = default;
        }
    }
}