using System.Collections.Generic;

namespace EggCentric.Buffs
{
    public interface IReadOnlyBuffReceiver
    {
        public IReadOnlyList<IBuff> AppliedBuff { get; }
    }
}
