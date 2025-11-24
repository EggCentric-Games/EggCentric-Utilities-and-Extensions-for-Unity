using System;

namespace EggCentric.StateMachines
{
    public abstract class TransitionRequest
    {
        public Type TargetState => _targetState;
        public int Priority => _priority;
        public TransitionFlags Flags => _flags;

        public abstract bool IsValid { get; }

        private readonly Type _targetState;
        private readonly int _priority;
        protected TransitionFlags _flags;

        public TransitionRequest(Type targetState, int priority = 0)
        {
            _targetState = targetState;
            _priority = priority;
        }
    }
}