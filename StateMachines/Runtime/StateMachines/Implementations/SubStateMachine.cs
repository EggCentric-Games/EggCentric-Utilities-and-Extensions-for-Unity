namespace EggCentric.StateMachines
{
    public abstract class SubStateMachine<TStateType> : StateMachine<TStateType>, IState
    {
        public abstract void Exit();
    }
}
