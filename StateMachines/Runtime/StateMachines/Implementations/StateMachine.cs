using System;
using System.Collections.Generic;

namespace EggCentric.StateMachines
{
    public abstract class StateMachine<TStateType> : IStateMachine<TStateType> where TStateType : IState
    {
        public Type DefaultState => _defaultState;
        public TStateType CurrentState => _currentState;
        public bool IsLocked => _lockHandler.IsLocked;

        private TransitionEvaluator<TStateType> _transitionEvaluator;
        private RequestHandler<TStateType> _requestHandler;
        private LockHandler _lockHandler;

        private Dictionary<Type, IState> _registeredStates;
        private Type _defaultState;
        private bool _isInitialized;
        private TStateType _currentState;

        public StateMachine() => CreateFields();

        public ITransitionBuilder To<TTarget>(object source) where TTarget : class, ICommonState, TStateType => _requestHandler.To<TTarget>(source);
        
        public ITransitionBuilder To<TTarget, TPayload>(object source, TPayload payload) where TTarget : class, IPayloadedState<TPayload>, TStateType => _requestHandler.To<TTarget, TPayload>(source, payload);

        public void DisposeRequest(TransitionRequest requestToDispose) => _requestHandler.DisposeRequest(requestToDispose);

        public void DisposeRequests(object source) => _requestHandler.DisposeRequests(source);

        public Guid RequestLock(object source, int priority = 0) => _lockHandler.RequestLock(source, priority);

        public void DisposeLock(Guid lockId) => _lockHandler.DisposeLock(lockId);

        public void DisposeLocks(object source) => _lockHandler.DisposeLocks(source);

        public void ExecuteTransition<TTarget>(ITransition<TTarget> transition) where TTarget : class, ICommonState, TStateType => Enter<TTarget>();

        public void ExecuteTransition<TTarget, TPayload>(ITransition<TTarget> transition, TPayload payload) where TTarget : class, IPayloadedState<TPayload>, TStateType => Enter<TTarget, TPayload>(payload);

        public bool IsFreeFor(int priority) => !_lockHandler.IsLockedFor(priority);

        protected virtual void CreateFields()
        {
            _transitionEvaluator = new TransitionEvaluator<TStateType>(this);
            _requestHandler = new RequestHandler<TStateType>(this, _transitionEvaluator);
            _lockHandler = new LockHandler();

            _registeredStates = new Dictionary<Type, IState>();
        }

        protected virtual void Tick() => _requestHandler.HandleRequests();

        protected void Initialize<TDefaultState>() where TDefaultState : class, ICommonState, TStateType
        {
            if (_isInitialized)
                return;

            RegisterStates();
            RegisterTransitions();
            SetDefaultState<TDefaultState>();

            _isInitialized = true;
        }

        protected void Initialize<TDefaultState, TPayload>(TPayload payload) where TDefaultState : class, IPayloadedState<TPayload>, TStateType
        {
            if (_isInitialized)
                return;

            RegisterStates();
            RegisterTransitions();
            SetDefaultState<TDefaultState, TPayload>(payload);

            _isInitialized = true;
        }

        protected void RegisterState<TState>(TState state) where TState : class, IState, TStateType
        {
            _registeredStates.Add(typeof(TState), state);
            _transitionEvaluator.RegisterState<TState>();
        }

        protected Transition<TTarget> AddTransition<TSource, TTarget>() where TSource : class, IState, TStateType where TTarget : class, IState, TStateType
        {
            return _transitionEvaluator.AddTransition<TSource, TTarget>();
        }

        protected abstract void RegisterStates();
        protected abstract void RegisterTransitions();

        private void SetDefaultState<TDefaultState>() where TDefaultState : class, ICommonState, TStateType
        {
            _defaultState = typeof(TDefaultState);
            Enter<TDefaultState>();
        }

        private void SetDefaultState<TDefaultState, TPayload>(TPayload payload) where TDefaultState : class, IPayloadedState<TPayload>, TStateType
        {
            _defaultState = typeof(TDefaultState);
            Enter<TDefaultState, TPayload>(payload);
        }

        private void Enter<TState>() where TState : class, TStateType, ICommonState => ChangeState<TState>().Enter();

        private void Enter<TState, TPayload>(TPayload payload) where TState : class, TStateType, IPayloadedState<TPayload> => ChangeState<TState>().Enter(payload);

        private TState ChangeState<TState>() where TState : class, IState, TStateType
        {
            TState state = GetState<TState>();
            _currentState?.Exit();
            _currentState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IState
        {
            return _registeredStates[typeof(TState)] as TState;
        }
    }
}