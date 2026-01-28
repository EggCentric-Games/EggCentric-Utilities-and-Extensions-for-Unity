using EggCentric.DI;
using EggCentric.Visitables;

namespace EggCentric.Buffs
{
    public interface IBuff : IVisitor<IBuffReceiver>, IBuffEventsProvider
    {
        public IReadOnlyContainer Context { get; }
        public IBuffReceiver Host { get; }

        public void Remove();
    }
}
