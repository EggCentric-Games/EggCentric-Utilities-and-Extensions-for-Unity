using EggCentric.Validation;
using System;

namespace EggCentric.ValueProviders.Validated
{
    public class ValidatedValue<T> : ITrackableValue<T>
    {
        public T Value
        {
            get => value;
            set => UpdateValue(value);
        }

        protected T value;

        private readonly Corrector<T> _corrector;

        public event Action<T> OnValueChanged;
        public event Action OnValueChangedNoArgs;

        public ValidatedValue(Corrector<T> validator, T value = default)
        {
            _corrector = validator;
            _corrector.Invoke(value, out this.value);
        }

        public static implicit operator T(ValidatedValue<T> obj) => obj.Value;

        protected void UpdateValue(T value)
        {
            if (Equals(this.value, value))
                return;

            _corrector.Invoke(value, out this.value);
            OnValueChanged?.Invoke(Value);
            OnValueChangedNoArgs?.Invoke();
        }
    }
}