using System;

namespace EggCentric.DataContainers
{
    public class Field<T>
    {
        public T Value
        {
            get => value;
            set => UpdateValue(value);
        }

        public event Action<T> OnValueChanged;
        public event Action OnValueChangedNoArgs;

        protected T value;

        public Field(T value = default)
        {
            this.value = value;
        }

        protected void UpdateValue(T value)
        {
            if (Equals(this.value, value))
                return;

            this.value = value;
            OnValueChanged?.Invoke(value);
            OnValueChangedNoArgs?.Invoke();
        }
    }
}