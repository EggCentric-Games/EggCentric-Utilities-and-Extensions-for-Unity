namespace EggCentric.PID
{
    public interface IPidController<TValue>
    {
        public float Output { get; }

        public void SetCoefficients(PidCoefficients coefficients);
        public void SetTarget(TValue target);
    }
}
