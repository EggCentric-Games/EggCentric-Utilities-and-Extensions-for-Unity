using System.Collections.Generic;

namespace EggCentric.Validation
{
    public class ValidationStack<T> : IValidationStack<T>
    {
        public IReadOnlyList<IValidator<T>> Items => _validators;

        private readonly List<IValidator<T>> _validators;

        public ValidationStack(IReadOnlyList<IValidator<T>> validators = null)
        {
            if(validators != null)
                _validators = new List<IValidator<T>>(validators);
            else
                _validators = new List<IValidator<T>>();
        }

        public bool Validate(T item)
        {
            bool validatorsPassed = true;
            foreach (var validator in _validators)
                validatorsPassed = validatorsPassed && validator.Validate(item);

            return validatorsPassed;
        }

        public void Add(IValidator<T> component) => _validators.Add(component);
        public bool Remove(IValidator<T> component) => _validators.Remove(component);
        public void Clear() => _validators.Clear();
    }
}
