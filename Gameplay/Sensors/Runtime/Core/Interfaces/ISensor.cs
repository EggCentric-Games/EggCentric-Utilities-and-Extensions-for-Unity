using System.Collections.Generic;

namespace EggCentric.Sensors
{
    public interface ISensor<out T> : ISensorEventsProvider<T>
    {
        public IReadOnlyCollection<IDetection<T>> Detections { get; }
    }
}