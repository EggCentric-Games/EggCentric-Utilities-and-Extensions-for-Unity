using System;

namespace EggCentric.NoiseGeneration
{
    public static class NoiseHash
    {
        /// <summary>
        /// SplitMix64 finalizer.
        /// </summary>
        public static ulong Mix(ulong x)
        {
            x ^= x >> 30;
            x *= 0xbf58476d1ce4e5b9UL;

            x ^= x >> 27;
            x *= 0x94d049bb133111ebUL;

            x ^= x >> 31;

            return x;
        }

        /// <summary>
        /// N-dimensional float hash.
        /// Deterministic and stable across platforms.
        /// </summary>
        public static ulong Hash(ReadOnlySpan<float> values, ulong seed)
        {
            ulong h = seed;

            for (int i = 0; i < values.Length; i++)
            {
                ulong v = (ulong)(uint)BitConverter.SingleToInt32Bits(values[i]);

                // Distinct large odd constant per dimension
                ulong prime = GetDimensionPrime(i);

                h ^= Mix(v * prime);

                // Optional avalanche between dimensions
                h = Mix(h);
            }

            return h;
        }
        
        /// <summary>
        /// Convenience overload.
        /// Allocates.
        /// </summary>
        public static ulong Hash(ulong seed, params float[] values)
        {
            return Hash(values.AsSpan(), seed);
        }

        /// <summary>
        /// Returns deterministic noise in [0,1].
        /// </summary>
        public static double Hash01(ReadOnlySpan<float> values, ulong seed)
        {
            return Hash(values, seed) / (double)ulong.MaxValue;
        }

        /// <summary>
        /// Convenience overload.
        /// Allocates.
        /// </summary>
        public static double Hash01(ulong seed, params float[] values)
        {
            return Hash01(values.AsSpan(), seed);
        }

        /// <summary>
        /// Float version.
        /// </summary>
        public static float Hash01f(ReadOnlySpan<float> values, ulong seed)
        {
            return (float)Hash01(values, seed);
        }

        /// <summary>
        /// Convenience overload.
        /// Allocates.
        /// </summary>
        public static float Hash01f(ulong seed, params float[] values)
        {
            return Hash01f(values.AsSpan(), seed);
        }

        /// <summary>
        /// Generates a dimension-specific odd constant.
        /// </summary>
        private static ulong GetDimensionPrime(int dimension)
        {
            // Golden ratio based Weyl sequence
            return 0x9E3779B185EBCA87UL + (ulong)dimension * 0xC2B2AE3D27D4EB4FUL;
        }
    }
}