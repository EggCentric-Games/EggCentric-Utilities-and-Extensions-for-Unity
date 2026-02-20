namespace EggCentric.Randomization
{
    public static class RandomTypes
    {
        public static readonly UniformRandom Uniform = new UniformRandom();
        public static readonly GaussianRandom Gaussian = new GaussianRandom();

        public static IRandom ByType(RandomType type)
        {
            switch (type) {
                case RandomType.Uniform: return Uniform;
                case RandomType.Gaussian: return Gaussian;
                default: return Uniform;
            }
        }
    }
}
