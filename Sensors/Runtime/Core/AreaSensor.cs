using EggCentric.QoL;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EggCentric.Sensors
{
    public class AreaSensor<T> : MonoBehaviour, ISensor<T> where T : Component
    {
        public IReadOnlyCollection<Detection<T>> Detections => _detections;

        private HashSet<Detection<T>> _detections = new();

        public event Action<Detection<T>> OnNewDetection;
        public event Action<Detection<T>> OnDetectionLost;

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (!collider.TryGetComponentInParent(out T component))
                return;

            var detection = new Detection<T>(component, collider);
            _detections.Add(detection);
            OnNewDetection?.Invoke(detection);
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            if (!collider.TryGetComponentInParent(out T component))
                return;

            var targetToRemove = _detections.FirstOrDefault(obj => obj.Collider == collider);
            if (targetToRemove == null)
                return;

            _detections.Remove(targetToRemove);
            OnDetectionLost?.Invoke(targetToRemove);
        }
    }
}