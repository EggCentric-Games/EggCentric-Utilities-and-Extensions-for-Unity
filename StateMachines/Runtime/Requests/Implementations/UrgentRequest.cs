namespace EggCentric.StateMachines
{
    public class UrgentRequest : TransitionRequest
    {
        public override bool IsValid => meantTransition.IsSatisfied;
        public bool IsForced { get; private set; }

        public UrgentRequest(Transition meantTransition, bool isForced = false) : base(meantTransition)
        {
            IsForced = isForced;
        }
    }
}