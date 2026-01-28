using EggCentric.DI;
using EggCentric.Visitables;
using System.Collections.Generic;

namespace EggCentric.Effects
{
    public class Effect : IEffect
    {
        public IReadOnlyContainer Context { get; }
        public IEnumerable<IEffectComponentView> Components { get; }

        private readonly IReadOnlyList<IEffectComponent> _components;

        public Effect(IReadOnlyContainer context, IReadOnlyList<IEffectComponent> components)
        {
            Context = context;
            Components = components;
        }

        public bool Visit(IReception<IEffectComponent, IEffectReceiver> visitable)
        {
            foreach(var component in _components)
                visitable.Accept(component);

            return true;
        }
    }
}