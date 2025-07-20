namespace EggCentric.Evaluators
{
    public class GrowthFactory
    {
        public static IGrowth Create(GrowthType type, float growthFactor)
        {
            switch (type)
            {
                case GrowthType.Linear:
                    return new LinearGrowth(growthFactor);

                case GrowthType.Logarithmic:
                    return new LogarithmicGrowth(growthFactor);

                case GrowthType.Power:
                    return new PowerGrowth(growthFactor);

                case GrowthType.Exponential:
                    return new ExponentialGrowth(growthFactor);

                default:
                    return new FlatGrowth(growthFactor);
            }
        }
    }
}