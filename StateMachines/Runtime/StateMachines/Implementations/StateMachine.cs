using EggCentric.StateMachines;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace EggCentric.StateMachines
{
    public abstract class StateMachine<TStateType> : IStateMachine<TStateType>
    {
        protected Dictionary<Type, IState> registeredStates;
        protected Dictionary<Type, List<ITransition>> transitions;

        protected IState currentState;

        public StateMachine()
        {
            registeredStates = new Dictionary<Type, IState>();
            transitions = new Dictionary<Type, List<ITransition>>();
        }

        public void Enter<TState>() where TState : class, TStateType, ICommonState
        {
            ChangeState<TState>().Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, TStateType, IPayloadedState<TPayload>
        {
            ChangeState<TState>().Enter(payload);
        }

        protected void RegisterState<TState>(TState state) where TState : IState
        { 
            registeredStates.Add(typeof(TState), state);
            transitions.Add(typeof(TState), new List<ITransition>());
        }


        protected Transition AddTransition<TSource>() where TSource : class, IState
        {
            if(!transitions.TryGetValue(typeof(TSource), out List<ITransition> availableTransitions))
            {
                Debug.LogError($"There is no registered states of type {typeof(TSource)}!");
                return null;
            }

            Transition newTransition = new Transition();
            availableTransitions.Add(newTransition);

            return newTransition;
        }

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