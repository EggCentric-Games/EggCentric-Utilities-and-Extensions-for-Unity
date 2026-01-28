using System.Collections.Generic;

namespace EggCentric.Sensors
{
    public interface ISensor<T> : ISensorEventsProvider<T>
    {
        public IReadOnlyCollection<Detection<T>> Detections { get; }
    }
}