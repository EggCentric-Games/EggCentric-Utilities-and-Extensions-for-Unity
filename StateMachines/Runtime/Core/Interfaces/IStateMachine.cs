using System;

namespace EggCentric.StateMachines
{
    public interface IStateMachine<TStateType> : IStateMachineEventProvider<TStateType> where TStateType : IState
    {
        public TStateType CurrentState { get; }
        public Type DefaultState { get; }
        public bool IsLocked { get; }

        public ITransitionBuilder<TStateType> To<TTarget>(object source) where TTarget : class, IPlainState, TStateType;
        public ITransitionBuilder<TStateType> To<TTarget, TPayload>(object source, TPayload payload) where TTarget : class, IPayloadedState<TPayload>, TStateType;
        public void DiscardRequest(TransitionRequest<TStateType> requestToDispose);
        public void DiscardFromSource(object source);

        public Guid RequestLock(object source, int priority = 0);
        public void DisposeLock(Guid lockId);
        public void DisposeLocks(object source);

        public void ExecuteTransition<TTarget>(ITransition<TTarget> transition) where TTarget : class, IPlainState, TStateType;
        public void ExecuteTransition<TTarget, TPayload>(ITransition<TTarget> transition, TPayload payload) where TTarget : class, IPayloadedState<TPayload>, TStateType;
        
        public bool IsFreeFor(int priority);
    }
}