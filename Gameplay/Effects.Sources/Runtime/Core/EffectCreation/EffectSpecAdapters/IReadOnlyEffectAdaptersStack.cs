using System.Collections.Generic;

namespace EggCentric.Effects.Sources
{
    public interface IReadOnlyEffectAdaptersStack<TContext> : IReadOnlyItemStack<IEffectSpecsAdapter<TContext>>
    {
        public IEnumerable<IEffectView> EffectViews { get; }

        public IEnumerable<IEffect> CreateEffects(TContext context);
    }
}
