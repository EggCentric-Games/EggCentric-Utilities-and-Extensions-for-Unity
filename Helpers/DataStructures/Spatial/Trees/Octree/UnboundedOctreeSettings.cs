using UnityEngine;

namespace EggCentric.DataStructures.Trees
{
    [System.Serializable]
    public struct UnboundedOctreeSettings
    {
        public readonly int NodeCapacity;
        public readonly int MaxDepth;
        public readonly float MinSize;

        public UnboundedOctreeSettings(int nodeCapacity, int maxDepth, float minSize)
        {
            if (nodeCapacity < 1)
            {
                Debug.LogWarning($"Tree's node capacity must be greater or equal to 1. Clamping.");
                nodeCapacity = 1;
            }

            if (maxDepth < 0)
            {
                Debug.LogWarning($"Tree's max depth must be greater or equal to 0. Clamping.");
                maxDepth = 0;
            }

            if (float.IsNaN(minSize) || float.IsInfinity(minSize) || minSize <= 0)
            {
                Debug.LogWarning($"Tree's min node size must be greater than 0. Set to 0.01f.");
                minSize = 0.01f;
            }

            NodeCapacity = nodeCapacity;
            MaxDepth = maxDepth;
            MinSize = minSize;
        }
    }
}