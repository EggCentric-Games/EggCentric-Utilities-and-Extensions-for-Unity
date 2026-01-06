using System;

namespace EggCentric.DataContainers
{
    public interface ITrackableValue<T> : IValueProvider<T>
    {
        public event Action<T> OnValueChanged;
        public event Action OnValueChangedNoArgs;
    }
}