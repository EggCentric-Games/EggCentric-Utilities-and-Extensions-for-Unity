using EggCentric.Common.Behaviours;

namespace EggCentric.Buffs
{
    public interface IBuffReceiver : IReadOnlyBuffReceiver, IPresenter<object>
    {
        public void RegisterBuff(IBuff buff);
        public void UnregisterBuff(IBuff buff);
    }
}
