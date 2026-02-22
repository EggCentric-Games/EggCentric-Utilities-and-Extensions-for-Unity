using EggCentric.Samplers;
using EggCentric.ValueProviders.Validated;
using UnityEngine;

namespace EggCentric.Emmiters
{
    public class Emitter : IEmitter
    {
        public ValidatedValue<float> Thickness { get; set; }

        private IEmitterShape _emitterShape;
        private Sampler _fillingSampler;

        public Emitter(IEmitterShape shape, Sampler sampler, float thickness = 1f)
        {
            _emitterShape = shape;
            _fillingSampler = sampler;

            Thickness = new ValidatedValue<float>(ValidateThickness, thickness);
        }

        public Point Emit()
        {
            var fillingSample = _fillingSampler.GetSample();
            var correctedFilling = Mathf.Sqrt(fillingSample);
            var positionMultiplier = Mathf.Lerp(1f - Thickness, 1f, fillingSample);

            return _emitterShape.GetPoint(positionMultiplier);
        }

        private bool ValidateThickness(float ratio, out float validated)
        {
            validated = ratio;
            if (ratio >= 0f && ratio <= 1f)
                return true;

            validated = Mathf.Clamp01(ratio);
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