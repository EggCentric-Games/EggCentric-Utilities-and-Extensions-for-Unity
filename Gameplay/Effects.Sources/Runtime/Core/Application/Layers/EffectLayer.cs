using EggCentric.CollectionProcessing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EggCentric.Effects.Sources
{
    public class EffectLayer<TDetectionContext> : IEffectLayer<TDetectionContext> where TDetectionContext : IDetectionContext
    {
        public IReadOnlyEffectAdaptersStack<IEffectApplicationContext<TDetectionContext>> Producers => _producers;
        public IEffectApplicationFilter ApplicationFilter => _applicationFilter;

        private readonly TargetingPipeline<IEffectApplicationContext<TDetectionContext>> _targetingPipeline;
        private readonly IEffectAdaptersStack<IEffectApplicationContext<TDetectionContext>> _producers;

        private IEffectExecutionPolicy<TDetectionContext> _executionPolicy;
        private IEffectApplicationFilter _applicationFilter;

        public EffectLayer()
        {
            _targetingPipeline = new TargetingPipeline<IEffectApplicationContext<TDetectionContext>>();
            _producers = new EffectAdaptersStack<IEffectApplicationContext<TDetectionContext>>();
        }

        public EffectLayer<TDetectionContext> WithPolicy(IEffectExecutionPolicy<TDetectionContext> policy)
        {
            _executionPolicy = policy;
            return this;
        }

        public EffectLayer<TDetectionContext> WithFilter(IEffectApplicationFilter filter)
        {
            _applicationFilter = filter;
            return this;
        }

        public EffectLayer<TDetectionContext> WithReducer(ICollectionProcessor<IEffectApplicationContext<TDetectionContext>> reducer)
        {
            _targetingPipeline.WithReducer(reducer);
            return this;
        }

        public EffectLayer<TDetectionContext> WithSelector(IItemSelector<IEffectApplicationContext<TDetectionContext>> selector)
        {
            _targetingPipeline.WithSelector(selector);
            return this;
        }

        public EffectLayer<TDetectionContext> AddEffect(IEffectSpecsAdapter<IEffectApplicationContext<TDetectionContext>> adapter)
        {
            _producers.Add(adapter);
            return this;
        }

        public EffectLayer<TDetectionContext> AddEffect<TData>(Converter<IEffectApplicationContext<TDetectionContext>, TData> converter, IEffectSpecs<TData> specs)
        {
            _producers.Add(new EffectSpecsAdapter<IEffectApplicationContext<TDetectionContext>, TData>(converter, specs));
            return this;
        }

        public void ApplyTo(TDetectionContext item) => ApplyTo(new TDetectionContext[] { item });
        public void ApplyTo(IEnumerable<TDetectionContext> items)
        { 
            var validated = items.Where(x => _executionPolicy.ValidateTarget(x));
            validated = validated.Where(x => _applicationFilter.ValidateFor(x.Target, _producers.EffectViews));

            var layerContext = CreateLayerContext(validated);
            var applicationContexts = CreateApplicationContexts(layerContext, validated);
            var targets = _targetingPipeline.Process(applicationContexts);

            foreach (var target in targets)
                ExecuteRequest(target);
        }

        private void ExecuteRequest(IEffectApplicationContext<TDetectionContext> aplication)
        {
            var effects = _producers.CreateEffects(aplication);

            foreach (var effect in effects)
                aplication.DetectionContext.Target.Accept(effect);

            _executionPolicy.HandleRequestExecution(aplication);
        }

        private IEffectLayerContext CreateLayerContext(IEnumerable<TDetectionContext> detectionContexts)
        {
            var layerContext = new EffectLayerContext();
            layerContext.TargetCount = detectionContexts.Count();

            return layerContext;
        }

        private IEnumerable<IEffectApplicationContext<TDetectionContext>> CreateApplicationContexts(IEffectLayerContext layerContext, IEnumerable<TDetectionContext> detectionContexts)
        {
            foreach (var detectionContext in detectionContexts)
                yield return new EffectApplicationContext<TDetectionContext>(layerContext, detectionContext);
        }
    }
}