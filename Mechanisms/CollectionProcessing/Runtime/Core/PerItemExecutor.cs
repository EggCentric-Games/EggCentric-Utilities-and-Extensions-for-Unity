using System.Collections.Generic;

namespace EggCentric.CollectionProcessing
{
    public abstract class PerItemExecutor<T> : IPerItemExecutor<T>
    {
        public virtual void ExecuteFor(IEnumerable<T> entries)
        {
            foreach (var entry in entries)
                HandleProcessResult(entry, ExecuteFor(entry));
        }

        protected abstract bool ExecuteFor(T entry);
        protected abstract void HandleProcessResult(T entry, bool result = true);
    }
}
