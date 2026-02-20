using EggCentric.ValueProviders;

namespace EggCentric.Timers
{
    public class RepeatingStrategy : IResetStrategy
    {
        public int MaxCycles;

        private readonly IValueProvider<int> _cycleCount;

        public RepeatingStrategy(IValueProvider<int> cycleCount, int maxCycles = 0)
        {
            _cycleCount = cycleCount;

            MaxCycles = maxCycles;
        }

        public bool IsFinal() => _cycleCount.Value < MaxCycles;
    }
}