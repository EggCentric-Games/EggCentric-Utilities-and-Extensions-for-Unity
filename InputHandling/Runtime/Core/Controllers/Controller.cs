namespace EggCentric.InputHandling
{
    public abstract class Controller<TControllableEntity, TInputHandler> : IController<TControllableEntity, TInputHandler> where TInputHandler : IInputHandler
    {
        protected readonly TControllableEntity controlledEntity;
        protected readonly TInputHandler inputHandler;

        public abstract void BindFor(TInputHandler input);
    }
}
