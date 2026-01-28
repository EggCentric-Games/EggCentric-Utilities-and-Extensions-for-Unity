namespace EggCentric.Configurators
{
    public interface IConfigurator<TConfigurator> : IConfigurator where TConfigurator : IConfigurator<TConfigurator>
    {
        public bool Validate(out TConfigurator validated);
    }
}