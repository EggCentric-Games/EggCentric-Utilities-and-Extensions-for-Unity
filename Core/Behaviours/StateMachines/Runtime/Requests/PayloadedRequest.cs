using System;

namespace EggCentric.StateMachines
{
    public class PayloadedRequest<TStateType, TTarget, TPayload> : TransitionRequest<TStateType, TTarget> where TTarget : class, IPayloadedState<TPayload>, TStateType where TStateType : IState
    {
        private readonly TPayload _payload;

        public PayloadedRequest(object source, TPayload payload, IExecutionPolicy executionPolicy, int priority = 0) : base(source, executionPolicy, priority)
        {
            _payload = payload;
        }

        protected override Action<ITransition> CreateExecutor(IStateMachine<TStateType> stateMachine)
        {
            Action<ITransition> executor = transition => {
                if (Convert(transition, out var typedTransition))
                    stateMachine.ExecuteTransition(typedTransition, _payload);
            };

            return executor;
        }
    }
}