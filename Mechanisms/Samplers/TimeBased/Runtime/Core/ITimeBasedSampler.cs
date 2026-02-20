using EggCentric.ValueProviders.DataContainers;
using EggCentric.LifeCycleHandling;

namespace EggCentric.Samplers.TimeBased
{
    public interface ITimeBasedSampler : ISampler, ITickable
    {
        public Field<float> Speed { get; }
    }
}
