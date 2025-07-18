namespace EggCentric.Configurables
{
    public interface IConfigurable<TConfig> where TConfig : IConfig
    {
        public void ApplyConfig(TConfig config);
    }
}