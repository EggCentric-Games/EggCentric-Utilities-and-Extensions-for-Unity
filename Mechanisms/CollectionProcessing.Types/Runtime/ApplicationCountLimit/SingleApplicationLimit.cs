using System.Collections.Generic;

namespace EggCentric.CollectionProcessing.Types
{
    public class SingleApplicationLimit<T> : ApplicationCountLimiter<T>
    {
        private readonly HashSet<T> _entitiesReceivedTheEffect;

        public SingleApplicationLimit() => _entitiesReceivedTheEffect = new HashSet<T>();

        public override bool CheckItemValidity(T target) => !_entitiesReceivedTheEffect.Contains(target);
        public override void RegisterApplicationOnItem(T target) => _entitiesReceivedTheEffect.Add(target);
    }
}
