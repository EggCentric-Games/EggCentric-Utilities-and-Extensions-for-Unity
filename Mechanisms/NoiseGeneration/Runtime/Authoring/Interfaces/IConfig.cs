namespace EggCentric.ProceduralGeneration
{
    public interface IConfig<out TInstance>
    {
        public TInstance CreateInstance();
    }
}