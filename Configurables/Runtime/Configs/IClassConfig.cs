namespace EggCentric.Configurables
{
    public interface IClassConfig<TConfig> : IConfig
    {
        public TConfig Validate();
    }
}