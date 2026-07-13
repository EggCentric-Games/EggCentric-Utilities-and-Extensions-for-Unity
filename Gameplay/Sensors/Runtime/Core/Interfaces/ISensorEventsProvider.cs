using System;

namespace EggCentric.Sensors
{
    public interface ISensorEventsProvider<out TComponent>
    {
        public event Action<IDetection<TComponent>> OnNewDetection;
        public event Action<IDetection<TComponent>> OnDetectionLost;
    }
}