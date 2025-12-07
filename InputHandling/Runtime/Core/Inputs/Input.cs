using UnityEngine.InputSystem;

namespace EggCentric.InputHandling
{
    public abstract class Input
    {
        protected readonly InputAction observedAction;

        public Input(InputAction observedAction)
        {
            this.observedAction = observedAction;
        }

        public void Enable()
        {
            observedAction.started += OnActionStarted;
            observedAction.canceled += OnActionFinished;
        }

        public void Disable()
        {
            observedAction.started -= OnActionStarted;
            observedAction.canceled -= OnActionFinished;
        }

        protected abstract void OnActionStarted(InputAction.CallbackContext context);
        protected abstract void OnActionFinished(InputAction.CallbackContext context);
    }
}