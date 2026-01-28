namespace EggCentric.InputHandling
{
    public interface IController<TInputHandler> where TInputHandler : IInputHandler
    {
        public void BindFor(TInputHandler input);
    }
}
