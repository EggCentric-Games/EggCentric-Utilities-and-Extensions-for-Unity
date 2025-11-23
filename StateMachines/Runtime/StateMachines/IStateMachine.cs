namespace EggCentric.StateMachines
{
    public interface IStateMachine<TStateType> where TStateType : IState
    {
        public TStateType CurrentState { get; }

        public void ExecuteTransition<TTarget>(ITransition<TTarget> transition) where TTarget : class, ICommonState, TStateType;
        public void ExecuteTransition<TTarget, TPayload>(ITransition<TTarget> transition, TPayload payload) where TTarget : class, IPayloadedState<TPayload>, TStateType;
        //void Enter<TState>() where TState : class, TStateType, ICommonState;
        //void Enter<TState, TPayload>(TPayload payload) where TState : class, TStateType, IPayloadedState<TPayload>;
    }
}