using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

        public ITransitionBuilder To<TTarget>(object source) where TTarget : class, ICommonState, TStateType
        {
            return new TransitionBuilder<TTarget>(this, source);
        }

        public ITransitionBuilder To<TTarget, TPayload>(object source, TPayload payload) where TTarget : class, IPayloadedState<TPayload>, TStateType
        {
            return new PayloadTransitionBuilder<TTarget, TPayload>(this, source, payload);
        }

        public void DisposeRequest(TransitionRequest requestToDispose) => _pendingRequests.RemoveAll(x => x.request == requestToDispose);

        public void DisposeRequests(object source) => _pendingRequests.RemoveAll(x => x.request.Source == source);

        public void HandleRequests()
        {
            ProcessRequestsInQueue();
            CleanUpQueue();
        }

        private TransitionRequest AddRequest<TTarget>(object source, bool isForced = false) where TTarget : class, ICommonState, TStateType
        {
            UrgentRequest newRequest = new UrgentRequest(typeof(TTarget), source, isForced);
            var executor = CreateExecutor<TTarget>();

            EnqueueRequest(newRequest, executor);

            return newRequest;
        }

        private TransitionRequest AddRequest<TTarget, TPayload>(object source, TPayload payload, bool isForced = false) where TTarget : class, IPayloadedState<TPayload>, TStateType
        {
            UrgentRequest newRequest = new UrgentRequest(typeof(TTarget), source, isForced);
            var executor = CreateExecutor<TTarget, TPayload>(payload);

            EnqueueRequest(newRequest, executor);

            return newRequest;
        }

        private TransitionRequest AddRequest<TTarget>(object source, float lifetime = -1f) where TTarget : class, ICommonState, TStateType
        {
            DelayedRequest newRequest = new DelayedRequest(typeof(TTarget), source, lifetime);
            var executor = CreateExecutor<TTarget>();

            EnqueueRequest(newRequest, executor);

            return newRequest;
        }

        private TransitionRequest AddRequest<TTarget, TPayload>(object source, TPayload payload, float lifetime = -1f) where TTarget : class, IPayloadedState<TPayload>, TStateType
        {
            DelayedRequest newRequest = new DelayedRequest(typeof(TTarget), source, lifetime);
            var executor = CreateExecutor<TTarget, TPayload>(payload);

            EnqueueRequest(newRequest, executor);

            return newRequest;
        }

        private void EnqueueRequest(TransitionRequest request, Action<ITransition> executor)
        {
            _pendingRequests.Add((request, executor));
        }

        private void ProcessRequestsInQueue()
        {
            foreach (var requestPair in _pendingRequests.ToList())
            {
                if (!requestPair.request.IsValid)
                {
                    RemoveRequest(requestPair);
                    continue;
                }

                if (_transitionEvaluator.ResolveTransition(requestPair.request, out ITransition performedTransition))
                {
                    requestPair.executor(performedTransition);
                    RemoveRequest(requestPair);
                }
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
                else
                    Debug.LogError($"{x?.GetType()} isn't a valid type!");
            };

            return executor;
        }

        private Action<ITransition> CreateExecutor<TTarget, TPayload>(TPayload payload) where TTarget : class, IPayloadedState<TPayload>, TStateType
        {
            Action<ITransition> executor = x => {
                if (x is ITransition<TTarget> typedTransition)
                    _stateMachine.ExecuteTransition(typedTransition, payload);
                else
                    Debug.LogError($"{x?.GetType()} isn't a valid type!");
            };

            return executor;
        }

        private abstract class TransitionBuilderBase<TTarget> : ITransitionBuilder where TTarget : class, IState, TStateType
        {
            public object Source { get; private set; }
            public int Priority { get; private set; }
            public Type Target => typeof(TTarget);

            protected readonly RequestHandler<TStateType> requestHandler;

            public TransitionBuilderBase(RequestHandler<TStateType> requestHandler, object source)
            {
                this.requestHandler = requestHandler;
                Source = source;
            }

            public ITransitionBuilder WithPriority(int priority)
            {
                Priority = priority;

                return this;
            }

            public abstract TransitionRequest Now();
            public abstract TransitionRequest Forced();
            public abstract TransitionRequest AwaitFor(float lifetime);
        }

        private class TransitionBuilder<TTarget> : TransitionBuilderBase<TTarget> where TTarget : class, ICommonState, TStateType
        {
            public TransitionBuilder(RequestHandler<TStateType> requestHandler, object source) : base(requestHandler, source)
            {
            }

            public override TransitionRequest Now() => requestHandler.AddRequest<TTarget>(Source, false);

            public override TransitionRequest Forced() => requestHandler.AddRequest<TTarget>(Source, true);

            public override TransitionRequest AwaitFor(float lifetime) => requestHandler.AddRequest<TTarget>(Source, lifetime);
        }

        private class PayloadTransitionBuilder<TTarget, TPayload> : TransitionBuilderBase<TTarget> where TTarget : class, IPayloadedState<TPayload>, TStateType
        {
            private readonly TPayload _payload;

            public PayloadTransitionBuilder(RequestHandler<TStateType> requestHandler, object source, TPayload payload) : base(requestHandler, source)
            {
                _payload = payload;
            }

            public override TransitionRequest Now() => requestHandler.AddRequest<TTarget, TPayload>(Source, _payload, false);
            
            public override TransitionRequest Forced() => requestHandler.AddRequest<TTarget, TPayload>(Source, _payload, true);

            public override TransitionRequest AwaitFor(float lifetime) => requestHandler.AddRequest<TTarget, TPayload>(Source, _payload, lifetime);
        }
    }
}