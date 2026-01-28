namespace EggCentric.StateMachines
{

    public interface IPayloadedState<TPayload> : IState
    {
        public void Enter(TPayload payload);
    }
}