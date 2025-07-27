namespace EggCentric.Triggerables
{
    public class SingleStrategy : TriggerStrategy
    {
        public SingleStrategy(Trigger attachedTrigger) : base(attachedTrigger)
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