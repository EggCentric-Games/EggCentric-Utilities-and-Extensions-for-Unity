using EggCentric.DI;
using System.Collections.Generic;

namespace EggCentric.Effects.Sources
{
    public abstract class EffectSource<TDetectionContext> : IEffectSource<TDetectionContext> where TDetectionContext : IDetectionContext
    {
        public IReadOnlyContainer Context => _context;
        public IReadOnlyList<IEffectLayer<TDetectionContext>> EffectLayers => _layers;

        private readonly IContainer _context;
        private readonly List<IEffectLayer<TDetectionContext>> _layers;

        public EffectSource(IReadOnlyContainer parentContext)
        {
            _context = ContainerFactory.CreateNew(parentContext);
            PopulateContext(ref _context);
            _layers = new List<IEffectLayer<TDetectionContext>>();
        }

        public EffectLayer<TDetectionContext> AddLayer()
        {
            var layer = new EffectLayer<TDetectionContext>();
            _layers.Add(layer);

            return layer;
        }

        public void AddLayer(IEffectLayer<TDetectionContext> effectLayer)
        {
            if (effectLayer == null)
                return;

            _layers.Add(effectLayer);
        }

        public void ApplyTo(IEffectGateway item)
        {
            var context = CreateContextFor(item);

            foreach (var layer in _layers)
                layer.ApplyTo(context);
        }

        public void ApplyTo(IEnumerable<IEffectGateway> items)
        {
            var contexts = CreateContextFor(items);

            foreach (var layer in _layers)
                layer.ApplyTo(contexts);
        }

        public IEnumerable<TDetectionContext> CreateContextFor(IEnumerable<IEffectGateway> entries)
        {
            foreach(var entry in entries)
                yield return CreateContextFor(entry);
        }

        public abstract TDetectionContext CreateContextFor(IEffectGateway entry);
        public abstract void PopulateContext(ref IContainer context);
    }
}