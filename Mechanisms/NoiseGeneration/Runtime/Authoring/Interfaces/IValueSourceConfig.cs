namespace EggCentric.ProceduralGeneration
{
    public interface IValueSourceConfig<out TValueSource> : IConfig<TValueSource> where TValueSource : IValueSource
    {
        public TValueSource CreateInstance();
    }
}