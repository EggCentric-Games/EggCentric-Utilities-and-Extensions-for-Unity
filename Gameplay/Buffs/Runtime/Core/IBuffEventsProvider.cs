using System;

namespace EggCentric.Buffs
{
    public interface IBuffEventsProvider
    {
        public event Action<IBuff> OnApplication;
        public event Action<IBuff> OnRemoval;
    }
}
