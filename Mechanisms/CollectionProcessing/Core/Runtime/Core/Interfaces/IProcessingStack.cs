namespace EggCentric.CollectionProcessing
{
    public interface IProcessingStack<T> : IReadOnlyProcessingStack<T>
    {
        public void Add(ICollectionProcessor<T> processor);
        public bool Remove(ICollectionProcessor<T> processor);
    }
}
