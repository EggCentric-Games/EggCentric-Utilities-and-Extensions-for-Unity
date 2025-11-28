using System;
using UnityEngine;

namespace EggCentric.StateMachines
{
    public class RequestHandlerDebugger<TStateType> : IDebugger where TStateType : IState
    {
        private IRequestHandlerEventsProvider<TStateType> _eventsProvider;

        public RequestHandlerDebugger(IRequestHandlerEventsProvider<TStateType> eventsProvider) => _eventsProvider = eventsProvider;

        public void Enable() => EnableRequestHandlerEvents();

        public void Disable() => DisableRequestHandlerEvents();

        private void EnableRequestHandlerEvents()
        {
            _eventsProvider.OnInvalidEnqueueRequest += LogInvalidEnqueueRequest;
            _eventsProvider.OnRequestPerformed += LogRequestPerformed;
            _eventsProvider.OnTransitionDisposeRequested += LogTransitionDisposeRequested;
            _eventsProvider.OnInvalidDisposeRequest += LogInvalidDisposeRequest;
            _eventsProvider.OnInvalidRequestSource += LogInvalidRequestSource;
            _eventsProvider.OnRequestDiscarded += LogRequestDiscarded;
            _eventsProvider.OnPriorityLock += LogPriorityLock;
            _eventsProvider.OnRequestAdded += LogRequestAdded;
            _eventsProvider.OnTransitionRequested += LogTransitionRequested;
        }

        private void DisableRequestHandlerEvents()
        {
            _eventsProvider.OnInvalidEnqueueRequest -= LogInvalidEnqueueRequest;
            _eventsProvider.OnRequestPerformed -= LogRequestPerformed;
            _eventsProvider.OnTransitionDisposeRequested -= LogTransitionDisposeRequested;
            _eventsProvider.OnInvalidDisposeRequest -= LogInvalidDisposeRequest;
            _eventsProvider.OnInvalidRequestSource -= LogInvalidRequestSource;
            _eventsProvider.OnRequestDiscarded -= LogRequestDiscarded;
            _eventsProvider.OnPriorityLock -= LogPriorityLock;
            _eventsProvider.OnRequestAdded -= LogRequestAdded;
            _eventsProvider.OnTransitionRequested -= LogTransitionRequested;
        }

        private void LogInvalidEnqueueRequest() => Debug.LogError($"Performed attempt to enqueue invalid transition request!");
        private void LogRequestPerformed(TransitionRequest<TStateType> request) => Debug.Log($"Performed transition request to {request.TargetState} created by {request.Source}.");
        private void LogTransitionDisposeRequested(TransitionRequest<TStateType> request) => Debug.Log($"Received dispose request for transition to {request?.TargetState} by {request?.Source}!");
        private void LogInvalidDisposeRequest(TransitionRequest<TStateType> request) => Debug.LogError($"Invalid dispose request {request?.TargetState} by {request?.Source}!");
        private void LogInvalidRequestSource() => Debug.LogError($"The source of transition request must not be null!");
        private void LogRequestDiscarded(TransitionRequest<TStateType> request) => Debug.Log($"Transition request to {request.TargetState} created by {request.Source} was discarded.");
        private void LogPriorityLock(TransitionRequest<TStateType> request, int lockPriority) => Debug.LogWarning($"State machine locked for any request with priority {lockPriority} or less.\nRequested transition to {request.TargetState} by {request.Source} has priority {request.Priority}.");
        private void LogRequestAdded(TransitionRequest<TStateType> request) => Debug.Log($"Enqueued request to {request.TargetState} that came from {request.Source}.");
        private void LogTransitionRequested(Type targetState, object source) => Debug.Log($"{source} requested transition to {targetState}.");
    }
}
