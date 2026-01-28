namespace EggCentric.Effects
{
    public interface IEffectSpecs : IEffectView
    {
    }

    public interface IEffectSpecs<in TData> : IEffectSpecs
    {
        public IEffect CreateInstance(TData context);
    }
}
