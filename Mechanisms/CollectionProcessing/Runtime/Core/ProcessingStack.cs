using System.Collections.Generic;
using System.Linq;

namespace EggCentric.CollectionProcessing
{
    public class ProcessingStack<T> : IProcessingStack<T>
    {
        public IReadOnlyList<ICollectionProcessor<T>> Processors => _processors;

        private readonly List<ICollectionProcessor<T>> _processors;

        public ProcessingStack() : this(new List<ICollectionProcessor<T>>()) { }
        public ProcessingStack(List<ICollectionProcessor<T>> processors) => _processors = processors;

        public IEnumerable<T> Process(IEnumerable<T> entries)
        {
            if(entries == null || _processors == null || _processors.Count <= 0)
                return Enumerable.Empty<T>();

            IEnumerable<T> processedEntries = entries;
            foreach(var processor in _processors)
                processedEntries = processor?.Process(processedEntries);

            return processedEntries;
        }

        public void Add(ICollectionProcessor<T> processor) => _processors.Add(processor);
        public bool Remove(ICollectionProcessor<T> processor) => _processors.Remove(processor);
    }
}
