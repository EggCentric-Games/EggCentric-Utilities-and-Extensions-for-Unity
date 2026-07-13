namespace EggCentric.Randomization
{
    public class MiddleDeviation : IDeviationStrategy
    {
        public float GetValue(IRandom random) => random.Range(0, 1f);
    }
}