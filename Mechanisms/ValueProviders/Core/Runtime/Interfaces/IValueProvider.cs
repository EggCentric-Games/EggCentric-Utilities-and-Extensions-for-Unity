namespace EggCentric.ValueProviders
{
    public interface IValueProvider<out T>
    {
        public T Value { get; }
    }
}