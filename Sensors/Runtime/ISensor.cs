using System.Collections.Generic;
using UnityEngine;

namespace EggCentric.Sensors
{
    public interface ISensor<T> where T : Component
    {
        public IReadOnlyCollection<Detection<T>> Detections { get; }
    }
}