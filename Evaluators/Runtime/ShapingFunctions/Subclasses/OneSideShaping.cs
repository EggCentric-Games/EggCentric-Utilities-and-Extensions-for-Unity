
namespace EggCentric.Utilities.Evaluators
{
    public class OneSideShaping : DotShaping
    {
        public override float GetMultiplier(float angle)
        {
            float baseValue = base.GetMultiplier(angle);

            return (baseValue + 1f) / 2f;
        }
    }
}