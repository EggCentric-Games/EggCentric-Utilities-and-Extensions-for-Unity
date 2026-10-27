using UnityEngine;

namespace EggCentric.QoL
{
    public static class CurveExtensions
    {
        public static void RemapTime(this AnimationCurve curve, float targetMin = 0f, float targetMax = 1f)
        {
            int maxIndex = curve.length - 1;

            if (maxIndex < 0)
                return;

            var keys = curve.keys;
            void RemapKeyframe(int index, float minTime, float maxTime)
            {
                keys[index].time = keys[index].time.Remap(minTime, maxTime, targetMin, targetMax);
            }

            if (maxIndex == 0)
            {
                RemapKeyframe(0, targetMin, targetMax);
                curve.keys = keys;
                return;
            }

            var minTime = keys[0].time;
            var maxTime = keys[maxIndex].time;
            for (int i = 0; i <= maxIndex; i++)
                RemapKeyframe(i, minTime, maxTime);

            curve.keys = keys;
        }

        public static void RemapValue(this AnimationCurve curve, float targetMin = 0f, float targetMax = 1f)
        {
            int maxIndex = curve.length - 1;

            if (maxIndex < 0)
                return;

            var keys = curve.keys;
            void RemapKeyframe(int index, float minValue, float maxValue)
            {
                keys[index].value = keys[index].value.Remap(minValue, maxValue, targetMin, targetMax);
            }

            if (maxIndex == 0)
            {
                RemapKeyframe(0, targetMin, targetMax);
                curve.keys = keys;
                return;
            }

            float minValue = float.PositiveInfinity;
            float maxValue = float.NegativeInfinity;

            for (int i = 0; i < keys.Length; i++)
            {
                float value = keys[i].value;
                if (value < minValue) minValue = value;
                if (value > maxValue) maxValue = value;
            }

            for (int i = 0; i <= maxIndex; i++)
                RemapKeyframe(i, minValue, maxValue);

            curve.keys = keys;
        }

        public static void RemapTo(this AnimationCurve curve, Vector2 targetTimeRange, Vector2 targetValueRange)
        {
            if (curve == null || curve.length == 0)
                return;

            var keys = curve.keys;

            float oldMinTime = keys[0].time;
            float oldMaxTime = keys[^1].time;

            float oldMinValue = float.PositiveInfinity;
            float oldMaxValue = float.NegativeInfinity;

            foreach (var key in keys)
            {
                oldMinValue = Mathf.Min(oldMinValue, key.value);
                oldMaxValue = Mathf.Max(oldMaxValue, key.value);
            }

            float timeRange = oldMaxTime - oldMinTime;
            float valueRange = oldMaxValue - oldMinValue;

            float timeScale = Mathf.Approximately(timeRange, 0f)
                ? 0f
                : (targetTimeRange.y - targetTimeRange.x) / timeRange;

            float valueScale = Mathf.Approximately(valueRange, 0f)
                ? 0f
                : (targetValueRange.y - targetValueRange.x) / valueRange;

            float tangentScale = timeScale == 0f
                ? 0f
                : valueScale / timeScale;


            for (int i = 0; i < keys.Length; i++)
            {
                var key = keys[i];

                key.time = Mathf.Lerp(
                    targetTimeRange.x,
                    targetTimeRange.y,
                    Mathf.InverseLerp(oldMinTime, oldMaxTime, key.time));

                key.value = Mathf.Lerp(
                    targetValueRange.x,
                    targetValueRange.y,
                    Mathf.InverseLerp(oldMinValue, oldMaxValue, key.value));

                key.inTangent *= tangentScale;
                key.outTangent *= tangentScale;

                keys[i] = key;
            }

            curve.keys = keys;
        }

        public static float GetCurveArea(this AnimationCurve curve, int stepCount = 100)
        {
            if(stepCount < 2)
            {
                Debug.LogWarning($"The minimal required step count for curve area calculation is 2. You've entered {stepCount}. Clamping.");
                stepCount = 2;
            }

            var maxTime = curve.keys[curve.length - 1].time;
            var stepLength = maxTime / stepCount;
            var totalValue = 0f;

            for(int i = 1; i < stepCount; i++)
            {
                var previousTime = (i - 1) * stepLength;
                var currentTime = i * stepLength;

                var previousValue = curve.Evaluate(previousTime);
                var currentValue = curve.Evaluate(currentTime);
                var averageValue = (currentValue + previousValue) * 0.5f;

                totalValue += averageValue;
            }

            return totalValue / (stepCount - 1);
        }
    }
}
