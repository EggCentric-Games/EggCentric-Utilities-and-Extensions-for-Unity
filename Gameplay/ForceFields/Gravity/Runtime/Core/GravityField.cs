using EggCentric.Sensors;

namespace EggCentric.ForceFields.Gravity
{
    public class GravityField : ForceField<IGravityFieldContext>
    {
        public GravityField(IGravityFieldContext context, ISensor<IForceFieldsReceiver<IGravityFieldContext>> sensor) : base(context, sensor)
        {
        }

        protected override void HandleNewDetection(IDetection<IForceFieldsReceiver<IGravityFieldContext>> detection) => detection.Component.AddSource(context);
        protected override void HandleDetectionLoss(IDetection<IForceFieldsReceiver<IGravityFieldContext>> detection) => detection.Component.AddSource(context);
    }
}