using System;

namespace EggCentric.StateMachines
{
    public class UrgentRequest : TransitionRequest
    {
        public override bool IsValid => true;
        public bool IsForced { get; private set; }

        public UrgentRequest(Type targetState, bool isForced = false, int priority = 0) : base(targetState, priority)
        {
            IsForced = isForced;
        }
    }
}