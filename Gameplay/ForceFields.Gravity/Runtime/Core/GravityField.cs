using EggCentric.Sensors;

namespace EggCentric.ForceFields.Gravity
{
    public class GravityField : ForceField<IGravityAffectedBody>
    {
        public GravityField(IForceFieldContext context, ISensor<IGravityAffectedBody> sensor) : base(context, sensor)
        {
        }

        protected override void HandleNewDetection(Detection<IGravityAffectedBody> detection) => detection.Component.ApplyGravity(context);
        protected override void HandleDetectionLoss(Detection<IGravityAffectedBody> detection) => detection.Component.RemoveGravity(context);
    }
}