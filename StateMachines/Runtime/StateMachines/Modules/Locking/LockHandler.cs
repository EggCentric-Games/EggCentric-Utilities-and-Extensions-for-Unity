using System;
using System.Collections.Generic;
using System.Linq;

namespace EggCentric.StateMachines
{
    public class LockHandler : ILockEventsProvider
    {
        public bool IsLocked => _activeLocks.Count > 0;

        private Dictionary<Guid, Lock> _activeLocks;

        public event Action<Lock, object> OnLockPlaced;
        public event Action<Lock> OnLockDisposed;
        public event Action<Guid> OnInvalidDisposalRequested;
        public event Action OnInvalidRequestSource;

        public LockHandler() => _activeLocks = new Dictionary<Guid, Lock>();

        public Guid RequestLock(object source, int priority = 0)
        {
            if (!HandleRequestSource(source))
                return default;

            return PlaceLock(source, priority).ID;
        }

        public void DisposeLock(Guid lockID)
        {
            if (!_activeLocks.TryGetValue(lockID, out _))
            {
                OnInvalidDisposalRequested?.Invoke(lockID);
                return;
            }

            OnLockDisposed?.Invoke(_activeLocks[lockID]);
            _activeLocks.Remove(lockID);
        }

        public void DisposeLocks(object source)
        {
            if (!HandleRequestSource(source))
                return;

            var locksToRemove = _activeLocks.Values
            .Where(activeLock => activeLock.Source == source)
            .Select(activeLock => activeLock.ID)
            .ToList();

            foreach (var activeLock in locksToRemove)
                DisposeLock(activeLock);
        }

        public bool IsLockedFor(int priority) => _activeLocks.Any(x => x.Value.Priority >= priority);

        public void ClearAllLocks() => _activeLocks.Clear();

        private Lock PlaceLock(object source, int priority = 0)
        {
            if (!HandleRequestSource(source))
                return default;

            Lock newLock = new Lock(source, priority);
            _activeLocks.Add(newLock.ID, newLock);
            OnLockPlaced?.Invoke(newLock, source);

            return newLock;
        }

        private bool HandleRequestSource(object source)
        {
            if (source == null)
            {
                OnInvalidRequestSource?.Invoke();
                return false;
            }

            return true;
        }

    }
}