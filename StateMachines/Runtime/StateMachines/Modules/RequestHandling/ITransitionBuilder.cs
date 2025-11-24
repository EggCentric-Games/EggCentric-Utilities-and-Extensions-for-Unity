namespace EggCentric.StateMachines
{
    public interface ITransitionBuilder
    {
        public ITransitionBuilder WithPriority(int priority);

        public void Now();
        public void Forced();
        public void AwaitFor(float lifetime);
    }
}
