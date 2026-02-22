using EggCentric.CoordinateSystems;
using EggCentric.CoordinateSystems.Defaults;
using EggCentric.ValueProviders.Validated;
using UnityEngine;

namespace EggCentric.Emmiters
{
    public class ConeEmitter : IEmitterShape
    {
        public ValidatedValue<float> MinAngle { get; }
        public ValidatedValue<float> MaxAngle { get; }

        private ICoordinateFrame _coordinatesReference;
        private Arc _base;

        public ConeEmitter(float minAngle = 0f, float maxAngle = 45f) : this(CoordinateFrames.Frame2D, minAngle, maxAngle) { }
        public ConeEmitter(ICoordinateFrame coordinatesReferences, float minAngle = 0f, float maxAngle = 45f)
        {
            _coordinatesReference = coordinatesReferences;

            MinAngle = new ValidatedValue<float>(ValidateLowerAngle, minAngle);
            MaxAngle = new ValidatedValue<float>(ValidateUpperAngle, maxAngle);

            _base = new Arc(MinAngle, MaxAngle).InCoordinates(coordinatesReferences).WithStrategy(new UniformTan());

            MinAngle.OnValueChanged += value => _base.MinAngle.Value = value;
            MaxAngle.OnValueChanged += value => _base.MaxAngle.Value = value;
        }

        public Point GetPoint(float filling)
        {
            var deviatedDirection = _base.SampleAt(filling);

            return new Point(_coordinatesReference.Bitangent * filling, deviatedDirection);
        }

        private bool ValidateLowerAngle(float angle, out float validated)
        {
            var upperLimit = MaxAngle != null ? MaxAngle : 90f;

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
            if (angle >= lowerLimit && angle <= 90f)
                return true;

            validated = Mathf.Clamp(angle, lowerLimit, 90f);
            return false;
        }
    }
}