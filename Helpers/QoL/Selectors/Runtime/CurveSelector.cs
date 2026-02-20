using EggCentric.Randomization;
using UnityEngine;

namespace EggCentric.QoL.Selectors
{
    [System.Serializable]
    public class CurveSelector : INumberSelector
    {
        public float Value => _distribution.Evaluate(RandomTypes.ByType(_randomType).Range(0f, 1f));

        [SerializeField] private AnimationCurve _distribution;
        [SerializeField] private RandomType _randomType;

        public CurveSelector()
        {
            _distribution = AnimationCurve.Linear(0f, 0f, 1f, 1f);
        }

        public void Validate() => _distribution.RemapTime(0f, 1f);
    }
}