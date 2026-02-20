using System.Collections.Generic;

namespace EggCentric.CollectionProcessing
{
    public interface IItemSelector<T>
    {
        public IReadOnlyList<T> Process(IReadOnlyList<T> entries);
    }
}
