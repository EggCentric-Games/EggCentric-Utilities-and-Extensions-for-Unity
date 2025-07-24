using System;

namespace EggCentric.DataContainers
{
    public interface IReadOnlyField<T>
    {
        public T Value { get; }

        public event Action<T> OnValueChanged;
        public event Action OnValueChangedNoArgs;
    }
}
