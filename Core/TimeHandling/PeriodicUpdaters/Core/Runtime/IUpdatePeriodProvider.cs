namespace EggCentric.PeriodicUpdaters
{
    public interface IUpdatePeriodProvider
    {
        public float GetNext();
        public void Reset();
    }
}