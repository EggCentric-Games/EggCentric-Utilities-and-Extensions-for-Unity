using EggCentric.LifeCycleHandling;
using System;

namespace EggCentric.StateMachines
{
    public interface IStateMachine : ITickable
    {
        public bool IsInitialized { get; }
        public IState CurrentState { get; }
        public bool IsLocked { get; }
        public int LockLevel { get; }

        public Guid RequestLock(object source, int priority = 0);
        public void DisposeLock(Guid lockId);
        public void DisposeLocks(object source);

        public void DiscardFromSource(object source);
        public bool IsFreeFor(int priority);
    }

    public interface IStateMachine<TStateType> : IStateMachine, IStateMachineEventProvider<TStateType> where TStateType : IState
    {
        public new TStateType CurrentState { get; }
        IState IStateMachine.CurrentState => CurrentState;

        public void Initialize<TDefaultState>() where TDefaultState : class, IPlainState, TStateType;
        public void Initialize<TDefaultState, TPayload>(TPayload payload) where TDefaultState : class, IPayloadedState<TPayload>, TStateType;

        public ITransitionBuilder<TStateType> To<TTarget>(object source) where TTarget : class, IPlainState, TStateType;
        public ITransitionBuilder<TStateType> To<TTarget, TPayload>(object source, TPayload payload) where TTarget : class, IPayloadedState<TPayload>, TStateType;
        public void DiscardRequest(TransitionRequest<TStateType> requestToDispose);

        public void ExecuteTransition<TTarget>(ITransition<TTarget> transition) where TTarget : class, IPlainState, TStateType;
        public void ExecuteTransition<TTarget, TPayload>(ITransition<TTarget> transition, TPayload payload) where TTarget : class, IPayloadedState<TPayload>, TStateType;        
    }
}