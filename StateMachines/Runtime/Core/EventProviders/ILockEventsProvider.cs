using System;

namespace EggCentric.StateMachines
{
    public interface ILockEventsProvider
    {
        public event Action<object, int> OnLockRequested;
        public event Action<Guid> OnLockDisposeRequested;
        public event Action<Lock> OnLockPlaced;
        public event Action<Lock> OnLockDisposed;
        public event Action<Guid> OnInvalidDisposalRequested;
        public event Action OnInvalidRequestSource;
    }
}