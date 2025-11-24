using System;
using System.Collections.Generic;
using UnityEngine;

namespace EggCentric.StateMachines
{
    public class TransitionEvaluator<TStateType> where TStateType : IState
    {
        private IStateMachine<TStateType> _stateMachine;
        private Dictionary<Type, List<ITransition>> transitions;

        public TransitionEvaluator(IStateMachine<TStateType> stateMachine)
        {
            transitions = new Dictionary<Type, List<ITransition>>();

            _stateMachine = stateMachine;
        }

        public void RegisterState<TState>() where TState : class, IState, TStateType
        {
            transitions.Add(typeof(TState), new List<ITransition>());
        }

        public Transition<TTarget> AddTransition<TSource, TTarget>() where TSource : class, IState, TStateType where TTarget : class, IState, TStateType
        {
            if (!transitions.TryGetValue(typeof(TSource), out var stateTransitions))
            {
                Debug.LogError($"There is no registered states of type {typeof(TSource)}!");
                return null;
            }

            Transition<TTarget> newTransition = new Transition<TTarget>();
            stateTransitions.Add(newTransition);

            return newTransition;
        }

        public bool ResolveTransition(TransitionRequest request, out ITransition result)
        {
            result = null;

            var source = _stateMachine.CurrentState?.GetType();
            var target = request.TargetState;
            var transitionAvailable = request.Flags.HasFlag(TransitionFlags.Forced) || _stateMachine.IsFreeFor(request.Priority);

            if(!transitionAvailable)
            {
                Debug.LogWarning($"State machine is locked for {request.Priority}. Rejecting.");
                return false;
            }

            if (source == null)
            {
                Debug.LogError($"State machine must be initialized first.");
                return false;
            }

            if (!transitions.TryGetValue(source, out List<ITransition> stateTransitions))
            {
                Debug.LogWarning($"There's is no registered state of type {source}. Rejecting.");
                return false;
            }

            List<ITransition> availableTransitions = stateTransitions.FindAll(x => x.TargetState == target);

            if (availableTransitions.Count <= 0)
            {
                Debug.LogWarning($"There's is no transition from {source} to {target}. Rejecting.");
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
    }
}
