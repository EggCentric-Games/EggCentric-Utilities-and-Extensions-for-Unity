using EggCentric.Sensors;
using UnityEngine;

namespace EggCentric.Effectors
{
    public abstract class EffectorEntity<T> : EffectorEntity where T : Component
    {
        private AreaSensor<T> _sensor;

        protected override void Initialize()
        {
            base.Initialize();

            _sensor = CreateSensor();
        }

        protected override void ApplyEffects()
        {
            if (!_sensor)
            {
                Debug.LogError("Corresponding sensor is missing!");
                return;
            }

            foreach (var effectTarget in _sensor.Detections)
                ApplyEffect(effectTarget);
        }

        private void OnDestroy()
        {
            if (_sensor)
                Destroy(_sensor);
        }

        protected abstract AreaSensor<T> CreateSensor();
        protected abstract void ApplyEffect(Detection<T> target);
    }
}
