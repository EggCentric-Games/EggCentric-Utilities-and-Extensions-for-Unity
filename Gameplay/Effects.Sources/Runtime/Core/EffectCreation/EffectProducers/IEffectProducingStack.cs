namespace EggCentric.Effects.Sources
{
    public interface IEffectProducingStack<TContext> : IReadOnlyEffectProducingStack<TContext>, IItemStack<IEffectProducer<TContext>>
    {
    }
}
