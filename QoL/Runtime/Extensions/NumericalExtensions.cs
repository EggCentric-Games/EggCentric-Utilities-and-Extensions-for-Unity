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
    }
}
