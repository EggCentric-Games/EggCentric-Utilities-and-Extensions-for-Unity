using EggCentric.CollectionProcessing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EggCentric.Effects.Sources
{
    public class TargetingPipeline<T> : ICollectionProcessor<T>
    {
        private ICollectionProcessor<T> _reducer;
        private IItemSelector<T> _selector;

        public IEnumerable<T> Process(IEnumerable<T> entries)
        {
            if (entries == null || entries.Count() <= 0)
                return Array.Empty<T>();

            var result = _reducer?.Process(entries) ?? entries;
            return _selector?.Process(result.ToList()) ?? result;
        }

        public void WithReducer(ICollectionProcessor<T> reducer) => _reducer = reducer;
        public void WithSelector(IItemSelector<T> selector) => _selector = selector;
    }
}