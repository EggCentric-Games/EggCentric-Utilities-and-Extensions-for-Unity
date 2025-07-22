using EggCentric.QoL;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EggCentric.Sensors
{
    public class AreaSensor<T> : MonoBehaviour, ISensor<T> where T : Component
    {
        public IReadOnlyCollection<Detection<T>> Detections => _detections;

        private HashSet<Detection<T>> _detections = new();

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (!collider.TryGetComponentInParent(out T component))
                return;

            _detections.Add(new Detection<T>(component, collider));
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            if (!collider.TryGetComponentInParent(out T component))
                return;

            var targetToRemove = _detections.FirstOrDefault(obj => obj.Collider == collider);

            if (targetToRemove != null)
                _detections.Remove(targetToRemove);
        }
    }
}