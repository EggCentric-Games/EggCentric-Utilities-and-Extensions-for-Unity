using System.Collections.Generic;
using System.Linq;

namespace EggCentric.Effects.Sources
{
    public class EffectProducingStack<TContext> : IEffectProducingStack<TContext>
    {
        public IReadOnlyList<IEffectProducer<TContext>> Items => _producers;

        private readonly List<IEffectProducer<TContext>> _producers;

        public EffectProducingStack() => _producers = new List<IEffectProducer<TContext>>();
        public EffectProducingStack(IEnumerable<IEffectProducer<TContext>> producers) => _producers = producers.ToList();

        public void Add(IEffectProducer<TContext> item) => _producers.Add(item);
        public bool Remove(IEffectProducer<TContext> item) => _producers.Remove(item);
        public void Clear() => _producers.Clear();

        public IReadOnlyList<IEffect> CreateFor(TContext context)
        {
            int effectCount = _producers.Count;
            IEffect[] effects = new IEffect[effectCount];

            for (int i = 0; i < effectCount; i++)
                effects[i] = _producers[i].CreateWith(context);

            return effects;
        }
    }
}
