public interface IItemPool<T>
{
    public T GetNext();
}

public abstract class StaticItemPool<TItem> : IItemPool<TItem>
{
    public TItem GetNext()
    {
        throw new System.NotImplementedException();
    }

    protected abstract TItem GetItem();
}

public class SequencedItemPool<T> : IItemPool<T>
{
    public T GetNext()
    {
        throw new System.NotImplementedException();
    }
}