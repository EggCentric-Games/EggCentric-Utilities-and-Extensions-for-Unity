namespace EggCentric.StateMachines
{
    public interface ITransitionBuilder
    {
        public ITransitionBuilder WithPriority(int priority);

        public TransitionRequest Now();
        public TransitionRequest Forced();
        public TransitionRequest AwaitFor(float lifetime);
    }
}
