using EggCentric.ValueProviders.DataContainers;
using EggCentric.ValueProviders;
using UnityEngine;

namespace EggCentric.ForceFields
{
    public class ForceFieldsReceiver<TSource> : MonoBehaviour, IForceFieldsReceiver<TSource> where TSource: IForceFieldContext
    {
        public ITrackableValue<bool> HasActiveFields => _evaluator.HasActiveFields;
        public ITrackableValue<Vector3> TotalForce => _totalForce;

        private ForceFieldsEvaluator<TSource> _evaluator;
        private IDataCache<Vector3> _totalForce;

        private void Awake()
        {
            _evaluator = new ForceFieldsEvaluator<TSource>();
            _totalForce = new AutomatedDataCache<Vector3>(new TimeDependentDataCache<Vector3>(), () => _evaluator.EvaluateAt(transform.position));
        }

        public void AddSource(TSource context) => _evaluator.AddSource(context);
        public void RemoveSource(TSource context) => _evaluator.RemoveSource(context);

    }
}