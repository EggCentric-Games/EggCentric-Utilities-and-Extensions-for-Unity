using EggCentric.Configurators;

namespace EggCentric.Configurables
{
    public interface IConfigurable<TConfigurator> where TConfigurator : IConfigurator
    {
        public void ApplyConfig(TConfigurator config);
    }
}