using EggCentric.CoordinateSystems;
using EggCentric.CoordinateSystems.Defaults;
using EggCentric.Samplers;
using EggCentric.ValueProviders.Validated;
using UnityEngine;

namespace EggCentric.Emmiters
{
    public class SectorEmitter : IEmitterShape
    {
        public ValidatedValue<float> MinAngle { get; }
        public ValidatedValue<float> MaxAngle { get; }

        private Sampler _sampler;
        private Arc _base;

        public SectorEmitter(Sampler sampler, float minAngle = 0f, float maxAngle = 0f) : this(sampler, CoordinateFrames.Frame2D, minAngle, maxAngle) { }
        public SectorEmitter(Sampler sampler, ICoordinateFrame coordinatesReferences, float minAngle = 0f, float maxAngle = 45f)
        {
            _sampler = sampler;

            MinAngle = new ValidatedValue<float>(ValidateLowerAngle, minAngle);
            MaxAngle = new ValidatedValue<float>(ValidateUpperAngle, maxAngle);

            _base = new Arc(MinAngle, MaxAngle).InCoordinates(coordinatesReferences);

            MinAngle.OnValueChanged += value => _base.MinAngle.Value = value;
            MaxAngle.OnValueChanged += value => _base.MaxAngle.Value = value;
        }

        public Point GetPoint(float filling)
        {
            var sample = _sampler.GetSample();
            var deviatedDirection = _base.SampleAt(sample);

            return new Point(deviatedDirection * filling, deviatedDirection);
        }

        private bool ValidateLowerAngle(float angle, out float validated)
        {
            var upperLimit = MaxAngle != null ? MaxAngle : 180f;

            validated = angle;
            if (angle >= 0f && angle <= upperLimit)
                return true;

            validated = Mathf.Clamp(angle, 0f, upperLimit);
            return false;
        }

        private bool ValidateUpperAngle(float angle, out float validated)
        {
            var lowerLimit = MinAngle != null ? MinAngle : 0f;

            validated = angle;
            if (angle >= lowerLimit && angle <= 180f)
                return true;

            validated = Mathf.Clamp(angle, lowerLimit, 180f);
            return false;
        }
    }
}