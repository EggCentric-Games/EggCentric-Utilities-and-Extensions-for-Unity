using EggCentric.Randomization;
using UnityEngine;

namespace EggCentric.QoL.Selectors
{
    [System.Serializable]
    public class RandomCurvesSelector : INumberSelector
    {
        public float Value => GetValue();

        [SerializeField] private AnimationCurve _minCurve;
        [SerializeField] private AnimationCurve _maxCurve;
        [SerializeField] private RandomType _evaluationRandomType;
        [SerializeField] private RandomType _mixRandomType;

        public RandomCurvesSelector()
        {
            _minCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
            _maxCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
        }

        private float GetValue()
        {
            var time = GetTime();
            var range = GetRange(time);
            var value = RandomTypes.ByType(_mixRandomType).Range(range.x, range.y);

            return value;
        }

        private Vector2 GetRange(float time)
        {
            float min = _minCurve.Evaluate(time);
            float max = _maxCurve.Evaluate(time);

            return new Vector2(min, max);
        }

        private float GetTime() => RandomTypes.ByType(_evaluationRandomType).Range(0f, 1f);

        public void Validate()
        {
            _minCurve.RemapTime(0f, 1f);
            _maxCurve.RemapTime(0f, 1f);
        }
    }
}