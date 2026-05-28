namespace EggCentric.ProceduralGeneration
{
    internal class PerlinNoiseConfig : IValueSourceConfig<PerlinNoise>
    {
        public PerlinNoise CreateInstance() => new PerlinNoise();
    }
}