namespace EggCentric.Abilities.Modifiers
{
    public interface IAbilityModifier
    {
        public bool CanExecute { get; }

        public void BeforeExecution();
        public void AfterExecution();
        public void OnInterruption();

        public void Tick(float timeStep);
    }
}