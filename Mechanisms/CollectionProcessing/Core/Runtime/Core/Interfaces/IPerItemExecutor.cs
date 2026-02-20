using System.Collections.Generic;

namespace EggCentric.CollectionProcessing
{
    public interface IPerItemExecutor<T>
    {
        public void ExecuteFor(IEnumerable<T> entries);
    }
}
