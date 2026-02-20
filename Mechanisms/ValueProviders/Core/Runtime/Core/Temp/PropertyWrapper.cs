namespace EggCentric.ValueProviders
{
    public class PropertyWrapper<T> : IValueProvider<T>
    {
        public T Value { get; set; }
    }
}