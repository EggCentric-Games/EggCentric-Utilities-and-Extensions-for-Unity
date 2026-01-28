using System;

namespace EggCentric.StateMachines
{
    public delegate bool Resolver(out ITransition transition, bool ignoreConditions = false);

    public abstract class TransitionRequest<TRequestType> where TRequestType : IState
    {
        public abstract Type TargetState { get; }
        public bool IsValid => _executionPolicy.IsValid;
        public object Source => _source; 
        public int Priority => _priority;
        public TransitionFlags Flags => _executionPolicy.TransitionFlags;

        private readonly IExecutionPolicy _executionPolicy;
        private readonly object _source;
        private readonly int _priority;

        private Action<ITransition> _executor;
        private Resolver _resolver;

        public abstract event Action<ITransition> OnInvalidTransitionType; // to-do: implement proper handling

        public TransitionRequest(object source, IExecutionPolicy executionPolicy, int priority = 0)
        {
            _source = source;
            _executionPolicy = executionPolicy;
            _priority = priority;
        }

        public bool TryToPerform()
        {
            if (!_resolver(out ITransition performedTransition, Flags.HasFlag(TransitionFlags.IgnoreConditions)))
                return false;

            _executor(performedTransition);
            return true;
        }

        public void InitializeExecutor(IStateMachine<TRequestType> stateMachine) => _executor = CreateExecutor(stateMachine);
        public void InitializeResolver(TransitionEvaluator<TRequestType> transitionEvaluator) => _resolver = CreateResolver(transitionEvaluator);

        protected abstract Action<ITransition> CreateExecutor(IStateMachine<TRequestType> stateMachine);
        protected abstract Resolver CreateResolver(TransitionEvaluator<TRequestType> transitionEvaluator);

    }

    public abstract class TransitionRequest<TStateType, TTarget> : TransitionRequest<TStateType> where TTarget : class, IState, TStateType where TStateType : IState
    {
        public override Type TargetState => typeof(TTarget);

        public override event Action<ITransition> OnInvalidTransitionType;

        protected TransitionRequest(object source, IExecutionPolicy executionPolicy, int priority = 0) : base(source, executionPolicy, priority)
        {
        }

        protected bool Convert(ITransition transition, out ITransition<TTarget> result)
        {
            if (transition is ITransition<TTarget> typedTransition)
            {
                result = typedTransition;
                return true;
            }

            OnInvalidTransitionType?.Invoke(transition);

            result = null;
            return false;
        }

        protected override Resolver CreateResolver(TransitionEvaluator<TStateType> transitionEvaluator) => transitionEvaluator.ResolveTransition<TTarget>;
    }
}