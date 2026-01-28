using UnityEngine;

namespace EggCentric.Evaluators
{
    public class AbsoluteDotShaping : DotShaping
    {
        public override float GetMultiplier(float angle)
        {
            float baseValue = base.GetMultiplier(angle);

            return Mathf.Abs(baseValue);
        }
    }
}