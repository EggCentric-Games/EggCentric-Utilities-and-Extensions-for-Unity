using UnityEngine;

namespace EggCentric.NoiseGeneration
{
    public class GenerationPipeline
    {
        public IGenerationNode Root => _root;

        private readonly IGenerationNode _root;

        public GenerationPipeline(IGenerationNode root)
        {
            _root = root ?? EmptyGenerationNode.Instance;
        }

        public float SampleAt(Vector3 position, float t = 0f) => _root.SampleAt(position, t);
    }
}
