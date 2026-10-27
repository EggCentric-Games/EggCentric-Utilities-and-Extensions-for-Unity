using EggCentric.Entities;
using EggCentric.LifeCycleHandling;
using System;

namespace EggCentric.Sensors
{
    public class CollisionProbe<THit> : ITickable where THit : struct
    {
        private IKinematicEntity _kinematicEntity;
        private IProbe<THit> _caster;

        public event Action<THit> OnContact;

        public CollisionProbe(IKinematicEntity kinematicEntity, IProbe<THit> caster)
        {
            _kinematicEntity = kinematicEntity;
            _caster = caster;
        }

        public void Tick(float timeStep)
        {
            var translation = _kinematicEntity.Velocity * timeStep;

            if (!_caster.Cast(_kinematicEntity.Position, translation.normalized, translation.magnitude, out var detection))
                return;

            OnContact?.Invoke(detection);
        }
    }
}
