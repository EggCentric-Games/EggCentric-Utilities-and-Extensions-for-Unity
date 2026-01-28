namespace EggCentric.DataContainers
{
    public interface IValueProvider<T>
    {
        public T Value { get; }
    }
}