using Sirenix.OdinInspector;
using UnityEngine;

namespace EggCentric.ProceduralGeneration
{
    [CreateAssetMenu(fileName = "GenerationPipeline_", menuName = "EggCentric/Generation")]
    public class GenerationPipelineConfig : SerializedScriptableObject, IConfig<GenerationPipeline>
    {
        public IGenerationNodeConfig<IGenerationNode> RootConfig => _rootConfig;

        [SerializeField] private IGenerationNodeConfig<IGenerationNode> _rootConfig;

        public GenerationPipeline CreateInstance()
        {
            if (_rootConfig != null)
                return new GenerationPipeline(_rootConfig.CreateInstance());

            return new GenerationPipeline(EmptyGenerationNode.Instance);
        }
    }
}
