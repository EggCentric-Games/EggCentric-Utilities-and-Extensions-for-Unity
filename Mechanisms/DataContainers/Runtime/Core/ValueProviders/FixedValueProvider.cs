using EggCentric.DataContainers;

public class FixedValueProvider<T> : IValueProvider<T>
{
    public T Value { get; private set; }

    public FixedValueProvider(T value) => SetValue(value);

    public void SetValue(T value) => Value = value;
}
