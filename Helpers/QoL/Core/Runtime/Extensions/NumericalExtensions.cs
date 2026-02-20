using System;
using UnityEngine;

namespace EggCentric.QoL
{
    public static class NumericalExtensions
    {
        public static int Wrapped(this int number, int max)
        {
            double loop = Math.Floor((double)number / max);
            int wrappedNumber = (int)(number - max * loop);

            return wrappedNumber;
        }

        public static double Wrapped(this double number, double max)
        {
            double loop = Math.Floor((double)number / max);
            double wrappedNumber = number - max * loop;

            return wrappedNumber;
        }

        public static float Wrapped(this float number, float max)
        {
            float loop = Mathf.Floor((float)number / max);
            float wrappedNumber = number - max * loop;

            return wrappedNumber;
        }

        public static float Remap(this float number, float minIn, float maxIn, float minOut, float maxOut)
        {
            var t = Mathf.InverseLerp(minIn, maxIn, number);
            var remapped = Mathf.Lerp(minOut, maxOut, t);

            return remapped;
        }

        public static float Remap(this float number, Vector2 inputRange, Vector2 outputRange)
        {
            var t = Mathf.InverseLerp(inputRange.x, inputRange.y, number);
            var remapped = Mathf.Lerp(outputRange.x, outputRange.y, t);

            return remapped;
        }

        public static int Ceil(this float number) => Mathf.CeilToInt(number);
        public static int Floor(this float number) => Mathf.FloorToInt(number);

        public static float Loop(this float number, float step) => number % step;
        public static float PingPong(this float number, float ceil) => Mathf.Abs((number % (2f * ceil)) - ceil)
            ;
        public static float LowerQuantize(this float number, float step) => Mathf.Floor(number / step) * step;
        public static float Quantize(this float number, float step) => Mathf.Round(number / step) * step;
        public static float UpperQuantize(this float number, float step) => Mathf.Ceil(number / step) * step;
    }
}
