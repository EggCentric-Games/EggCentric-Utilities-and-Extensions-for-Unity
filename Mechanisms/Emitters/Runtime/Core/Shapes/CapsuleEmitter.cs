using EggCentric.CoordinateSystems;
using EggCentric.CoordinateSystems.Defaults;
using EggCentric.Samplers;
using EggCentric.ValueProviders.Validated;
using UnityEngine;

namespace EggCentric.Emmiters
{
    public class CapsuleEmitter : IEmitterShape
    {
        public ValidatedValue<float> Ratio { get; }

        private ICoordinateFrame _coordinatesReference;
        private Sampler _sampler;
        private Arc _base;

        public CapsuleEmitter(Sampler sampler, float ratio = 0.5f) : this(sampler, CoordinateFrames.HorizontalFrame2D, ratio) { }
        public CapsuleEmitter(Sampler sampler, ICoordinateFrame coordinatesReferences, float ratio = 0.5f)
        {
            _coordinatesReference = coordinatesReferences;
            _sampler = sampler;

            Ratio = new ValidatedValue<float>(ValidateRatio, ratio);

            _base = new Arc(0f, 90f).InCoordinates(coordinatesReferences);
        }

        public Point GetPoint(float filling)
        {
            var straightPart = Ratio;
            var circularPart = 1 - Ratio;
            
            var circleLength = circularPart * Mathf.PI * 0.5f;
            var totalLength = straightPart + circleLength;

            var sample = _sampler.GetSample();
            var samplePosition = totalLength * sample;
            var circularity = Mathf.InverseLerp(totalLength, straightPart, samplePosition);
            circularity = Mathf.Clamp01(circularity);

            var positionOffset = Mathf.Clamp(sample, 0f, straightPart);
            var deviatedDirection = _base.SampleAt(circularity);

            var position = _coordinatesReference.MainDirection * positionOffset;
            position += deviatedDirection * filling * circularPart;

            if(Random.value > 0.5f)
            {
                deviatedDirection = Vector3.Reflect(deviatedDirection, _coordinatesReference.MainDirection);
                position = Vector3.Reflect(position, _coordinatesReference.MainDirection);
            }

            return new Point(position, deviatedDirection);
        }

        private bool ValidateRatio(float ratio, out float validated)
        {
            validated = ratio;
            if (ratio >= 0f && ratio <= 1f)
                return true;

            validated = Mathf.Clamp01(ratio);
            return false;
        }
    }
}