namespace EggCentric.StateMachines
{
    public interface ITransitionBuilder
    {
        public void Now();
        public void Forced();
        public void AwaitFor(float lifetime);
    }
}
