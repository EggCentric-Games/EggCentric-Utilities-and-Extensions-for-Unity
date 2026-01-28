using System.Collections.Generic;

public interface IReadOnlyItemStack<out T>
{
    public IReadOnlyList<T> Items { get; }
}
