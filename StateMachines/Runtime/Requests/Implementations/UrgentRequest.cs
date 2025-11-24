using System;

namespace EggCentric.StateMachines
{
    public class UrgentRequest : TransitionRequest
    {
        public override bool IsValid => true;

        public UrgentRequest(Type targetState, bool isForced = false, int priority = 0) : base(targetState, priority)
        {
            if(isForced)
                _flags = TransitionFlags.Forced;
        }
    }
}