using System.Collections.Generic;
using System.Linq;

namespace EggCentric.Effects.Sources
{
    public class EffectAdaptersStack<TContext> : IEffectAdaptersStack<TContext>
    {
        public IEnumerable<IEffectView> EffectViews => Items.Select(x => x.EffectView);
        public IReadOnlyList<IEffectSpecsAdapter<TContext>> Items => _adapters;

        private readonly List<IEffectSpecsAdapter<TContext>> _adapters;

        public IEnumerable<IEffect> CreateEffects(TContext context)
        {
            foreach (var adapter in _adapters)
                yield return adapter.CreateWith(context);
        }

        public void Add(IEffectSpecsAdapter<TContext> item) => _adapters.Add(item);
        public bool Remove(IEffectSpecsAdapter<TContext> item) => _adapters.Remove(item);
        public void Clear() => _adapters.Clear();

    }
}
