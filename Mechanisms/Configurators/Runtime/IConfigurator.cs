namespace EggCentric.Configurators
{
    public interface IConfigurator
    {
        public bool Validate(out IConfigurator validated);
    }
}