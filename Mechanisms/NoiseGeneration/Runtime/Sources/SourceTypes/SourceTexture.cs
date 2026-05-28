using UnityEngine;

namespace EggCentric.NoiseGeneration
{
    public class SourceTexture : ValueSource
    {
        public Texture2D Texture { get; set; }

        public SourceTexture(Texture2D texture) => Texture = texture;

        public override float SampleAt(Vector3 position)
        {
            var samplePosition = GetSamplePosition(position);
            return Texture.GetPixel(samplePosition.x, samplePosition.y).r;
        }

        private Vector2Int GetSamplePosition(Vector3 position)
        {
            Debug.LogError($"Isn't implemented yet. Should return sample position based on wrap mode");
            return Vector2Int.zero;
        }
    }
}