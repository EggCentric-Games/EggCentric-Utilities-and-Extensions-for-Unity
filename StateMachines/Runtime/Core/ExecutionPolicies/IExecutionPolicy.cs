namespace EggCentric.StateMachines
{
    public interface IExecutionPolicy
    {
        public TransitionFlags TransitionFlags { get; }

        public abstract bool IsValid { get; }
    }
}