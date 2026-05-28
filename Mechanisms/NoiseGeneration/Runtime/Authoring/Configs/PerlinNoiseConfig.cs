namespace EggCentric.NoiseGeneration
{
    internal class PerlinNoiseConfig : IValueSourceConfig<PerlinNoise>
    {
        public PerlinNoise CreateInstance() => new PerlinNoise();
    }
}