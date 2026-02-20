using System.Collections.Generic;
using System.Linq;

namespace EggCentric.Effects.Sources
{
    public class EffectExecutionPoliciesStack<TDetectionContext> : IEffectExecutionPolicy<TDetectionContext>, IItemStack<IEffectExecutionPolicy<TDetectionContext>> where TDetectionContext : IDetectionContext
    {
        public IReadOnlyList<IEffectExecutionPolicy<TDetectionContext>> Items => _policies;

        private readonly List<IEffectExecutionPolicy<TDetectionContext>> _policies;

        public EffectExecutionPoliciesStack() => _policies = new List<IEffectExecutionPolicy<TDetectionContext>>();

        public void HandleRequestExecution(IEffectApplicationContext<TDetectionContext> entry)
        {
            foreach (var policy in _policies)
                policy.HandleRequestExecution(entry);
        }

        public bool ValidateTarget(TDetectionContext entry) => _policies.All(x => x.ValidateTarget(entry));

        public void Add(IEffectExecutionPolicy<TDetectionContext> item) => _policies.Add(item);
        public bool Remove(IEffectExecutionPolicy<TDetectionContext> item) => _policies.Remove(item);
        public void Clear() => _policies.Clear();

    }
}