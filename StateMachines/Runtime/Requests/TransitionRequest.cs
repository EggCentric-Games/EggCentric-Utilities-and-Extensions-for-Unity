namespace EggCentric.StateMachines
{
    public abstract class TransitionRequest
    {
        public abstract bool IsValid { get; }

        protected Transition meantTransition;

        public TransitionRequest(Transition meantTransition)
        {
            this.meantTransition = meantTransition;
        }
    }
}