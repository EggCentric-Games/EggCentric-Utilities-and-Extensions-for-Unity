using System;

namespace EggCentric.StateMachines
{
    public interface IRequestHandlerEventsProvider<TStateType> where TStateType : IState
    {
        public event Action<Type, object> OnTransitionRequested;
        public event Action<TransitionRequest<TStateType>> OnTransitionDisposeRequested;
        public event Action<TransitionRequest<TStateType>> OnRequestAdded;
        public event Action<TransitionRequest<TStateType>> OnRequestPerformed;
        public event Action<TransitionRequest<TStateType>> OnRequestDiscarded;

        public event Action<TransitionRequest<TStateType>, int> OnPriorityLock;

        public event Action OnInvalidEnqueueRequest;
        public event Action<TransitionRequest<TStateType>> OnInvalidDisposeRequest;
        public event Action OnInvalidRequestSource;
    }
}