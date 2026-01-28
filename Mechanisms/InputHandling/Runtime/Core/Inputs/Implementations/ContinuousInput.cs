using EggCentric.LifeCycleHandling;
using System;
using UnityEngine.InputSystem;

namespace EggCentric.InputHandling
{

    public class ContinuousInput : ContinuousInputBase, IContinuousInput
    {
        public event Action OnInput;

        public ContinuousInput(ICoroutineRunner coroutineRunner, InputAction observedAction) : base(coroutineRunner, observedAction)
        {
        }

        protected override void SendInput() => OnInput?.Invoke();
        protected override void SendDefault() => OnInput?.Invoke();
    }

    public class ContinuousInput<TInputType> : ContinuousInputBase, IContinuousInput<TInputType> where TInputType : struct
    {
        public TInputType CurrentInput => observedAction.ReadValue<TInputType>();

        public event Action<TInputType> OnInput;

        public ContinuousInput(ICoroutineRunner coroutineRunner, InputAction observedAction) : base(coroutineRunner, observedAction)
        {
        }

        protected override void SendInput() => OnInput?.Invoke(CurrentInput);
        protected override void SendDefault() => OnInput?.Invoke(default);
    }
}