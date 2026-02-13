using System;
using UnityEngine;

namespace EggCentric.StateMachines
{
    public class TransitionEvaluatorDebugger : IDebugger
    {
        private ITransitionEvaluatorEventsProvider _eventsProvider;

        public TransitionEvaluatorDebugger(ITransitionEvaluatorEventsProvider eventsProvider) => _eventsProvider = eventsProvider;

        public void Enable() => EnableTransitionEvaluatorEvents();

        public void Disable() => DisableTransitionEvaluatorEvents();

        private void EnableTransitionEvaluatorEvents()
        {
            _eventsProvider.OnUninitializedStateMachineUsage += LogUninitializedStateMachine;
            _eventsProvider.OnMissingRegisteredState += LogMissingRegisteredState;
            _eventsProvider.OnRegisterStateDuplication += LogRegisterStateDuplication;
            _eventsProvider.OnTransitionAdded += LogTransitionAdded;
            _eventsProvider.OnMissingTransition += LogMissingTransition;
            _eventsProvider.OnInvalidStateType += LogInvalidStateType;
        }

        private void DisableTransitionEvaluatorEvents()
        {
            _eventsProvider.OnUninitializedStateMachineUsage -= LogUninitializedStateMachine;
            _eventsProvider.OnMissingRegisteredState -= LogMissingRegisteredState;
            _eventsProvider.OnRegisterStateDuplication -= LogRegisterStateDuplication;
            _eventsProvider.OnTransitionAdded -= LogTransitionAdded;
            _eventsProvider.OnMissingTransition -= LogMissingTransition;
            _eventsProvider.OnInvalidStateType -= LogInvalidStateType;
        }

        private void LogUninitializedStateMachine() => Debug.LogError($"Current state of state machine is null, state machine should be initialized before usage!");
        private void LogMissingRegisteredState(Type stateType) => Debug.LogError($"Transition evaluator has no registered states with type {stateType}!");
        private void LogRegisterStateDuplication(Type stateType) => Debug.LogWarning($"Transition evaluator already has registered state with type {stateType}!");
        private void LogTransitionAdded(Type source, ITransition transition) => Debug.Log($"Added transition from {source} to {transition.TargetState}.");
        private void LogMissingTransition(Type source, Type target) => Debug.LogWarning($"There is no transitions from {source} to {target}!");
        private void LogInvalidStateType(Type stateType, Type targetType) => Debug.LogError($"{stateType} can not be converted into {targetType}!");

    }
}
