using EggCentric.DI;

namespace EggCentric.Effects
{
    public interface IEffectComponentView
    {
        public IReadOnlyContainer Context { get; }
    }
}
