using UnityEngine;

namespace EggCentric.NoiseGeneration
{
    public class NoiseGenerator : MonoBehaviour
    {
        public GenerationPipeline Pipeline { get; set; }

        public float SampleAt(Vector3 position, float t = 0f)
        {
            if(Pipeline == null)
                return 0f;

            return Pipeline.SampleAt(position, t);
        }
    }
}