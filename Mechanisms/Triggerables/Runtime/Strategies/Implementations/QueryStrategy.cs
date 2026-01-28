using EggCentric.QoL;

namespace EggCentric.Triggerables
{
    public class QueryStrategy : TriggerStrategy
    {
        private int _nextToTrigger;

        public QueryStrategy(Trigger attachedTrigger) : base(attachedTrigger)
        {
        }

        public override void Release()
        {
            if (attachedTrigger.Triggerables.Count <= 0)
                return;

            attachedTrigger.Triggerables[_nextToTrigger].TryToRelease();
            _nextToTrigger = _nextToTrigger.Wrapped(attachedTrigger.Triggerables.Count);
        }

        public override bool EvaluateReadiness()
        {
            if (attachedTrigger.Triggerables.Count <= 0)
                return false;

            return attachedTrigger.Triggerables[_nextToTrigger].IsReady;
        }
    }
}