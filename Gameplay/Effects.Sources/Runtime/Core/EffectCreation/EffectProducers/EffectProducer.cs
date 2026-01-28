using System;

namespace EggCentric.Effects.Sources
{
    public class EffectProducer<TContext> : IEffectProducer<TContext>
    {
        private readonly Func<TContext, IEffect> _getter;

        public EffectProducer(Func<TContext, IEffect> getter) => _getter = getter;

        public IEffect CreateWith(TContext effectSource) => _getter(effectSource);
    }
}
