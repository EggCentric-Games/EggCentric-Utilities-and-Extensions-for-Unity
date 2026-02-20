using UnityEngine;

namespace EggCentric.Samplers
{
    public class StepNumber : ITimeProcessor
    {
        public float Process(float time, float step) => Mathf.Floor(time / step);
    }
}