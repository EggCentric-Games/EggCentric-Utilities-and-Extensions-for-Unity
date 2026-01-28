using System.Collections.Generic;

namespace EggCentric.PeriodicUpdaters
{
    public class SequentialUpdatePeriodProvider : SequentialValueProvider<float>, IUpdatePeriodProvider
    {
        public SequentialUpdatePeriodProvider(IEnumerable<float> sequence) : base(sequence)
        {
        }
    }
}