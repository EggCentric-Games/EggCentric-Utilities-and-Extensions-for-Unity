namespace EggCentric.Triggerables
{
    public interface ITriggerStrategy
    {
        public void Release();
        public bool EvaluateReadiness();
    }
}