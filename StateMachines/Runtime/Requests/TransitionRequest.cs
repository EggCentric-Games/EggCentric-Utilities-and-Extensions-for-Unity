using System;

namespace EggCentric.StateMachines
{
    public abstract class TransitionRequest
    {
        public object Source => _source; 
        public Type TargetState => _targetState;
        public int Priority => _priority;
        public TransitionFlags Flags => _flags;

        public abstract bool IsValid { get; }

        protected TransitionFlags _flags;

        private readonly object _source;
        private readonly Type _targetState;
        private readonly int _priority;

        public TransitionRequest(Type targetState, object source, int priority = 0)
        {
            _source = source;
            _targetState = targetState;
            _priority = priority;
        }
    }
}