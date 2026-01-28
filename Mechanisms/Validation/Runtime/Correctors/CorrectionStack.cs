using System.Collections.Generic;

namespace EggCentric.Validation
{
    public abstract class CorrectionStack<T> : ICorrectionStack<T>
    {
        public IReadOnlyList<ICorrector<T>> Items => _validators;

        private readonly List<ICorrector<T>> _validators;

        public CorrectionStack() : this(new List<ICorrector<T>>()) { }
        public CorrectionStack(List<ICorrector<T>> validators) => _validators = validators;

        public bool Validate(T item) => Validate(item, out _);
        public bool Validate(T item, out T validated)
        {
            validated = default;

            bool validatorsPassed = true;
            foreach (var validator in _validators)
                validatorsPassed = validatorsPassed && validator.Validate(item, out validated);

            return validatorsPassed;
        }

        public void Add(ICorrector<T> component) => _validators.Add(component);
        public bool Remove(ICorrector<T> component) => _validators.Remove(component);
        public void Clear() => _validators.Clear();

    }
}
