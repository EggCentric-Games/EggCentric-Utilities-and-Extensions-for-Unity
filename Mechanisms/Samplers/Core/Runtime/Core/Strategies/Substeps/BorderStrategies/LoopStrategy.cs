using EggCentric.QoL;

namespace EggCentric.Samplers
{
    public class LoopStrategy : IBorderStrategy
    {
        public float Process(float value, float limit) => value.Loop(limit);
    }
}