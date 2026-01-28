namespace EggCentric.Effects.Sources
{
    public interface IEffectSpecsAdapter<TContext> : IEffectProducer<TContext>
    {
        public IEffectView EffectView { get; }
    }
}
