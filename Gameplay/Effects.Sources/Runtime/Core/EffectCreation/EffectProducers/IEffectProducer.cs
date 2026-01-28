namespace EggCentric.Effects.Sources
{
    public interface IEffectProducer<in TContext>
    {
        public IEffect CreateWith(TContext effectSource);
    }
}
