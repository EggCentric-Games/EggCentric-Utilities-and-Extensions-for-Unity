namespace EggCentric.StateMachines
{
    public interface IState
    {
        public void Exit();
    }

    public interface ICommonState : IState
    {
        public void Enter();
    }

    public interface IPayloadedState<TPayload> : IState
    {
        public void Enter(TPayload payload);
    }
}