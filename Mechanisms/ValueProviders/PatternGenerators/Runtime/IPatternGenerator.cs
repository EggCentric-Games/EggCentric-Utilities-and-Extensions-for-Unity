namespace EggCentric.ValueProviders.PatternGenerators
{
    public interface IPatternGenerator<out T> : IValueProvider<T>
    {
        public T GetNext();
        public T Peek(int offset = 0);
    }
}