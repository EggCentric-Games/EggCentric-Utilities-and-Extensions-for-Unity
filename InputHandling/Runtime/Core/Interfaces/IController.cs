namespace EggCentric.InputHandling
{
    public interface IController<TControllableEntity, TInputHandler> where TInputHandler : IInputHandler
    {
        public void BindFor(TInputHandler input);
    }
}
