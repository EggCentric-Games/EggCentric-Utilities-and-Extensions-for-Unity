using EggCentric.ProceduralGeneration.Authoring.Modifiers;
using UnityEngine;

namespace EggCentric.ProceduralGeneration
{
    [System.Serializable]
    public class GenerationOperationConfig : IGenerationNodeConfig<GenerationOperationNode>
    {
        public IGenerationNodeConfig<IGenerationNode> LeftConfig => _leftConfig;
        public IGenerationNodeConfig<IGenerationNode> RightConfig => _rightConfig;
        public GenerationOperation Operation => _operation;
        public IModifierConfig[] ModifierConfigs => _modifierConfigs;

        [SerializeField] private IGenerationNodeConfig<IGenerationNode> _leftConfig;
        [SerializeField] private IGenerationNodeConfig<IGenerationNode> _rightConfig;
        [SerializeField] private GenerationOperation _operation;
        [SerializeField] private IModifierConfig[] _modifierConfigs = new IModifierConfig[0];

        public GenerationOperationNode CreateInstance()
        {
            return new GenerationOperationNode(
                CreateNode(_leftConfig),
                CreateNode(_rightConfig),
                _operation,
                ModifierConfigUtility.CreateModifiers(_modifierConfigs));
        }

        private static IGenerationNode CreateNode(IGenerationNodeConfig<IGenerationNode> config)
        {
            if (config == null)
                return EmptyGenerationNode.Instance;

            return config.CreateInstance();
        }
    }
}
