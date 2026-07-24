using EggCentric.LifeCycleHandling;
using EggCentric.ValueProviders;
using System;
using UnityEngine;

namespace EggCentric.Sensors
{
    public class CollisionProbe<THit> : ITickable where THit : struct
    {
        private IValueProvider<Vector3> _position;
        private IValueProvider<Vector3> _velocity;
        private IProbe<THit> _caster;

        public event Action<THit> OnContact;

        public CollisionProbe(IProbe<THit> caster, IValueProvider<Vector3> position, IValueProvider<Vector3> velocity)
        {
            _position = position;
            _velocity = velocity;
            _caster = caster;
        }

        public void Tick(float timeStep)
        {
            if (!_caster.Cast(_position.Value, _velocity.Value.normalized, _velocity.Value.magnitude * timeStep, out var detection))
                return;

            OnContact?.Invoke(detection);
        }
    }
}
