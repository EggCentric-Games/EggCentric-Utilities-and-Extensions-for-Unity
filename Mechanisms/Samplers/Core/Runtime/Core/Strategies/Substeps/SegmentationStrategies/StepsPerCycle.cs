using UnityEngine;

namespace EggCentric.Samplers
{
    public class StepsPerCycle : ISegmentationStrategy
    {
        public float Process(float step) => Mathf.Floor(1f / step);
    }
}