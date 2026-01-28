using System;

namespace EggCentric.StateMachines
{
    public class PlainRequest<TStateType, TTarget> : TransitionRequest<TStateType, TTarget> where TTarget : class, IPlainState, TStateType where TStateType : IState
    {
        public PlainRequest(object source, IExecutionPolicy executionPolicy, int priority = 0) : base(source, executionPolicy, priority)
        {
        }

        protected override Action<ITransition> CreateExecutor(IStateMachine<TStateType> stateMachine)
        {
            Action<ITransition> executor = transition => {
                if (Convert(transition, out var typedTransition))
                    stateMachine.ExecuteTransition(typedTransition);
            };

            return executor;
        }
    }
}