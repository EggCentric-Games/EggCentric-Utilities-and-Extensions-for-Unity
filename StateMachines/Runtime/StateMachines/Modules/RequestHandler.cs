using System;
using System.Collections.Generic;

namespace EggCentric.StateMachines
{
    public class RequestHandler<TStateType> where TStateType : IState
    {
        private IStateMachine<TStateType> _stateMachine;
        private TransitionEvaluator<TStateType> _transitionEvaluator;

        private List<(TransitionRequest request, Action<ITransition> executor)> _pendingRequests;
        private List<(TransitionRequest, Action<ITransition>)> _rejectQueue;

        public RequestHandler(IStateMachine<TStateType> stateMachine, TransitionEvaluator<TStateType> transitionEvaluator)
        {
            _pendingRequests = new List<(TransitionRequest, Action<ITransition>)>();
            _rejectQueue = new List<(TransitionRequest, Action<ITransition>)>();

            _stateMachine = stateMachine;
            _transitionEvaluator = transitionEvaluator;
        }

        public void AddRequest<TTarget>(bool isForced = false) where TTarget : class, ICommonState, TStateType
        {
            UrgentRequest newRequest = new UrgentRequest(typeof(TTarget), isForced);
            var executor = CreateExecutor<TTarget>();

            EnqueueRequest(newRequest, executor);
        }

        public void AddRequest<TTarget, TPayload>(TPayload payload, bool isForced = false) where TTarget : class, IPayloadedState<TPayload>, TStateType
        {
            UrgentRequest newRequest = new UrgentRequest(typeof(TTarget), isForced);
            var executor = CreateExecutor<TTarget, TPayload>(payload);

            EnqueueRequest(newRequest, executor);
        }

        public void AddDelayedRequest<TTarget>(float lifetime = -1f) where TTarget : class, ICommonState, TStateType
        {
            DelayedRequest newRequest = new DelayedRequest(typeof(TTarget), lifetime);
            var executor = CreateExecutor<TTarget>();

            EnqueueRequest(newRequest, executor);
        }

        public void AddDelayedRequest<TTarget, TPayload>(TPayload payload, float lifetime = -1f) where TTarget : class, IPayloadedState<TPayload>, TStateType
        {
            DelayedRequest newRequest = new DelayedRequest(typeof(TTarget), lifetime);
            var executor = CreateExecutor<TTarget, TPayload>(payload);

            EnqueueRequest(newRequest, executor);
        }

        public void HandleRequests()
        {
            ProcessRequestsInQueue();
            CleanUpQueue();
        }

        private void EnqueueRequest(TransitionRequest request, Action<ITransition> executor)
        {
            _pendingRequests.Add((request, executor));
        }

        private void ProcessRequestsInQueue()
        {
            foreach (var requestPair in _pendingRequests)
            {
                if (!requestPair.request.IsValid)
                {
                    RemoveRequest(requestPair);
                    continue;
                }

                _transitionEvaluator.ResolveTransition(requestPair.request, out ITransition performedTransition);
                requestPair.executor(performedTransition);
                RemoveRequest(requestPair);
            }
        }

        private void CleanUpQueue()
        {
            foreach (var requestPair in _rejectQueue)
                DiscardRequest(requestPair);

            _rejectQueue.Clear();
        }

        private void RemoveRequest((TransitionRequest, Action<ITransition>) requestPair)
        {
            _rejectQueue.Add(requestPair);
        }

        private void DiscardRequest((TransitionRequest, Action<ITransition>) requestPair)
        {
            _pendingRequests.Remove(requestPair);
        }

        private Action<ITransition> CreateExecutor<TTarget>() where TTarget : class, ICommonState, TStateType
        {
            Action<ITransition> executor = x => {
                if (x is ITransition<TTarget> typedTransition)
                    _stateMachine.ExecuteTransition(typedTransition);
            };

            return executor;
        }

        private Action<ITransition> CreateExecutor<TTarget, TPayload>(TPayload payload) where TTarget : class, IPayloadedState<TPayload>, TStateType
        {
            Action<ITransition> executor = x => {
                if (x is ITransition<TTarget> typedTransition)
                    _stateMachine.ExecuteTransition(typedTransition, payload);
            };

            return executor;
        }
    }
}
