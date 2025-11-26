namespace EggCentric.InputHandling
{
    public interface IPayloadedInput<TInputType> where TInputType : struct
    {
        public TInputType CurrentInput { get; }
    }
}