namespace EggCentric.Abilities.Modifiers
{
    [System.Serializable]
    public abstract class AbilityModifierSettings
    {
        public abstract IAbilityModifier Create();
    }

    public abstract class AbilityModifierSettings<TModifier> : AbilityModifierSettings where TModifier : IAbilityModifier
    {
        public sealed override IAbilityModifier Create() => CreateInstance();
        protected abstract TModifier CreateInstance();
    }
}