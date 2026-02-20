using EggCentric.ValueProviders;
using System;

namespace EggCentric.ValueProviders.DataContainers
{
    public interface IReadOnlyDataCache<T> : ITrackableValue<T>
    {
        public bool IsValid { get; }

        public event Action OnCacheInvalidated;
    }
}