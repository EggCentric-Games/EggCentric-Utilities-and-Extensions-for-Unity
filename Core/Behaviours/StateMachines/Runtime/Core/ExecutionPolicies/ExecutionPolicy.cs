namespace EggCentric.StateMachines
{
    public abstract class ExecutionPolicy : IExecutionPolicy
    {
        public TransitionFlags TransitionFlags => flags;
        public abstract bool IsValid { get; }

        protected readonly TransitionFlags flags;

        public ExecutionPolicy(TransitionFlags flags) => this.flags = flags;
    }
}