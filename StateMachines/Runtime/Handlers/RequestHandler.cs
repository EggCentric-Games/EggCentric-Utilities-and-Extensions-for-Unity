using System;
using System.Collections.Generic;
using System.Linq;

namespace EggCentric.StateMachines
{

    public class RequestHandler<TStateType> : IRequestHandlerEventsProvider<TStateType> where TStateType : IState
    {
        private IStateMachine<TStateType> _stateMachine;
        private TransitionEvaluator<TStateType> _transitionEvaluator;

        private List<TransitionRequest<TStateType>> _pendingRequests;

        public event Action<Type, object> OnTransitionRequested;
        public event Action<TransitionRequest<TStateType>> OnTransitionDisposeRequested;
        public event Action<TransitionRequest<TStateType>> OnRequestAdded;
        public event Action<TransitionRequest<TStateType>> OnRequestPerformed;
        public event Action<TransitionRequest<TStateType>> OnRequestDiscarded;
        public event Action<TransitionRequest<TStateType>, int> OnPriorityLock;
        public event Action OnInvalidEnqueueRequest;
        public event Action<TransitionRequest<TStateType>> OnInvalidDisposeRequest;
        public event Action OnInvalidRequestSource;

        public RequestHandler(IStateMachine<TStateType> stateMachine, TransitionEvaluator<TStateType> transitionEvaluator)
        {
            _pendingRequests = new List<TransitionRequest<TStateType>>();

            _stateMachine = stateMachine;
            _transitionEvaluator = transitionEvaluator;
        }

        public ITransitionBuilder<TStateType> To<TTarget>(object source) where TTarget : class, IPlainState, TStateType
        {
            if (!HandleRequestSource(source))
                return null;

            OnTransitionRequested?.Invoke(typeof(TTarget), source);
            return new TransitionBuilder<TTarget>(this, source);
        }

        public ITransitionBuilder<TStateType> To<TTarget, TPayload>(object source, TPayload payload) where TTarget : class, IPayloadedState<TPayload>, TStateType
        {
            if (!HandleRequestSource(source))
                return null;

            OnTransitionRequested?.Invoke(typeof(TTarget), source);
            return new PayloadTransitionBuilder<TTarget, TPayload>(this, source, payload);
        }

        public void DiscardRequest(TransitionRequest<TStateType> requestToDispose)
        {
            OnTransitionDisposeRequested(requestToDispose);

            if (requestToDispose == null)
            {
                OnInvalidDisposeRequest?.Invoke(requestToDispose);
                return;
            }

            if(!_pendingRequests.Contains(requestToDispose))
            {
                OnInvalidDisposeRequest?.Invoke(requestToDispose);
                return;
            }

            DisposeRequest(requestToDispose);
        }

        public void DiscardFromSource(object source)
        {
            if (!HandleRequestSource(source))
                return;

            var toDiscard = _pendingRequests.Where(x => x.Source == source).ToList();

            foreach (var request in toDiscard)
                DiscardRequest(request);
        }

        public void HandleRequests() => ProcessRequestsInQueue();

        private TransitionRequest<TStateType> AddRequest<TTarget>(object source, int priority = 0, bool isForced = false) where TTarget : class, IPlainState, TStateType
        {
            if (!HandleRequestSource(source))
                return null;

            IExecutionPolicy executionPolicy = new UrgentPolicy(isForced);
            TransitionRequest<TStateType> newRequest = new PlainRequest<TStateType, TTarget>(source, executionPolicy, priority);

            EnqueueRequest(newRequest);

            return newRequest;
        }

        private TransitionRequest<TStateType> AddRequest<TTarget, TPayload>(object source, TPayload payload, int priority = 0, bool isForced = false) where TTarget : class, IPayloadedState<TPayload>, TStateType
        {
            if (!HandleRequestSource(source))
                return null;

            IExecutionPolicy executionPolicy = new UrgentPolicy(isForced);
            TransitionRequest<TStateType> newRequest = new PayloadedRequest<TStateType, TTarget, TPayload>(source, payload, executionPolicy, priority);

            EnqueueRequest(newRequest);

            return newRequest;
        }

        private TransitionRequest<TStateType> AddRequest<TTarget>(object source, int priority = 0, float lifetime = -1f) where TTarget : class, IPlainState, TStateType
        {
            if (!HandleRequestSource(source))
                return null;

            IExecutionPolicy executionPolicy = new DelayedPolicy(lifetime);
            TransitionRequest<TStateType> newRequest = new PlainRequest<TStateType, TTarget>(source, executionPolicy, priority);

            EnqueueRequest(newRequest);

            return newRequest;
        }

        private TransitionRequest<TStateType> AddRequest<TTarget, TPayload>(object source, TPayload payload, int priority = 0, float lifetime = -1f) where TTarget : class, IPayloadedState<TPayload>, TStateType
        {
            if (!HandleRequestSource(source))
                return null;

            IExecutionPolicy executionPolicy = new DelayedPolicy(lifetime);
            TransitionRequest<TStateType> newRequest = new PayloadedRequest<TStateType, TTarget, TPayload>(source, payload, executionPolicy, priority);

            EnqueueRequest(newRequest);

            return newRequest;
        }

        private void EnqueueRequest(TransitionRequest<TStateType> request)
        {
            if (request == null)
            {
                OnInvalidEnqueueRequest?.Invoke();
                return;
            }

            request.InitializeExecutor(_stateMachine);
            request.InitializeResolver(_transitionEvaluator);

            _pendingRequests.Add(request);
            OnRequestAdded?.Invoke(request);
        }

        private void ProcessRequestsInQueue()
        {
            foreach (var request in _pendingRequests.ToList())
            {
                if (!request.IsValid)
                {
                    DisposeRequest(request);
                    continue;
                }

                if (!IsRequestPerformable(request))
                {
                    OnPriorityLock?.Invoke(request, _stateMachine.LockLevel);
                    continue;
                }

                if(request.TryToPerform())
                {
                    OnRequestPerformed?.Invoke(request);
                    DisposeRequest(request);
                }
            }
        }

        private void DisposeRequest(TransitionRequest<TStateType> request)
        {
            if (request == null)
            {
                OnInvalidDisposeRequest?.Invoke(request);
                return;
            }

            _pendingRequests.Remove(request);
            OnRequestDiscarded?.Invoke(request);
        }

        private bool IsRequestPerformable(TransitionRequest<TStateType> request) => request.Flags.HasFlag(TransitionFlags.IgnoreLocks) || _stateMachine.IsFreeFor(request.Priority);

        private bool HandleRequestSource(object source)
        {
            if (source == null)
            {
                OnInvalidRequestSource?.Invoke();
                return false;
            }

            return true;
        }

        private abstract class TransitionBuilderBase<TTarget> : ITransitionBuilder<TStateType> where TTarget : class, IState, TStateType
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

            public ITransitionBuilder<TStateType> WithPriority(int priority)
            {
                Priority = priority;

                return this;
            }

            public abstract TransitionRequest<TStateType> Now();
            public abstract TransitionRequest<TStateType> Forced();
            public abstract TransitionRequest<TStateType> AwaitFor(float lifetime);
        }

        private class TransitionBuilder<TTarget> : TransitionBuilderBase<TTarget> where TTarget : class, IPlainState, TStateType
        {
            public TransitionBuilder(RequestHandler<TStateType> requestHandler, object source) : base(requestHandler, source)
            {
            }

            public override TransitionRequest<TStateType> Now() => requestHandler.AddRequest<TTarget>(Source, Priority, false);

            public override TransitionRequest<TStateType> Forced() => requestHandler.AddRequest<TTarget>(Source, Priority, true);

            public override TransitionRequest<TStateType> AwaitFor(float lifetime) => requestHandler.AddRequest<TTarget>(Source, Priority, lifetime);
        }

        private class PayloadTransitionBuilder<TTarget, TPayload> : TransitionBuilderBase<TTarget> where TTarget : class, IPayloadedState<TPayload>, TStateType
        {
            private readonly TPayload _payload;

            public PayloadTransitionBuilder(RequestHandler<TStateType> requestHandler, object source, TPayload payload) : base(requestHandler, source)
            {
                _payload = payload;
            }

            public override TransitionRequest<TStateType> Now() => requestHandler.AddRequest<TTarget, TPayload>(Source, _payload, Priority, false);
            
            public override TransitionRequest<TStateType> Forced() => requestHandler.AddRequest<TTarget, TPayload>(Source, _payload, Priority, true);

            public override TransitionRequest<TStateType> AwaitFor(float lifetime) => requestHandler.AddRequest<TTarget, TPayload>(Source, _payload, Priority, lifetime);
        }
    }
}