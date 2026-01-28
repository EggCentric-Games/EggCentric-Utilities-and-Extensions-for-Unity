using EggCentric.DI;
namespace EggCentric.Effects
{
    public abstract class EffectComponent<T> : IEffectComponent
    {
        public IReadOnlyContainer Context { get; }

        public EffectComponent(IReadOnlyContainer context) => Context = context;

        public bool Visit(IEffectReceiver receiver)
        {
            if (!receiver.CanPresent<T>(out var target))
                return false;

            ApplyTo(target);
            return true;
        }

        public abstract void ApplyTo(T target);
    }
}