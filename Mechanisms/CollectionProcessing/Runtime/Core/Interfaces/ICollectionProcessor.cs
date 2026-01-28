using System.Collections.Generic;

namespace EggCentric.CollectionProcessing
{
    public interface ICollectionProcessor<T>
    {
        public IEnumerable<T> Process(IEnumerable<T> entries);
    }
}
