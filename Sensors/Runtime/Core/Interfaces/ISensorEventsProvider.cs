using System;

namespace EggCentric.Sensors
{
    public interface ISensorEventsProvider<T>
    {
        public event Action<Detection<T>> OnNewDetection;
        public event Action<Detection<T>> OnDetectionLost;
    }
}