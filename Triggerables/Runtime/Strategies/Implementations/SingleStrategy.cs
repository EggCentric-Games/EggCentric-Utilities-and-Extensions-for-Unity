using System.Linq;

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

            ITriggerable selectedTriggerable = attachedTrigger.Triggerables.FirstOrDefault(x => x.IsReady);
            
            if(selectedTriggerable != null)
                selectedTriggerable.TryToRelease();
        }

        public override bool EvaluateReadiness() => attachedTrigger.ReadyTriggerables.Count > 0;
    }
}