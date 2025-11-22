using System;
using System.Collections.Generic;

namespace EggCentric.StateMachines
{
    public abstract class StateMachine<TStateType> : IStateMachine<TStateType>
    {
        protected Dictionary<Type, IState> registeredStates;
        protected IState currentState;

        public StateMachine()
        {
            registeredStates = new Dictionary<Type, IState>();
        }

        public void Enter<TState>() where TState : class, TStateType, ICommonState
        {
            ChangeState<TState>().Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, TStateType, IPayloadedState<TPayload>
        {
            ChangeState<TState>().Enter(payload);
        }

        protected void RegisterState<TState>(TState state) where TState : IState =>
            registeredStates.Add(typeof(TState), state);

        protected TState ChangeState<TState>() where TState : class, IState
        {
            TState state = GetState<TState>();
            currentState?.Exit();
            currentState = state;

            return state;
        }

        protected TState GetState<TState>() where TState : class, IState
        {
            return registeredStates[typeof(TState)] as TState;
        }
    }
}