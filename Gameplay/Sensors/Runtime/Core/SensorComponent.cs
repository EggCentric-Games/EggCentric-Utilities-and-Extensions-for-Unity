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
    public abstract class SensorComponent<TComponent> : MonoBehaviour, ISensor<TComponent>
    {
        public abstract IReadOnlyCollection<IDetection<TComponent>> Detections { get; }

        public event Action<IDetection<TComponent>> OnNewDetection;
        public event Action<IDetection<TComponent>> OnDetectionLost;

        protected void NotifyNewDetection(IDetection<TComponent> detection) => OnNewDetection?.Invoke(detection);
        protected void NotifyDetectionLoss(IDetection<TComponent> detection) => OnDetectionLost?.Invoke(detection);
    }

    public abstract class TriggerSensor<TComponent, TSource> : SensorComponent<TComponent> where TSource : Component
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

            var targetToRemove = _detections.FirstOrDefault(obj => obj.Context == source);
            if (targetToRemove == null)
                return;

            _detections.Remove(targetToRemove);
            NotifyDetectionLoss(targetToRemove);
        }
    }

    public abstract class ProbeSensor<TComponent, THit> : ISensor<TComponent>, ITickable where THit : struct
    {
        public float MaxDistance { get; set; }
        public IReadOnlyCollection<IDetection<TComponent>> Detections => _detections.Items;

        private IProbe<THit> _probe;
        private Register<Detection<TComponent, THit>> _detections;
        private List<Detection<TComponent, THit>> _detectionList;

        private IValueProvider<Vector3> _position;
        private IValueProvider<Vector3> _direction;

        public event Action<IDetection<TComponent>> OnNewDetection;
        public event Action<IDetection<TComponent>> OnDetectionLost;

        public ProbeSensor(IValueProvider<Vector3> positionProvider, IValueProvider<Vector3> directionProvider, IProbe<THit> probe)
        {
            _position = positionProvider;
            _direction = directionProvider;
            _probe = probe;

            _detections.OnItemEntry += OnNewDetection;
            _detections.OnItemExit += OnDetectionLost;
        }

        public void Tick(float timeStep)
        {
            var hits = _probe.CastAll(_position.Value, _direction.Value, MaxDistance);
            ConvertToDetections(hits);
            _detections.ProcessEntry(_detectionList);
        }

        protected abstract bool TryCreateDetection(THit hit, out Detection<TComponent, THit> detection);

        private Detection<TComponent, THit>[] ConvertToDetections(IReadOnlyCollection<THit> hits)
        {
            _detectionList.Clear();

            foreach (var hit in hits)
            {
                if (!TryCreateDetection(hit, out var detection))
                    continue;

                _detectionList.Add(detection);
            }

            return _detectionList.ToArray();
        }
    }

    public class ProbeSensor2D<TComponent> : ProbeSensor<TComponent, RaycastHit2D>
    {
        public ProbeSensor2D(IValueProvider<Vector3> positionProvider, IValueProvider<Vector3> directionProvider, IProbe<RaycastHit2D> probe) : base(positionProvider, directionProvider, probe)
        {
        }

        protected override bool TryCreateDetection(RaycastHit2D hit, out Detection<TComponent, RaycastHit2D> detection)
        {
            if(!hit.transform.TryGetComponentInHierarchy<TComponent>(out var component))
            {
                detection = default;
                return false;
            }

            detection = new Detection<TComponent, RaycastHit2D>(component, hit);
            return true;
        }
    }

    public class ProbeSensor3D<TComponent> : ProbeSensor<TComponent, RaycastHit>
    {
        public ProbeSensor3D(IValueProvider<Vector3> positionProvider, IValueProvider<Vector3> directionProvider, IProbe<RaycastHit> probe) : base(positionProvider, directionProvider, probe)
        {
        }

        protected override bool TryCreateDetection(RaycastHit hit, out Detection<TComponent, RaycastHit> detection)
        {
            if (!hit.transform.TryGetComponentInHierarchy<TComponent>(out var component))
            {
                detection = default;
                return false;
            }

            detection = new Detection<TComponent, RaycastHit>(component, hit);
            return true;
        }
    }
}
