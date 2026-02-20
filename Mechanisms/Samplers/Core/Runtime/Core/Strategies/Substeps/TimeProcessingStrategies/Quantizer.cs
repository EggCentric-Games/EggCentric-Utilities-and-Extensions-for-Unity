using EggCentric.QoL;

namespace EggCentric.Samplers
{
    public class Quantizer : ITimeProcessor
    {
        public float Process(float time, float step) => time.LowerQuantize(step);
    }
}