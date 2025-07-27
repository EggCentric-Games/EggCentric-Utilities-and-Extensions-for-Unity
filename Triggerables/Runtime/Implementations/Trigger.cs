using System;
using System.Collections.Generic;

namespace EggCentric.Triggerables
{

    public abstract class Trigger : Triggerable
    {
        public IReadOnlyList<ITriggerable> Triggerables => triggerables;
        public IReadOnlyList<ITriggerable> ReadyTriggerables => readyTriggerables;
        public IReadOnlyList<ITriggerable> InactiveTriggerables => inactiveTriggerables;

        protected ITriggerStrategy triggerStrategy;

        protected List<ITriggerable> triggerables;
        protected List<ITriggerable> readyTriggerables;
        protected List<ITriggerable> inactiveTriggerables;

        private Dictionary<ITriggerable, (Action onSet, Action onReleased)> _handlers;

        public Trigger(TriggerMode triggerMode = TriggerMode.Single)
        {
            triggerables = new List<ITriggerable>();
            readyTriggerables = new List<ITriggerable>();
            inactiveTriggerables = new List<ITriggerable>();

            _handlers = new Dictionary<ITriggerable, (Action onSet, Action onReleased)>();

            ChangeMode(triggerMode);
        }

        public override void EvaluateReadiness() => isReady.Value = triggerStrategy.EvaluateReadiness();

        public void ChangeMode(TriggerMode targetMode)
        {
            triggerStrategy = TriggerModeFactory.Create(targetMode, this);
            EvaluateReadiness();
        }

        protected override void Release()
        {
            triggerStrategy.Release();

            base.Release();
        }

        public void RegisterItem(ITriggerable item)
        {
            if (triggerables.Contains(item))
                return;

            Action onSet = () => MarkAsReady(item);
            Action onReleased = () => MarkAsUsed(item);
            _handlers[item] = (onSet, onReleased);

            item.OnTriggerSet += onSet;
            item.OnTriggerReleased += onReleased;

            triggerables.Add(item);
            if (item.IsReady)
                readyTriggerables.Add(item);
            else
                inactiveTriggerables.Add(item);
        }

        public void UnregisterItem(ITriggerable item)
        {
            if (!triggerables.Contains(item))
                return;

            item.OnTriggerSet -= _handlers[item].onSet;
            item.OnTriggerReleased -= _handlers[item].onReleased;

            _handlers.Remove(item);
            triggerables.Remove(item);
            if (item.IsReady)
                readyTriggerables.Remove(item);
            else
                inactiveTriggerables.Remove(item);
        }

        private void MarkAsReady(ITriggerable triggerable)
        {
            if (inactiveTriggerables.Contains(triggerable))
                inactiveTriggerables.Remove(triggerable);

            if(!readyTriggerables.Contains(triggerable))
                readyTriggerables.Add(triggerable);

            EvaluateReadiness();
        }

        private void MarkAsUsed(ITriggerable triggerable)
        {
            if (readyTriggerables.Contains(triggerable))
                readyTriggerables.Remove(triggerable);

            if (!inactiveTriggerables.Contains(triggerable))
                inactiveTriggerables.Add(triggerable);

            EvaluateReadiness();
        }
    }
}