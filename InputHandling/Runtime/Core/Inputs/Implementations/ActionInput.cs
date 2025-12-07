using System;
using UnityEngine.InputSystem;

namespace EggCentric.InputHandling
{
    public class ActionInput : Input, IActionInput
    {
        public event Action OnInputStarted;
        public event Action OnInputCanceled;

        public ActionInput(InputAction observedAction) : base(observedAction)
        {
        }

        protected override void OnActionStarted(InputAction.CallbackContext context) => OnInputStarted?.Invoke();
        protected override void OnActionFinished(InputAction.CallbackContext context) => OnInputCanceled?.Invoke();
    }

    public class ActionInput<TInputType> : Input, IActionInput<TInputType> where TInputType : struct
    {
        public TInputType CurrentInput => observedAction.ReadValue<TInputType>();

        public event Action<TInputType> OnInputStarted;
        public event Action<TInputType> OnInputCanceled;

        public ActionInput(InputAction observedAction) : base(observedAction)
        {
        }

        protected override void OnActionStarted(InputAction.CallbackContext context) => OnInputStarted?.Invoke(CurrentInput);

        protected override void OnActionFinished(InputAction.CallbackContext context) => OnInputCanceled?.Invoke(default);
    }
}