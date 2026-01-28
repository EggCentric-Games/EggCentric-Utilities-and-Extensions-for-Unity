namespace EggCentric.Effects
{
    public interface IEffectComponentSpecs<TData> : IEffectComponentView
    {
        public IEffectComponent CreateInstance(TData data);
    }
}
