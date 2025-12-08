namespace EggCentric.InputHandling
{
    public abstract class Controller<TControllableEntity, TInputHandler> : IController<TInputHandler> where TInputHandler : IInputHandler
    {
        protected readonly TControllableEntity controlledEntity;
        protected TInputHandler inputHandler;

        public Controller(TControllableEntity controlledEntity) => this.controlledEntity = controlledEntity;

        public abstract void BindFor(TInputHandler input);
    }
}
