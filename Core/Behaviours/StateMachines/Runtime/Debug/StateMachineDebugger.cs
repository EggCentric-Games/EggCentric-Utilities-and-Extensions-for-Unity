using System;
using UnityEngine;

namespace EggCentric.StateMachines
{
    public class StateMachineDebugger<TStateType> where TStateType : IState
    {
        private IStateMachineEventProvider<TStateType> _eventProvider;
        private RequestHandlerDebugger<TStateType> _requestDebugger;
        private TransitionEvaluatorDebugger _transitionDebugger;
        private LockHandlerDebugger _lockDebugger;

        public StateMachineDebugger(IStateMachineEventProvider<TStateType> eventProvider)
        {
            _eventProvider = eventProvider;
            _requestDebugger = new RequestHandlerDebugger<TStateType>(_eventProvider.RequestEvents);
            _transitionDebugger = new TransitionEvaluatorDebugger(_eventProvider.TransitionEvents);
            _lockDebugger = new LockHandlerDebugger(_eventProvider.LockEvents);
        }

        public void Enable() => EnableAllEvents();

        public void Disable() => DisableAllEvents();

        private void EnableAllEvents()
        {
            EnableStateMachineEvents();
            _requestDebugger.Enable();
            _transitionDebugger.Enable();
            _lockDebugger.Enable();
        }

        private void DisableAllEvents()
        {
            DisableStateMachineEvents();
            _requestDebugger.Disable();
            _transitionDebugger.Disable();
            _lockDebugger.Disable();
        }

        private void EnableStateMachineEvents()
        {
            _eventProvider.OnMissingRegisteredState += LogMissingRegisteredState;
            _eventProvider.OnRegisterStateDuplication += LogRegisterStateDuplication;
        }

        private void DisableStateMachineEvents()
        {
            _eventProvider.OnMissingRegisteredState -= LogMissingRegisteredState;
            _eventProvider.OnRegisterStateDuplication -= LogRegisterStateDuplication;
        }

        private void LogMissingRegisteredState(Type stateType) => Debug.LogError($"State machine has no registered states with type {stateType}!");
        private void LogRegisterStateDuplication(Type stateType) => Debug.LogWarning($"State machine already has registered state with type {stateType}!");
    }
}
