using System;
using UnityEngine;

namespace EggCentric.Sensors
{
    public interface ISensorEventsProvider<T> where T : Component
    {
        public event Action<Detection<T>> OnNewDetection;
        public event Action<Detection<T>> OnDetectionLost;
    }
}