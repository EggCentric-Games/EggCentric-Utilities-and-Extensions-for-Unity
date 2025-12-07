using System;

namespace EggCentric.InputHandling
{
    public interface IActionInput
    {
        public event Action OnInputStarted;
        public event Action OnInputCanceled;
    }

    public interface IActionInput<TInputType> : IPayloadedInput<TInputType> where TInputType : struct
    {
        public event Action<TInputType> OnInputStarted;
        public event Action<TInputType> OnInputCanceled;
    }
}