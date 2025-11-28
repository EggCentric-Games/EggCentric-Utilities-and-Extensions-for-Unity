using System;
using System.Collections.Generic;

namespace EggCentric.StateMachines
{

    public class TransitionEvaluator<TStateType> : ITransitionEvaluatorEventsProvider where TStateType : IState
    {
        private readonly IStateMachine<TStateType> _stateMachine;
        private Dictionary<Type, List<ITransition>> _transitions;

        public event Action OnUninitializedStateMachine;
        public event Action<Type> OnMissingRegisteredState;
        public event Action<Type> OnRegisterStateDuplication;

        public event Action<ITransition> OnTransitionAdded;

        public event Action<Type, Type> OnMissingTransition;
        public event Action<Type, Type> OnInvalidStateType;

        public TransitionEvaluator(IStateMachine<TStateType> stateMachine)
        {
            _stateMachine = stateMachine;
            _transitions = new Dictionary<Type, List<ITransition>>();
        }

        public void RegisterState<TState>() where TState : class, IState, TStateType
        {
            if(_transitions.ContainsKey(typeof(TState)))
            {
                OnRegisterStateDuplication?.Invoke(typeof(TState));
                return;
            }

            _transitions.Add(typeof(TState), new List<ITransition>());
        }

        public IReadOnlyList<ITransition> AvailableTransitions(Type type) => GetStateTransitions(type);

        public Transition<TTarget> AddTransition<TSource, TTarget>() where TSource : class, IState, TStateType where TTarget : class, IState, TStateType
        {
            var stateTransitions = GetStateTransitions(typeof(TSource));

            if (stateTransitions == null)
                return null;

            Transition<TTarget> newTransition = new Transition<TTarget>();
            stateTransitions.Add(newTransition);

            OnTransitionAdded?.Invoke(newTransition);
            return newTransition;
        }

        public bool ResolveTransition<TTarget>(out ITransition result) where TTarget : class, IState, TStateType => ResolveTransition<TTarget>(_stateMachine.CurrentState, out result);

        private bool ResolveTransition<TTarget>(IState source, out ITransition result) where TTarget : class, IState, TStateType
        {
            result = null;

            if (source == null)
            {
                OnUninitializedStateMachine?.Invoke();
                return false;
            }

            Type stateType = source.GetType();
            if (!typeof(TStateType).IsAssignableFrom(stateType))
            {
                OnInvalidStateType?.Invoke(stateType, typeof(TStateType));
                return false;
            }

            var stateTransitions = GetStateTransitions(stateType);

            if (stateTransitions == null)
                return false;

            List<ITransition> availableTransitions = stateTransitions.FindAll(x => x.TargetState == typeof(TTarget));

            if (availableTransitions.Count <= 0)
            {
                OnMissingTransition?.Invoke(stateType, typeof(TTarget));
                return false;
            }

            foreach (var transition in availableTransitions)
            {
                if (transition.IsSatisfied)
                {
                    result = transition;
                    return true;
                }
            }

            return false;
        }

        private List<ITransition> GetStateTransitions(Type stateType)
        {
            if (!typeof(TStateType).IsAssignableFrom(stateType))
            {
                OnInvalidStateType?.Invoke(stateType, typeof(TStateType));
                return null;
            }

            if (!_transitions.TryGetValue(stateType, out var stateTransitions))
            {
                OnMissingRegisteredState?.Invoke(stateType);
                return null;
            }

            return stateTransitions;
        }
    }
}
