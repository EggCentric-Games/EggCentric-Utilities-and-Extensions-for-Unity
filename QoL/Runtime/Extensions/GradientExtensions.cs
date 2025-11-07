using UnityEngine;

namespace EggCentric.QoL
{
    public static class GradientExtensions
    {
        private static Texture2D GetGradientTexture(this Gradient gradient, int precision)
        {
            Texture2D gradientTexture = new Texture2D(precision, 1);
            gradientTexture.filterMode = FilterMode.Point;
            gradientTexture.wrapMode = TextureWrapMode.Clamp;

            for (int i = 0; i < precision; i++)
            {
                float samplePosition = (float)i / precision;
                Color gradientSample = gradient.Evaluate(samplePosition);
                gradientTexture.SetPixel(i, 1, gradientSample);
            }
            gradientTexture.Apply();

            return gradientTexture;
        }
    }
}
