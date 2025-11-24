namespace EggCentric.StateMachines
{
    public interface IStateMachine<TStateType> where TStateType : IState
    {
        public TStateType CurrentState { get; }

        public ITransitionBuilder To<TTarget>() where TTarget : class, ICommonState, TStateType;
        public ITransitionBuilder To<TTarget, TPayload>(TPayload payload) where TTarget : class, IPayloadedState<TPayload>, TStateType;
        public void ExecuteTransition<TTarget>(ITransition<TTarget> transition) where TTarget : class, ICommonState, TStateType;
        public void ExecuteTransition<TTarget, TPayload>(ITransition<TTarget> transition, TPayload payload) where TTarget : class, IPayloadedState<TPayload>, TStateType;
    }
}