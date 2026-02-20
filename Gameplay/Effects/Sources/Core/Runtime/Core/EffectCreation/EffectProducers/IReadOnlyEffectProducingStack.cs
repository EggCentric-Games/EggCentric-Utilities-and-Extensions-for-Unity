using System.Collections.Generic;

namespace EggCentric.Effects.Sources
{
    public interface IReadOnlyEffectProducingStack<TContext> : IReadOnlyItemStack<IEffectProducer<TContext>>
    {
        public IReadOnlyList<IEffect> CreateFor(TContext context);
    }
}
