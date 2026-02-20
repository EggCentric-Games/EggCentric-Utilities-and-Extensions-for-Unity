namespace EggCentric.Samplers
{
    public interface IApplicationStrategy
    {
        public float Apply(float time, float step);
    }
}