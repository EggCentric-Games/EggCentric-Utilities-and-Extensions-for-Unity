using System.Collections.Generic;

namespace EggCentric.CollectionProcessing.Types
{
    public class MultipleApplicationsLimit<T> : ApplicationCountLimiter<T>
    {
        private readonly int _maxApplicationCount;
        private readonly Dictionary<T, int> _applications;

        public MultipleApplicationsLimit(int maxApplicationCount)
        {
            _maxApplicationCount = maxApplicationCount;
            _applications = new Dictionary<T, int>();
        }

        public override bool CheckItemValidity(T target)
        {
            if (!_applications.TryGetValue(target, out var applicationCount))
                return true;

            if (applicationCount < _maxApplicationCount)
                return true;
            
            return false;
        }

        public override void RegisterApplicationOnItem(T target)
        {
            if (!_applications.ContainsKey(target))
                _applications.Add(target, 0);

            _applications[target]++;
        }
    }
}
