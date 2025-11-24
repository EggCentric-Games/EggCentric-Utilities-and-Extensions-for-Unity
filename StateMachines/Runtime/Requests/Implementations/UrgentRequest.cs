using System;

namespace EggCentric.StateMachines
{
    public class UrgentRequest : TransitionRequest
    {
        public override bool IsValid
        {
            get {
                bool isValid = !_isConsumed;
                _isConsumed = true;
                return isValid;
            }
        }

        private bool _isConsumed = false;

        public UrgentRequest(Type targetState, bool isForced = false, int priority = 0) : base(targetState, priority)
        {
            if(isForced)
                _flags = TransitionFlags.Forced;
        }
    }
}