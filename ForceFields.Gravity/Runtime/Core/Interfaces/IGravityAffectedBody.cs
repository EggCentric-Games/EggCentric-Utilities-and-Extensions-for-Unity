using EggCentric.DataContainers;

namespace EggCentric.ForceFields.Gravity
{
    public interface IGravityAffectedBody
    {
        public ITrackableValue<bool> IsInZeroGravity { get; }

        public void ApplyGravity(IForceFieldContext context);
        public void RemoveGravity(IForceFieldContext context);
    }
}