using System;

namespace EggCentric.StateMachines
{
    public interface IStateMachine<TStateType> where TStateType : IState
    {
        public TStateType CurrentState { get; }
        public Type DefaultState { get; }
        public bool IsLocked { get; }

        public ITransitionBuilder To<TTarget>(object source) where TTarget : class, ICommonState, TStateType;
        public ITransitionBuilder To<TTarget, TPayload>(object source, TPayload payload) where TTarget : class, IPayloadedState<TPayload>, TStateType;

        public Guid RequestLock(object source, int priority = 0);
        public void DisposeLock(Guid lockId);
        public void DisposeLocks(object source);

        public void ExecuteTransition<TTarget>(ITransition<TTarget> transition) where TTarget : class, ICommonState, TStateType;
        public void ExecuteTransition<TTarget, TPayload>(ITransition<TTarget> transition, TPayload payload) where TTarget : class, IPayloadedState<TPayload>, TStateType;
        
        public bool IsFreeFor(int priority);
    }
}