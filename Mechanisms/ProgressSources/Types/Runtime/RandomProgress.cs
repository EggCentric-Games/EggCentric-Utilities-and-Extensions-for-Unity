using EggCentric.Randomization;
using EggCentric.ValueProviders.Random;

namespace EggCentric.ProgressSources.Types
{
    public class RandomProgress : ExternalProgress
    {
        public RandomProgress() : this(RandomTypes.Uniform, new MiddleDeviation())
        {
        }

        public RandomProgress(IRandom random, IDeviationStrategy deviationStrategy) : base(new RandomValueProvider(0f, 1f, random, deviationStrategy))
        {
        }
    }
}