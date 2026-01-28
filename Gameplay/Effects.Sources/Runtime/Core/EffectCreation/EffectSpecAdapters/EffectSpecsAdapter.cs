using System;

namespace EggCentric.Effects.Sources
{
    public class EffectSpecsAdapter<TContext, TData> : IEffectSpecsAdapter<TContext>
    {
        public IEffectView EffectView => _specs;

        private readonly Converter<TContext, TData> _converter;
        private readonly IEffectSpecs<TData> _specs;

        public EffectSpecsAdapter(Converter<TContext, TData> converter, IEffectSpecs<TData> specs)
        {
            _converter = converter;
            _specs = specs;
        }

        public IEffect CreateWith(TContext context)
        {
            var data = _converter.Invoke(context);
            var instance = _specs.CreateInstance(data);
            
            return instance;
        }
    }
}
