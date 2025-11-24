using System;
using System.Collections.Generic;

namespace EggCentric.StateMachines
{
    public abstract class StateMachine<TStateType> : IStateMachine<TStateType> where TStateType : IState
    {
        public TStateType CurrentState => _currentState;
        public bool IsLocked => _lockHandler.IsLocked;

        private TransitionEvaluator<TStateType> _transitionEvaluator;
        private RequestHandler<TStateType> _requestHandler;
        private LockHandler _lockHandler;

        private Dictionary<Type, IState> _registeredStates;
        private TStateType _currentState;
        private bool _isInitialized;


        public StateMachine() => CreateFields();
        
        public void Initialize()
        {
            if (_isInitialized)
                return;

            RegisterStates();
            RegisterTransitions();
            SetDefaultState();

            _isInitialized = true;
        }

        public ITransitionBuilder To<TTarget>() where TTarget : class, ICommonState, TStateType => _requestHandler.To<TTarget>();
        
        public ITransitionBuilder To<TTarget, TPayload>(TPayload payload) where TTarget : class, IPayloadedState<TPayload>, TStateType => _requestHandler.To<TTarget, TPayload>(payload);
        
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

        protected void Enter<TState>() where TState : class, TStateType, ICommonState => ChangeState<TState>().Enter();

        protected void Enter<TState, TPayload>(TPayload payload) where TState : class, TStateType, IPayloadedState<TPayload> => ChangeState<TState>().Enter(payload);

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
        protected abstract void SetDefaultState();

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