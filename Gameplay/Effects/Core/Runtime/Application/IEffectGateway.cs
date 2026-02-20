using EggCentric.Common.Behaviours;
using EggCentric.Visitables;

namespace EggCentric.Effects
{
    public interface IEffectGateway : IVisitable<IEffect>, IGateway<IEffect>, IGateway<IEffectView>
    {
        public IEffectReceiver Target { get; }
    }
}