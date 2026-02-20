namespace EggCentric.Samplers
{
    public class Linear : IApplicationStrategy
    {
        public float Apply(float time, float _) => time;
    }
}