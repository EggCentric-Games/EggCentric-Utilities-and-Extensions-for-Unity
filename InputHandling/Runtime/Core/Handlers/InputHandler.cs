using UnityEngine.InputSystem;

namespace EggCentric.InputHandling
{
    public abstract class InputHandler<TInputAsset> : IInputHandler where TInputAsset : IInputActionCollection2, new()
    {
        protected readonly TInputAsset inputAsset;

        public InputHandler() => inputAsset = new TInputAsset();

        public virtual void Enable() => inputAsset.Enable();
        public virtual void Disable() => inputAsset.Disable();
    }
}