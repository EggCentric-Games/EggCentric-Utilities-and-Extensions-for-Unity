namespace EggCentric.Emmiters
{
    public interface IAngleSamplingStrategy
    {
        public float GetAngle(float minAngle, float maxAngle, float t);
    }
}