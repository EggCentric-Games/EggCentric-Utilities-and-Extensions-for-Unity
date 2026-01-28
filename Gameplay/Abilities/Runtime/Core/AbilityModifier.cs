namespace EggCentric.Abilities.Modifiers
{
    public abstract class AbilityModifier : IAbilityModifier
    {
        public abstract bool CanExecute { get; }

        public virtual void BeforeExecution() { }

        public virtual void AfterExecution() { }

        public virtual void OnInterruption() { }

        public virtual void Tick(float timeStep) { }
    }
}