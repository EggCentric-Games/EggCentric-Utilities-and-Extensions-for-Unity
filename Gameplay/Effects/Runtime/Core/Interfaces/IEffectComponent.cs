using EggCentric.Visitables;

namespace EggCentric.Effects
{
    public interface IEffectComponent : IVisitor<IEffectReceiver>, IEffectComponentView
    {
    }
}