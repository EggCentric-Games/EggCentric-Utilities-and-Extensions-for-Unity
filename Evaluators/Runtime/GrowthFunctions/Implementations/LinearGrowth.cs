namespace EggCentric.Evaluators
{
    public class LinearGrowth : IGrowth
    {
        private float scaleCoefficient;

        public LinearGrowth() : this(1f)
        {
        }

        public LinearGrowth(float scale)
        {
            scaleCoefficient = scale;
        }

        public float Evaluate(float value)
        {
            return value * scaleCoefficient;
        }
    }
}