using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EggCentric.StateMachines
{
    public class LockHandler
    {
        public bool IsLocked => _activeLocks.Count > 0;

        private Dictionary<Guid, Lock> _activeLocks;

        public LockHandler()
        {
            _activeLocks = new Dictionary<Guid, Lock>();
        }

        public Guid RequestLock(object source, int priority = 0)
        {
            Lock newLock = new Lock(source, priority);
            _activeLocks.Add(newLock.ID, newLock);

            return newLock.ID;
        }

        public void DisposeLock(Guid lockID)
        {
            if (!_activeLocks.TryGetValue(lockID, out _))
            {
                Debug.LogWarning($"There is no lock with ID {lockID}");
                return;
            }

            _activeLocks.Remove(lockID);
        }

        public bool IsLockedFor(int priority) => _activeLocks.All(x => x.Value.priority < priority);

        public void ClearAllLocks() => _activeLocks.Clear();

    }
}