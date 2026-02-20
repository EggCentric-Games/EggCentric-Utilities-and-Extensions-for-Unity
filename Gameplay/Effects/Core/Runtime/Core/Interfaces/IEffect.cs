using EggCentric.Visitables;

namespace EggCentric.Effects
{
    public interface IEffect : IVisitor<IReception<IEffectComponent, IEffectReceiver>>, IEffectView
    {
    }
}