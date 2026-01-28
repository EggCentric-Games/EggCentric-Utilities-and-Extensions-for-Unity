using EggCentric.Effects;

namespace EggCentric.Buffs
{
    public abstract class EnforcedBuff : Buff
    {
        protected IEffectReceiver target;

        protected sealed override bool TryBind(IBuffReceiver receiver)
        {
            if (!receiver.CanPresent(out target))
                return false;

            return true;
        }
    }
}
