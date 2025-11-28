using System;

namespace EggCentric.StateMachines
{
    public interface ILockEventsProvider
    {
        public event Action<Lock, object> OnLockPlaced;
        public event Action<Lock> OnLockDisposed;
        public event Action<Guid> OnInvalidDisposalRequested;
        public event Action OnInvalidRequestSource;
    }
}