using System;

namespace EggCentric.StateMachines
{
    public abstract class TransitionRequest
    {
        public Type TargetState { get; private set; }
        public abstract bool IsValid { get; }

        public TransitionRequest(Type targetState)
        {
            TargetState = targetState;
        }
    }
}