namespace EggCentric.Effects.Sources
{
    public interface IEffectAdaptersStack<TContext> : IReadOnlyEffectAdaptersStack<TContext>, IItemStack<IEffectSpecsAdapter<TContext>>
    {

    }
}
