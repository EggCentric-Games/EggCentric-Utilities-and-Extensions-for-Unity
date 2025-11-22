namespace EggCentric.StateMachines
{
    public interface IStateMachine<TStateType>
    {
        void Enter<TState>() where TState : class, TStateType, ICommonState;
        void Enter<TState, TPayload>(TPayload payload) where TState : class, TStateType, IPayloadedState<TPayload>;
    }
}