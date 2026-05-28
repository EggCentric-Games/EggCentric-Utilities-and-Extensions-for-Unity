using EggCentric.NoiseGeneration.Modifiers;

namespace EggCentric.NoiseGeneration.Authoring.Modifiers
{
    public abstract class ModifierConfig<TModifier> : IModifierConfig where TModifier : IGenerationModifier
    {
        public IGenerationModifier CreateInstance() => CreateModifier();

        protected abstract TModifier CreateModifier();
    }
}
