using EggCentric.ValueProviders.DataContainers;
using EggCentric.ValueProviders;
using System.Collections.Generic;
using UnityEngine;

namespace EggCentric.ForceFields
{
    public class ForceFieldsEvaluator<TSource> : IForceFieldsReceiver<TSource> where TSource : IForceFieldContext
    {
        public ITrackableValue<bool> HasActiveFields => _hasActiveFields;

        private readonly HashSet<TSource> _sources = new();
        private readonly Field<bool> _hasActiveFields = new();

        public void AddSource(TSource source)
        {
            if(_sources.Add(source))
                UpdateActiveFields();
        }

        public void RemoveSource(TSource source)
        {
            if(_sources.Remove(source))
                UpdateActiveFields();
        }

        public Vector3 EvaluateAt(Vector3 position)
        {
            if (_sources.Count == 0)
                return Vector3.zero;

            Vector3 totalForce = Vector3.zero;
            foreach (var source in _sources)
                totalForce += (Vector3)source.EvaluateForPosition(position);

            return totalForce;
        }

        private void UpdateActiveFields() => _hasActiveFields.Value = _sources.Count > 0;
    }
}