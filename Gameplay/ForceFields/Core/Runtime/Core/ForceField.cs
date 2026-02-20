using EggCentric.Sensors;

namespace EggCentric.ForceFields
{
    public abstract class ForceField<TSource> where TSource : IForceFieldContext
    {
        protected readonly TSource context;
        private readonly ISensor<IForceFieldsReceiver<TSource>> _contactDetector;

        public ForceField(TSource context, ISensor<IForceFieldsReceiver<TSource>> contactDetector)
        {
            this.context = context;
            _contactDetector = contactDetector;
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

        protected abstract void HandleNewDetection(IDetection<IForceFieldsReceiver<TSource>> detection);
        protected abstract void HandleDetectionLoss(IDetection<IForceFieldsReceiver<TSource>> detection);
    }
}