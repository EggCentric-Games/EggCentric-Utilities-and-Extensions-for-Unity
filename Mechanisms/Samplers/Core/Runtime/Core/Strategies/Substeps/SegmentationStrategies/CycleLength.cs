using UnityEngine;

namespace EggCentric.Samplers
{
    public class CycleLength : ISegmentationStrategy
    {
        public float Process(float step) => Mathf.Ceil(1f / step);
    }
}