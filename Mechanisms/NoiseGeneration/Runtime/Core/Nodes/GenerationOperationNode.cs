using System.Collections.Generic;
using EggCentric.ProceduralGeneration.Modifiers;
using UnityEngine;

namespace EggCentric.ProceduralGeneration
{
    public class GenerationOperationNode : GenerationNode
    {
        public IGenerationNode Left => _left;
        public IGenerationNode Right => _right;
        public GenerationOperation Operation => _operation;

        private readonly IGenerationNode _left;
        private readonly IGenerationNode _right;
        private readonly GenerationOperation _operation;

        public GenerationOperationNode(
            IGenerationNode left,
            IGenerationNode right,
            GenerationOperation operation,
            IReadOnlyList<IGenerationModifier> modifiers)
            : base(modifiers)
        {
            _left = left ?? EmptyGenerationNode.Instance;
            _right = right ?? EmptyGenerationNode.Instance;
            _operation = operation;
        }

        protected override float GetSampleAt(Vector3 position, float t = 0f)
        {
            var leftValue = _left.SampleAt(position, t);
            var rightValue = _right.SampleAt(position, t);

            var value = _operation switch
            {
                GenerationOperation.Add => leftValue + rightValue,
                GenerationOperation.Subtract => leftValue - rightValue,
                GenerationOperation.Multiply => leftValue * rightValue,
                _ => leftValue + rightValue
            };

            return value;
        }
    }
}
