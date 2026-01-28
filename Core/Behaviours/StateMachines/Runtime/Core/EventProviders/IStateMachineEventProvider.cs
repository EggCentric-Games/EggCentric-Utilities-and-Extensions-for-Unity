using System;

namespace EggCentric.StateMachines
{
    public interface IStateMachineEventProvider<TStateType> where TStateType : IState
    {
        public IRequestHandlerEventsProvider<TStateType> RequestEvents { get; }
        public ITransitionEvaluatorEventsProvider TransitionEvents { get; }
        public ILockEventsProvider LockEvents { get; }

        public event Action<Type> OnRegisterStateDuplication;
        public event Action<Type> OnMissingRegisteredState;
    }
}