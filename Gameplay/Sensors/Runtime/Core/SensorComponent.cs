using EggCentric.LifeCycleHandling;
using EggCentric.QoL;
using EggCentric.ValueProviders;
using EggCentric.ValueProviders.DataContainers;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EggCentric.Sensors
{
    public abstract class Sensor<TComponent> : ISensor<TComponent>
    {
        public abstract IReadOnlyCollection<IDetection<TComponent>> Detections { get; }

        public event Action<IDetection<TComponent>> OnNewDetection;
        public event Action<IDetection<TComponent>> OnDetectionLost;

        protected void NotifyNewDetection(IDetection<TComponent> detection) => OnNewDetection?.Invoke(detection);
        protected void NotifyDetectionLoss(IDetection<TComponent> detection) => OnDetectionLost?.Invoke(detection);
    }

    public abstract class SensorComponent<TComponent> : Sensor<TComponent> where TComponent : Component
    {
    }

    public abstract class TriggerSensor<TComponent, TSource> : SensorComponent<TComponent> where TComponent : Component where TSource : Component
    {
        public override IReadOnlyCollection<IDetection<TComponent>> Detections => _detections;

        private HashSet<Detection<TComponent, TSource>> _detections = new();

        protected void HandleTriggerEntry(TSource source)
        {
            if (!source.TryGetComponentInParent(out TComponent component))
                return;

            var detection = new Detection<TComponent, TSource>(component, source);
            _detections.Add(detection);
            NotifyNewDetection(detection);
        }

        protected void HandleTriggerExit(TSource source)
        {
            if (!source.TryGetComponentInParent(out TComponent component))
                return;

            var targetToRemove = _detections.FirstOrDefault(obj => obj.Source == source);
            if (targetToRemove == null)
                return;

            _detections.Remove(targetToRemove);
            NotifyDetectionLoss(targetToRemove);
        }
    }

    public class CollisionSensor<TComponent, THit> : Sensor<TComponent>, ITickable where THit : struct
    {
        public override IReadOnlyCollection<IDetection<TComponent>> Detections => _caster.Detections;

        private IValueProvider<Vector3> _position;
        private IValueProvider<Vector3> _velocity;
        private ICastingSensor<TComponent, THit> _caster;

        public event Action<THit> OnContact;

        public CollisionSensor(IValueProvider<Vector3> position, IValueProvider<Vector3> velocity, ICastingSensor<TComponent, THit> caster)
        {
            _position = position;
            _velocity = velocity;
            _caster = caster;
        }

        public void Tick(float timeStep)
        {
            if(!_caster.Cast(_position.Value, _velocity.Value.normalized, _velocity.Value.magnitude * timeStep, out var detection))
                return;

            OnContact?.Invoke(detection.Source.HitInfo);
        }
    }

    public interface ICastingSensor<TComponent, THit> : ISensor<TComponent> where THit : struct
    {
        public bool Cast(Vector3 origin, Vector3 direction, float maxDistance, out Detection<TComponent, DetectionSource<THit>> detection);
        public IReadOnlyCollection<Detection<TComponent, DetectionSource<THit>>> CastAll(Vector3 origin, Vector3 direction, float maxDistance);
    }

    public abstract class CastingSensor<TComponent, THit> : Sensor<TComponent>, ICastingSensor<TComponent, THit> where THit : struct
    {
        public override IReadOnlyCollection<IDetection<TComponent>> Detections => _detectionRegistry.Items;

        private Register<Detection<TComponent, DetectionSource<THit>>> _detectionRegistry;

        public CastingSensor()
        {
            _detectionRegistry = new Register<Detection<TComponent, DetectionSource<THit>>>();

            _detectionRegistry.OnItemEntry += NotifyNewDetection;
            _detectionRegistry.OnItemExit += NotifyDetectionLoss;
        }

        public bool Cast(Vector3 origin, Vector3 direction, float maxDistance, out Detection<TComponent, DetectionSource<THit>> detection)
        {
            var detections = CollectDetections(origin, direction, maxDistance);
            _detectionRegistry.ProcessEntry(detections);

            var wasSomethingDetected = _detectionRegistry.Items.Count > 0;
            if (wasSomethingDetected)
                detection = detections[0];
            else
                detection = default;

            return wasSomethingDetected;
        }

        public IReadOnlyCollection<Detection<TComponent, DetectionSource<THit>>> CastAll(Vector3 origin, Vector3 direction, float maxDistance)
        {
            var detections = CollectDetections(origin, direction, maxDistance);
            _detectionRegistry.ProcessEntry(detections);

            return _detectionRegistry.Items;
        }

        protected abstract IEnumerable<THit> GetHits(Vector3 origin, Vector3 direction, float maxDistance);
        protected abstract bool TryCreateDetection(THit hit, out Detection<TComponent, DetectionSource<THit>> detection);

        private List<Detection<TComponent, DetectionSource<THit>>> CollectDetections(Vector3 origin, Vector3 direction, float maxDistance)
        {
            var hits = GetHits(origin, direction, maxDistance);
            var currentDetections = new List<Detection<TComponent, DetectionSource<THit>>>();

            foreach (var hit in hits)
            {
                if (!TryCreateDetection(hit, out var newDetection))
                    continue;

                currentDetections.Add(newDetection);
            }

            return currentDetections;
        }
    }

    public class Raycast2DSensor<TComponent> : CastingSensor<TComponent, RaycastHit2D>
    {
        [SerializeField] private ContactFilter2D _contactFilter;

        private readonly List<RaycastHit2D> _hits = new();

        protected override IEnumerable<RaycastHit2D> GetHits(Vector3 origin, Vector3 direction, float distance)
        {
            _hits.Clear();
            Physics2D.Raycast(origin, direction, _contactFilter, _hits, distance);

            return _hits;
        }

        protected override bool TryCreateDetection(RaycastHit2D hit, out Detection<TComponent, DetectionSource<RaycastHit2D>> detection)
        {
            if (!hit.transform.TryGetComponentInHierarchy(out TComponent component))
            {
                detection = default;
                return false;
            }
            var detectionSource = new DetectionSource<RaycastHit2D>(hit.transform, hit);
            detection = new Detection<TComponent, DetectionSource<RaycastHit2D>>(component, detectionSource);
            return true;
        }
    }

    public class Raycast3DSensor<TComponent> : CastingSensor<TComponent, RaycastHit>
    {
        [SerializeField] private LayerMask _mask;
        [SerializeField] private QueryTriggerInteraction _triggerInteraction;

        private readonly RaycastHit[] _hits = new RaycastHit[32];

        protected override IEnumerable<RaycastHit> GetHits(Vector3 origin, Vector3 direction, float distance)
        {
            int count = Physics.RaycastNonAlloc(origin, direction, _hits, distance, _mask, _triggerInteraction);

            for (int i = 0; i < count; i++)
                yield return _hits[i];
        }

        protected override bool TryCreateDetection(RaycastHit hit, out Detection<TComponent, DetectionSource<RaycastHit>> detection)
        {
            if (!hit.transform.TryGetComponentInHierarchy(out TComponent component))
            {
                detection = default;
                return false;
            }

            var detectionSource = new DetectionSource<RaycastHit>(hit.transform, hit);
            detection = new Detection<TComponent, DetectionSource<RaycastHit>>(component, detectionSource);
            return true;
        }
    }

    public struct DetectionSource<THitInfo>
    {
        public Transform HitObject;
        public THitInfo HitInfo;

        public DetectionSource(Transform hitObject, THitInfo hitInfo)
        {
            HitObject = hitObject;
            HitInfo = hitInfo;
        }

        public static bool operator ==(DetectionSource<THitInfo> lhs, DetectionSource<THitInfo> rhs) => lhs.HitObject == rhs.HitObject;
        public static bool operator !=(DetectionSource<THitInfo> lhs, DetectionSource<THitInfo> rhs) => lhs.HitObject != rhs.HitObject;

        public override bool Equals(object obj) => obj == HitObject;
    }
}
