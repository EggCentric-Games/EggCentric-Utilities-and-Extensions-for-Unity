using System;
using System.Collections.Generic;
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

        public ITransitionBuilder To<TTarget>() where TTarget : class, ICommonState, TStateType
        {
            return new TransitionBuilder<TTarget>(this);
        }

        public ITransitionBuilder To<TTarget, TPayload>(TPayload payload) where TTarget : class, IPayloadedState<TPayload>, TStateType
        {
            return new PayloadTransitionBuilder<TTarget, TPayload>(this, payload);
        }

        private void AddRequest<TTarget>(bool isForced = false) where TTarget : class, ICommonState, TStateType
        {
            UrgentRequest newRequest = new UrgentRequest(typeof(TTarget), isForced);
            var executor = CreateExecutor<TTarget>();

            EnqueueRequest(newRequest, executor);
        }

        private void AddRequest<TTarget, TPayload>(TPayload payload, bool isForced = false) where TTarget : class, IPayloadedState<TPayload>, TStateType
        {
            UrgentRequest newRequest = new UrgentRequest(typeof(TTarget), isForced);
            var executor = CreateExecutor<TTarget, TPayload>(payload);

            EnqueueRequest(newRequest, executor);
        }

        private void AddRequest<TTarget>(float lifetime = -1f) where TTarget : class, ICommonState, TStateType
        {
            DelayedRequest newRequest = new DelayedRequest(typeof(TTarget), lifetime);
            var executor = CreateExecutor<TTarget>();

            EnqueueRequest(newRequest, executor);
        }

        private void AddRequest<TTarget, TPayload>(TPayload payload, float lifetime = -1f) where TTarget : class, IPayloadedState<TPayload>, TStateType
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
                else
                    Debug.LogError($"{x.GetType()} isn't a valid type!");
            };

            return executor;
        }

        private Action<ITransition> CreateExecutor<TTarget, TPayload>(TPayload payload) where TTarget : class, IPayloadedState<TPayload>, TStateType
        {
            Action<ITransition> executor = x => {
                if (x is ITransition<TTarget> typedTransition)
                    _stateMachine.ExecuteTransition(typedTransition, payload);
                else
                    Debug.LogError($"{x.GetType()} isn't a valid type!");
            };

            return executor;
        }

        private abstract class TransitionBuilderBase<TTarget> : ITransitionBuilder where TTarget : class, IState, TStateType
        {
            public int Priority { get; private set; }
            public Type Target => typeof(TTarget);

            protected readonly RequestHandler<TStateType> requestHandler;

            public TransitionBuilderBase(RequestHandler<TStateType> requestHandler)
            {
                this.requestHandler = requestHandler;
            }

            public ITransitionBuilder WithPriority(int priority)
            {
                Priority = priority;

                return this;
            }

            public abstract void Now();
            public abstract void Forced();
            public abstract void AwaitFor(float lifetime);
        }

        private class TransitionBuilder<TTarget> : TransitionBuilderBase<TTarget> where TTarget : class, ICommonState, TStateType
        {
            public TransitionBuilder(RequestHandler<TStateType> requestHandler) : base(requestHandler)
            {
            }

            public override void Now() => requestHandler.AddRequest<TTarget>(false);

            public override void Forced() => requestHandler.AddRequest<TTarget>(true);

            public override void AwaitFor(float lifetime) => requestHandler.AddRequest<TTarget>(lifetime);
        }

        private class PayloadTransitionBuilder<TTarget, TPayload> : TransitionBuilderBase<TTarget> where TTarget : class, IPayloadedState<TPayload>, TStateType
        {
            private readonly TPayload _payload;

            public PayloadTransitionBuilder(RequestHandler<TStateType> requestHandler, TPayload payload) : base(requestHandler)
            {
                _payload = payload;
            }

            public override void Now() => requestHandler.AddRequest<TTarget, TPayload>(_payload, false);
            
            public override void Forced() => requestHandler.AddRequest<TTarget, TPayload>(_payload, true);

            public override void AwaitFor(float lifetime) => requestHandler.AddRequest<TTarget, TPayload>(_payload, lifetime);
        }
    }
}