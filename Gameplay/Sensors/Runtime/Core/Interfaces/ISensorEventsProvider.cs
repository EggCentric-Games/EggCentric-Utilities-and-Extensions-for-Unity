using System;

namespace EggCentric.Sensors
{
    public interface ISensorEventsProvider<out T>
    {
        public event Action<IDetection<T>> OnNewDetection;
        public event Action<IDetection<T>> OnDetectionLost;
    }
}