namespace EggCentric.NoiseGeneration
{
    public interface IValueSourceConfig<out TValueSource> : IConfig<TValueSource> where TValueSource : IValueSource
    {
        public TValueSource CreateInstance();
    }
}