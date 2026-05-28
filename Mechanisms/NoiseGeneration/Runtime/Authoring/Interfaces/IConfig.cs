namespace EggCentric.NoiseGeneration
{
    public interface IConfig<out TInstance>
    {
        public TInstance CreateInstance();
    }
}