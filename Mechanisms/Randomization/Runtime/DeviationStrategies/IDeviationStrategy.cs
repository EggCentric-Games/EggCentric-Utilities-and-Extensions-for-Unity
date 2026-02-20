using EggCentric.Randomization;

namespace EggCentric.Randomization
{
    public interface IDeviationStrategy
    {
        public float GetValue(IRandom random);
    }
}