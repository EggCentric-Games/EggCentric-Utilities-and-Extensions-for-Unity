using EggCentric.Sensors;

namespace EggCentric.ForceFields
{
    public abstract class ForceField<TAffectedComponent>
    {
        protected readonly IForceFieldContext context;
        private readonly ISensor<TAffectedComponent> _contactDetector;

        public ForceField(IForceFieldContext context, ISensor<TAffectedComponent> sensor)
        {
            this.context = context;
            _contactDetector = sensor;
        }

        public void Enable()
        {
            _contactDetector.OnNewDetection += HandleNewDetection;
            _contactDetector.OnDetectionLost += HandleDetectionLoss;
        }

        public void Disable()
        {
            _contactDetector.OnNewDetection -= HandleNewDetection;
            _contactDetector.OnDetectionLost -= HandleDetectionLoss;
        }

        protected abstract void HandleNewDetection(Detection<TAffectedComponent> detection);
        protected abstract void HandleDetectionLoss(Detection<TAffectedComponent> detection);
    }
}