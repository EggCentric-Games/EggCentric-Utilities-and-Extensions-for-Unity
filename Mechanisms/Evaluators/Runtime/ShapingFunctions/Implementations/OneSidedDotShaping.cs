
namespace EggCentric.Evaluators
{
    public class OneSidedDotShaping : DotShaping
    {
        public override float GetMultiplier(float angle)
        {
            float baseValue = base.GetMultiplier(angle);

            return (baseValue + 1f) / 2f;
        }
    }
}