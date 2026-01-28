namespace EggCentric.LifeCycleHandling
{
    public interface IPauseable : IPauseableEventProvider
    {
        public bool IsPaused { get; }

        public void Pause();
        public void Resume();
    }
}