namespace EggCentric.Triggerables
{
    public abstract class TriggerStrategy : ITriggerStrategy
    {
        protected readonly Trigger attachedTrigger;

        public TriggerStrategy(Trigger attachedTrigger)
        {
            this.attachedTrigger = attachedTrigger;
        }

        public abstract void Release();
        public abstract bool EvaluateReadiness();
    }
}