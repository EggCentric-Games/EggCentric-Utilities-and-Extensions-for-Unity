using System;
using UnityEngine;

namespace EggCentric.StateMachines
{
    public class LockHandlerDebugger : IDebugger
    {
        private ILockEventsProvider _eventsProvider;

        public LockHandlerDebugger(ILockEventsProvider eventsProvider) => _eventsProvider = eventsProvider;

        public void Enable() => EnableLockHandlerEventsEvents();

        public void Disable() => DisableLockHandlerEventsEvents();

        private void EnableLockHandlerEventsEvents()
        {
            _eventsProvider.OnInvalidRequestSource += LogInvalidRequestSource;
            _eventsProvider.OnLockRequested += LogLockRequested;
            _eventsProvider.OnLockDisposeRequested += LogLockDisposeRequested;
            _eventsProvider.OnLockPlaced += LogLockPlaced;
            _eventsProvider.OnInvalidDisposalRequested += LogInvalidDisposalRequested;
            _eventsProvider.OnLockDisposed += LogLockDisposed;
        }

        private void DisableLockHandlerEventsEvents()
        {
            _eventsProvider.OnInvalidRequestSource -= LogInvalidRequestSource;
            _eventsProvider.OnLockRequested -= LogLockRequested;
            _eventsProvider.OnLockDisposeRequested -= LogLockDisposeRequested;
            _eventsProvider.OnLockPlaced -= LogLockPlaced;
            _eventsProvider.OnInvalidDisposalRequested -= LogInvalidDisposalRequested;
            _eventsProvider.OnLockDisposed -= LogLockDisposed;
        }

        private void LogInvalidRequestSource() => Debug.LogError($"The source of lock placement request must not be null!");
        private void LogLockRequested(object source, int priority) => Debug.Log($"{source} requested to place lock with priority {priority}");
        private void LogLockDisposeRequested(Guid lockId) => Debug.Log($"Received request to dispose lock with ID: \"{lockId}\"");
        private void LogLockPlaced(Lock @lock) => Debug.Log($"Placed lock with priority {@lock.Priority}. Source: {@lock.Source}.\nID: {@lock.ID}");
        private void LogInvalidDisposalRequested(Guid lockId) => Debug.LogError($"Invalid request to dispose with ID: \"{lockId}\"!");
        private void LogLockDisposed(Lock @lock) => Debug.LogError($"Lock with ID \"{@lock.ID}\" was disposed");
    }
}
