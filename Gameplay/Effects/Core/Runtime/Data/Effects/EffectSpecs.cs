using EggCentric.DI;
using System.Collections.Generic;
using System.Linq;

namespace EggCentric.Effects
{
    public abstract class EffectSpecs<TData> : IEffectSpecs<TData>
    {
        public IReadOnlyContainer Context => _context;
        public IEnumerable<IEffectComponentSpecs<TData>> ComponentSpecs => _componentSpecs;

        public IEnumerable<IEffectComponentView> Components => ComponentSpecs;

        private readonly IContainer _context;
        private readonly List<IEffectComponentSpecs<TData>> _componentSpecs;


        public EffectSpecs(IReadOnlyContainer parentContext)
        {
            _context = ContainerFactory.CreateNew(parentContext);
            PopulateContext(ref _context);
            RegisterComponents();
        }

        public IEffect CreateInstance(TData context)
        {
            var components = MaterializeComponents(context).ToList();
            return new Effect(Context, components);
        }

        private IEnumerable<IEffectComponent> MaterializeComponents(TData data)
        {
            foreach (var componentSpec in ComponentSpecs)
                yield return componentSpec.CreateInstance(data);
        }

        protected void RegisterComponent(IEffectComponentSpecs<TData> component) => _componentSpecs.Add(component);

        protected abstract void PopulateContext(ref IContainer context);
        protected abstract void RegisterComponents();
    }
}
