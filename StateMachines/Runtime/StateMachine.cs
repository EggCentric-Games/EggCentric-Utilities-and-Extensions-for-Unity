using System;
using System.Collections.Generic;

public abstract class StateMachine<TStateType> : IStateMachine<TStateType>
{
    protected Dictionary<Type, IExitableState> registeredStates;
    protected IExitableState currentState;

    public StateMachine()
    {
        registeredStates = new Dictionary<Type, IExitableState>();
    }

    public void Enter<TState>() where TState : class, TStateType, IState
    {
        ChangeState<TState>().Enter();
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState : class, TStateType, IPayloadedState<TPayload>
    {
        ChangeState<TState>().Enter(payload);
    }

    protected void RegisterState<TState>(TState state) where TState : IExitableState =>
        registeredStates.Add(typeof(TState), state);

    protected TState ChangeState<TState>() where TState : class, IExitableState
    {
        TState state = GetState<TState>();
        currentState?.Exit();
        currentState = state;

        return state;
    }

    protected TState GetState<TState>() where TState : class, IExitableState
    {
        return registeredStates[typeof(TState)] as TState;
    }
}