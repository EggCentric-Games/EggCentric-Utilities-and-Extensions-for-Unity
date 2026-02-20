using System.Collections.Generic;
using System.Linq;

namespace EggCentric.CollectionProcessing.Types
{
    public abstract class ApplicationCountLimiter<T> : ICollectionProcessor<T>
    {
        public IEnumerable<T> Process(IEnumerable<T> entries) => entries.Where(x => CheckItemValidity(x));

        public abstract bool CheckItemValidity(T item);
        public abstract void RegisterApplicationOnItem(T item);
    }
}
