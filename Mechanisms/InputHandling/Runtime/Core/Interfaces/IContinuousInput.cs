using System;

namespace EggCentric.InputHandling
{
    public interface IContinuousInput
    {
        public event Action OnInput;
    }

    public interface IContinuousInput<TInputType> : IPayloadedInput<TInputType> where TInputType : struct
    {
        public event Action<TInputType> OnInput;
    }
}