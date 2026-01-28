public interface IItemStack<T> : IReadOnlyItemStack<T>
{
    public void Add(T item);
    public bool Remove(T item);
    public void Clear();
}