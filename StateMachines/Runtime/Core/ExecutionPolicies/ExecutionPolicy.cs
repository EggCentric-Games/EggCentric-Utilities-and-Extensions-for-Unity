namespace EggCentric.StateMachines
{
    public abstract class ExecutionPolicy : IExecutionPolicy
    {
        public TransitionFlags TransitionFlags => flags;
        public abstract bool IsValid { get; }

        protected TransitionFlags flags;
    }
}