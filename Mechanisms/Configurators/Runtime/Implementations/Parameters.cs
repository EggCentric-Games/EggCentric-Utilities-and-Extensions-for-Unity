using System;

namespace EggCentric.Configurators
{
    public abstract class Parameters<TParameters> : IConfigurator<TParameters> where TParameters : Parameters<TParameters>
    {
        public event Action OnParametersChanged;

        public abstract bool Validate(out TParameters validated);
        public abstract void ResetToSerialized();

        bool IConfigurator.Validate(out IConfigurator validated)
        {
            bool success = Validate(out TParameters typedValidated);
            validated = typedValidated;

            return success;
        }
    }
}