using EggCentric.DI;
using System;

namespace EggCentric.Effects
{
    public abstract class EffectComponentSpecs<TData, TComponent> : IEffectComponentSpecs<TData> where TComponent : IEffectComponent
    {
        public IReadOnlyContainer Context => _context;
        private readonly IContainer _context;
        private Func<IReadOnlyContainer, TData, TComponent> _producer;

        public EffectComponentSpecs(IReadOnlyContainer parentContext, Func<IReadOnlyContainer, TData, TComponent> producer)
        {
            _context = ContainerFactory.CreateNew(parentContext);
            _producer = producer;

            PopulateContext(ref _context);
        }

        public IEffectComponent CreateInstance(TData data) => _producer.Invoke(Context, data);

        protected abstract void PopulateContext(ref IContainer context);
    }
}
