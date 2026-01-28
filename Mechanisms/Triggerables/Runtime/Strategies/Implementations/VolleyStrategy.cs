namespace EggCentric.Triggerables
{
    public class VolleyStrategy : TriggerStrategy
    {
        public VolleyStrategy(Trigger attachedTrigger) : base(attachedTrigger)
        {
        }

        public override void Release()
        {
            if (attachedTrigger.Triggerables.Count <= 0)
                return;

            foreach (var triggerable in attachedTrigger.ReadyTriggerables)
                triggerable.TryToRelease();
        }

        public override bool EvaluateReadiness() => attachedTrigger.InactiveTriggerables.Count <= 0;
    }
}