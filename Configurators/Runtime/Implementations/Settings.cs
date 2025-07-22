namespace EggCentric.Configurators
{
    public abstract class Settings<TSettings> : IConfigurator<TSettings> where TSettings : Settings<TSettings>
    {
        public abstract bool Validate(out TSettings validated);

        bool IConfigurator.Validate(out IConfigurator validated)
        {
            bool success = Validate(out TSettings typedValidated);
            validated = typedValidated;

            return success;
        }
    }
}