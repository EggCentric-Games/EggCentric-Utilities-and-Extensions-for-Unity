using EggCentric.CoordinateSystems;
using EggCentric.CoordinateSystems.Defaults;
using EggCentric.ValueProviders.Validated;
using UnityEngine;

namespace EggCentric.Emmiters
{
    public class Arc : IEmitterBase
    {
        public ValidatedValue<float> MinAngle { get; }
        public ValidatedValue<float> MaxAngle { get; }

        private ICoordinateFrame _coordinatesReference;
        private IAngleSamplingStrategy _samplingStrategy;

        public Arc(float minAngle = 0f, float maxAngle = 0f)
        {
            MinAngle = new ValidatedValue<float>(ValidateLowerAngle, minAngle);
            MaxAngle = new ValidatedValue<float>(ValidateUpperAngle, maxAngle);

            InCoordinates(CoordinateFrames.Frame2D);
            WithStrategy(new UniformCos());
        }

        public Arc InCoordinates(ICoordinateFrame coordinatesReference)
        {
            if (coordinatesReference == null)
                return this;

            _coordinatesReference = coordinatesReference;
            return this;
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
            float sampleAngle = _samplingStrategy.GetAngle(MinAngle, MaxAngle, t) * Mathf.Deg2Rad;
            var sin = Mathf.Sin(sampleAngle);
            var cos = Mathf.Cos(sampleAngle);

            return cos * _coordinatesReference.Normal + sin * _coordinatesReference.Bitangent;
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