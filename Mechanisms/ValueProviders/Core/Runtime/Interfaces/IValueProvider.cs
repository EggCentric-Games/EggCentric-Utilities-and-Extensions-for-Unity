namespace EggCentric.ValueProviders
{
    public interface IValueProvider<T>
    {
        public T Value { get; }
    }
}