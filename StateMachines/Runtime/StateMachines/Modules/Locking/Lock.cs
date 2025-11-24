using System;

namespace EggCentric.StateMachines
{
    public struct Lock
    {
        public Guid ID => _lockId;
        public object Source => _lockSource;
        public int priority => _priority;

        private readonly Guid _lockId;
        private readonly object _lockSource;
        private readonly int _priority;

        public Lock(object source, int priority)
        {
            _lockId = Guid.NewGuid();
            _lockSource = source;
            _priority = priority;
        }
    }
}