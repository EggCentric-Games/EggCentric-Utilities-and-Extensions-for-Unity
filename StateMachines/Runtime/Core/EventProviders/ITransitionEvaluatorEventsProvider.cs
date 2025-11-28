using System;

namespace EggCentric.StateMachines
{
    public interface ITransitionEvaluatorEventsProvider
    {
        public event Action OnUninitializedStateMachine;
        public event Action<Type> OnMissingRegisteredState;
        public event Action<Type> OnRegisterStateDuplication;

        public event Action<Type, ITransition> OnTransitionAdded;

        public event Action<Type, Type> OnMissingTransition;
        public event Action<Type, Type> OnInvalidStateType;
    }
}
