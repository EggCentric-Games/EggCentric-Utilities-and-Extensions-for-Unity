using System.Collections.Generic;
using UnityEngine;

namespace EggCentric.Mergeables
{
    public class MergeableRegister<TMergeable> where TMergeable : IMergeable
    {
        private IMergePolicy<TMergeable> _mergePolicy;
        private List<TMergeable> _entries;
        private MergeableTree _tree;
    }

    public interface IMergeable
    {
        public Vector3 Position { get; }
        public float Quantity { get; }
    }

    public interface IMergePolicy<TMergeable> where TMergeable : IMergeable
    {
        public bool CanBeMerged(TMergeable a, TMergeable b);
    }

    public interface IMergeStrategy<TMergeable> where TMergeable : IMergeable
    {
        public TMergeable Merge(TMergeable a, TMergeable b);
    }

    public interface IWeightingPolicy<in TMergeable> where TMergeable : IMergeable
    {
        public float GetWeight(TMergeable mergeable);
        public float GetTotalWeight(TMergeable a, TMergeable b);
        public (float aWeight, float bWeight, float totalWeight) GetWeights(TMergeable a, TMergeable b);
    }
}