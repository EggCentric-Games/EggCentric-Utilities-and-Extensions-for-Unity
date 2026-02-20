using UnityEngine;

namespace EggCentric.QoL
{
    public static class CurveExtensions
    {
        public static AnimationCurve RemapTime(this AnimationCurve curve, float targetMin = 0f, float targetMax = 1f)
        {
            int maxIndex = curve.length - 1;

            if (maxIndex < 0)
                return curve;

            void ClampKeyframe(int index, float minTime, float maxTime)
            {
                var keyframe = curve[index];
                keyframe.time = keyframe.time.Remap(minTime, maxTime, targetMin, targetMax);
                curve.MoveKey(index, keyframe);
            }

            if (maxIndex == 0)
            {
                ClampKeyframe(0, targetMin, targetMax);
                return curve;
            }

            var minTime = curve[0].time;
            var maxTime = curve[maxIndex].time;
            for (int i = 0; i <= maxIndex; i++)
                ClampKeyframe(i, minTime, maxTime);

            return curve;
        }
    }
}
