using System.Collections.Generic;

namespace EggCentric.CollectionProcessing
{
    public interface IReadOnlyProcessingStack<T> : ICollectionProcessor<T>
    {
        public IReadOnlyList<ICollectionProcessor<T>> Processors { get; }
    }
}
