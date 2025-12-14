using System;

namespace EggCentric.DataContainers
{
    public interface IReadOnlyDataCache<T> : ITrackableValue<T>
    {
        public bool IsValid { get; }

        public event Action OnCacheInvalidated;
    }
}