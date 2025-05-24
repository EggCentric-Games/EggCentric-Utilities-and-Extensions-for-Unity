namespace EggCentric.Utilities.Evaluators
{
    public class FlatGrowth : IGrowth
    {
        private float scaleCoefficient;

        public FlatGrowth() : this(1f)
        {
        }

        public FlatGrowth(float scale)
        {
            scaleCoefficient = scale;
        }

        public float Evaluate(float value)
        {
            return scaleCoefficient;
        }
    }
}