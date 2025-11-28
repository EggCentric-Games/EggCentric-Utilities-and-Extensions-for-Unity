namespace EggCentric.StateMachines
{
    public class UrgentPolicy : ExecutionPolicy
    {
        private bool _isConsumed = false;

        public UrgentPolicy(bool isForced = false)
        {
            if (isForced)
                flags = TransitionFlags.Forced;
        }

        public override bool IsValid
        {
            get
            {
                bool isValid = !_isConsumed;
                _isConsumed = true;
                return isValid;
            }
        }
    }
}