using EggCentric.Entities;
using EggCentric.LifeCycleHandling;
using EggCentric.Sensors;
using System;
using UnityEngine;

namespace EggCentric.Ballistics
{
    public class CollisionProbeComponent : MonoBehaviour, ITickable
    {
        [SerializeField] private ProbeSettings3D _raycastLayers;

        private IKinematicEntity _sourceEntity;
        private CollisionProbe<RaycastHit> _collisionProbe;

        public event Action<RaycastHit> OnHit
        {
            add => _collisionProbe.OnContact += value;
            remove => _collisionProbe.OnContact -= value;
        }

        public void Initialize(IKinematicEntity sourceEntity)
        {
            _sourceEntity = sourceEntity;
            _collisionProbe = new CollisionProbe<RaycastHit>(sourceEntity, new RaycastProbe3D(_raycastLayers));
        }

        public void Tick(float timeStep) => _collisionProbe.Tick(timeStep);
    }
}

