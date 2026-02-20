using EggCentric.QoL;
using EggCentric.Randomization;
using EggCentric.Samplers;
using EggCentric.ValueProviders.Validated;
using UnityEngine;

namespace EggCentric.QoL
{
    public interface ICoordinateReferences
    {
        public Vector3 Normal { get; }
        public Vector3 Tangent { get; }
        public Vector3 Bitangent { get; }

        public bool ShouldAnglesBeFlipped { get; }
        public float AngleMultiplier { get; }
    }

    public abstract class CoordinateReferences : ICoordinateReferences
    {
        public bool ShouldAnglesBeFlipped { get; }
        public float AngleMultiplier { get; }

        public Vector3 Normal => _normal;
        public Vector3 Tangent => _tangent;
        public Vector3 Bitangent => _bitangent;

        private readonly Vector3 _normal;
        private readonly Vector3 _tangent;
        private readonly Vector3 _bitangent;

        protected CoordinateReferences(Vector3 normal, Vector3 tangent, Vector3 bitangent)
        {
            _normal = normal.normalized;
            _tangent = tangent.normalized;
            _bitangent = bitangent.normalized;

            AngleMultiplier = ComputeHandedness();
            ShouldAnglesBeFlipped = AngleMultiplier < 0f;
        }

        public float ComputeHandedness() => Vector3.Dot(Vector3.Cross(Tangent, Normal), Bitangent);
    }

    public sealed class CoordinateReferences3D : CoordinateReferences
    {
        public CoordinateReferences3D() : base(Vector3.forward, Vector3.right, Vector3.up) { }
    }

    public sealed class CoordinateReferences2D : CoordinateReferences
    {
        public CoordinateReferences2D() : base(Vector3.right, Vector3.forward, Vector3.up) { }
    }

    public static class References
    {
        public static readonly CoordinateReferences2D Coordinates2D = new CoordinateReferences2D();
        public static readonly CoordinateReferences3D Coordinates3D = new CoordinateReferences3D();
    }
}

namespace EggCentric.Emmiters
{
    public class Arc : IEmitterBase
    {
        public readonly ValidatedValue<float> MinAngle;
        public readonly ValidatedValue<float> MaxAngle;

        private ICoordinateReferences _coordinatesReference;
        private IAngleSamplingStrategy _samplingStrategy;

        public Arc(float minAngle = 0f, float maxAngle = 0f)
        {
            MinAngle = new ValidatedValue<float>(ValidateLowerAngle, minAngle);
            MaxAngle = new ValidatedValue<float>(ValidateUpperAngle, maxAngle);

            _coordinatesReference = References.Coordinates3D;
            WithStrategy(new UniformSpread());
        }

        public Arc WithStrategy(IAngleSamplingStrategy samplingStrategy)
        {
            if (samplingStrategy == null)
                return this;

            _samplingStrategy = samplingStrategy;
            return this;
        }

        public Vector3 SampleAt(float t)
        {
            t = Mathf.Clamp01(t);
            float sampleAngle = _samplingStrategy.GetAngle(MinAngle, MaxAngle, t);

            return Quaternion.AngleAxis(sampleAngle * _coordinatesReference.AngleMultiplier, _coordinatesReference.Tangent) * _coordinatesReference.Normal;
        }

        private bool ValidateLowerAngle(float angle, out float validated)
        {
            var upperLimit = MaxAngle != null ? MaxAngle : 180f;

            validated = angle;
            if (angle > 0f && angle <= upperLimit)
                return true;

            validated = Mathf.Clamp(angle, 0f, upperLimit);
            return false;
        }

        private bool ValidateUpperAngle(float angle, out float validated)
        {
            var lowerLimit = MinAngle != null ? MinAngle : 0f;

            validated = angle;
            if (angle > lowerLimit && angle <= 180f)
                return true;

            validated = Mathf.Clamp(angle, lowerLimit, 180f);
            return false;
        }
    }

    public class SectorEmitter : IEmitterShape
    {
        public readonly ValidatedValue<float> MinAngle;
        public readonly ValidatedValue<float> MaxAngle;

        private ICoordinateReferences _coordinatesReference;
        private Sampler _sampler;
        private Arc _base;

        public SectorEmitter(Sampler sampler, float minAngle = 0f, float maxAngle = 0f) : this(sampler, RandomTypes.Uniform, minAngle, maxAngle) { }
        public SectorEmitter(Sampler sampler, IRandom random, float minAngle = 0f, float maxAngle = 45f)
        {
            MinAngle = new ValidatedValue<float>(ValidateLowerAngle, minAngle);
            MaxAngle = new ValidatedValue<float>(ValidateUpperAngle, maxAngle);

            _sampler = sampler;
            _base = new Arc(minAngle, maxAngle);

            MinAngle.OnValueChanged += value => _base.MinAngle.Value = value;
            MaxAngle.OnValueChanged += value => _base.MaxAngle.Value = value;

            _coordinatesReference = References.Coordinates3D;
        }

        public Vector3 GetDirection()
        {
            //var deviation = _deviationStrategy.GetDeviation(_random);
            //var deviatedDirection = _base.SampleAt(deviation.Magnitude);
            //deviatedDirection.y *= deviation.Direction;

            var sample = _sampler.GetSample();
            var deviatedDirection = _base.SampleAt(sample);
            var inverted = Random.value > 0.5f;

            if(inverted)
                deviatedDirection.y *= -1f;

            var circularDeviation = Random.Range(0f, 180f);
            var rotationalModifier = Quaternion.AngleAxis(circularDeviation, _coordinatesReference.Normal);

            return rotationalModifier * deviatedDirection;
        }

        private bool ValidateLowerAngle(float angle, out float validated)
        {
            var upperLimit = MaxAngle != null ? MaxAngle : 180f;

            validated = angle;
            if (angle > 0f && angle <= upperLimit)
                return true;

            validated = Mathf.Clamp(angle, 0f, upperLimit);
            return false;
        }

        private bool ValidateUpperAngle(float angle, out float validated)
        {
            var lowerLimit = MinAngle != null ? MinAngle : 0f;

            validated = angle;
            if (angle > lowerLimit && angle <= 180f)
                return true;

            validated = Mathf.Clamp(angle, lowerLimit, 180f);
            return false;
        }
    }

    //public class Sphere : IEmitter
    //{
    //    public ValidatedValue<float> ArcAngle => _sector.ArcAngle;

    //    private readonly RingEmitter _sector;

    //    public Sphere(float arcAngle = 360f) => _sector = new RingEmitter(0f, 180f, arcAngle);

    //    public Point Emit() => _sector.Emit();
    //}

    //public class Hemisphere : IEmitter
    //{
    //    public ValidatedValue<float> ArcAngle => _sphereGenerator.ArcAngle;

    //    private readonly Sphere _sphereGenerator;

    //    public Hemisphere(float arc = 360f) => _sphereGenerator = new Sphere(arc);

    //    public Point Emit()
    //    {
    //        var point = _sphereGenerator.Emit();
    //        point.Direction.x = Mathf.Abs(point.Direction.x);

    //        return point;
    //    }
    //}
}