using System.Collections.Generic;

namespace EggCentric.Sensors
{
    public interface ISensor<out TComponent> : ISensorEventsProvider<TComponent>
    {
        public IReadOnlyCollection<IDetection<TComponent>> Detections { get; }
    }
}