public interface IStateMachine<TStateType>
{
    void Enter<TState>() where TState : class, TStateType, IState;
    void Enter<TState, TPayload>(TPayload payload) where TState : class, TStateType, IPayloadedState<TPayload>;
}
