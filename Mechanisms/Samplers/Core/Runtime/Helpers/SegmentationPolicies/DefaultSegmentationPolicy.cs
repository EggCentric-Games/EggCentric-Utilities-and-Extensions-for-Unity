namespace EggCentric.Samplers
{
    public class DefaultSegmentationPolicy : ISegmentationPolicy
    {
        public ISegmentationStrategy GetSegmentation(IBorderStrategy borderStrategy) => borderStrategy is LoopStrategy ? new CycleLength() : new StepsPerCycle();
    }
}