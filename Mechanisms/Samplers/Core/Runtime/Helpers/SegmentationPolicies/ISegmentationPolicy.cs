namespace EggCentric.Samplers
{
    public interface ISegmentationPolicy
    {
        public ISegmentationStrategy GetSegmentation(IBorderStrategy borderStrategy);
    }
}