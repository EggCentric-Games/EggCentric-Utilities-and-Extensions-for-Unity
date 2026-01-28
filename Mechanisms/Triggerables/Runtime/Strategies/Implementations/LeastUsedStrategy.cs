namespace EggCentric.Triggerables
{
    public class LeastUsedStrategy : TriggerStrategy
    {
        public LeastUsedStrategy(Trigger attachedTrigger) : base(attachedTrigger)
        {
        }

        public override void Release()
        {
            if (attachedTrigger.Triggerables.Count <= 0)
                return;

            attachedTrigger.ReadyTriggerables[0].TryToRelease();
        }

        public override bool EvaluateReadiness() => attachedTrigger.ReadyTriggerables.Count > 0;
    }
}