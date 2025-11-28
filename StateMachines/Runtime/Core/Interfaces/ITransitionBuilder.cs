namespace EggCentric.StateMachines
{
    public interface ITransitionBuilder<TStateType> where TStateType : IState
    {
        public ITransitionBuilder<TStateType> WithPriority(int priority);
        public ITransitionBuilder<TStateType> IgnoreLocks();
        public ITransitionBuilder<TStateType> IgnoreConditions();

        public TransitionRequest<TStateType> Now();
        public TransitionRequest<TStateType> Forced();
        public TransitionRequest<TStateType> AwaitFor(float lifetime = -1f);
    }
}
