namespace EggCentric.Configurables
{
    public interface IStructConfig<TConfig> : IConfig
    {
        public void Validate(ref TConfig config);
    }
}