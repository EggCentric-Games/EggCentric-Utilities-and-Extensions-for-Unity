namespace EggCentric.Samplers
{
    public class BorderlessStrategy : IBorderStrategy
    {
        public float Process(float value, float _) => value;
    }
}