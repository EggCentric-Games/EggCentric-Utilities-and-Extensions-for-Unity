namespace EggCentric.Samplers
{
    public interface IBorderStrategy
    {
        public float Process(float value, float limit);
    }
}