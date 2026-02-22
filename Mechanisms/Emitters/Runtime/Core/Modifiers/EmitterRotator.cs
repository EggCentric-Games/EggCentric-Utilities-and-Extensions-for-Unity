using EggCentric.CoordinateSystems;
using EggCentric.Samplers;
using EggCentric.ValueProviders.Validated;
using UnityEngine;

namespace EggCentric.Emmiters
{
    public class EmitterRotator : IEmitterShape
    {
        public ValidatedValue<float> MinAngle { get; }
        public ValidatedValue<float> MaxAngle { get; }

        private ICoordinateFrame _coordinatesReferences;
        private Sampler _rotationSampler;
        private IEmitterShape _emitter;

        public EmitterRotator(IEmitterShape emitter, Sampler rotationSampler, ICoordinateFrame coordinatesReferences, float minAngle = 0f, float maxAngle = 360f)
        {
            _emitter = emitter;
            _rotationSampler = rotationSampler;
            _coordinatesReferences = coordinatesReferences;

            MinAngle = new ValidatedValue<float>(ValidateLowerAngle, minAngle);
            MaxAngle = new ValidatedValue<float>(ValidateUpperAngle, maxAngle);
        }

        public Point GetPoint(float filling)
        {
            var basePoint = _emitter.GetPoint(filling);
            var targetAngle = Mathf.Lerp(MinAngle, MaxAngle, _rotationSampler.GetSample());
            var rotationalModifier = Quaternion.AngleAxis(targetAngle, _coordinatesReferences.Normal);

            return rotationalModifier * basePoint;
        }

        private bool ValidateLowerAngle(float angle, out float validated)
        {
            var upperLimit = MaxAngle != null ? MaxAngle : 360f;

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
            if (angle >= lowerLimit && angle <= 360f)
                return true;

            validated = Mathf.Clamp(angle, lowerLimit, 360f);
            return false;
        }
    }
}