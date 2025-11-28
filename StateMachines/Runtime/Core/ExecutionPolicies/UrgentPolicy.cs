namespace EggCentric.StateMachines
{
    public class UrgentPolicy : ExecutionPolicy
    {
        private bool _isConsumed = false;

        public UrgentPolicy(TransitionFlags flags) : base(flags)
        {

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