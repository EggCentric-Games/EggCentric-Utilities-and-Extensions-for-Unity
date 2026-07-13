using EggCentric.QoL;
using EggCentric.ValueProviders.Random;
using System.Collections.Generic;
using UnityEngine;

namespace EggCentric.Randomization.Distributors
{
    public class WeightDistributor
    {
        private AnimationCurve _weightDistribution;
        private RandomValueProvider _randomValueProvider;

        public WeightDistributor(IRandom random, IDeviationStrategy deviationStrategy) => _randomValueProvider = new RandomValueProvider(0f, 1f, random, deviationStrategy);

        public void SetDistribution(AnimationCurve weightDistribution)
        {
            _weightDistribution = weightDistribution;
            _weightDistribution.RemapTo(Vector2.up, Vector2.up);
        }

        public float GetSample(float minWeightPerSample = 0f, float maxWeightPerSample = 1f)
        {
            var tSample = _randomValueProvider.GetNext();
            var tWeight = _weightDistribution.Evaluate(tSample);

            var sampleWeight = Mathf.Lerp(minWeightPerSample, maxWeightPerSample, tWeight);
            return sampleWeight;
        }

        public float[] RedistributeWeight(float weight, float minWeightPerSample = 0f, float maxWeightPerSample = 1f)
        {
            var remainingWeight = weight;
            var samples = new List<float>();

            while (remainingWeight > 0)
            {
                var sampleWeight = Mathf.Min(GetSample(minWeightPerSample, maxWeightPerSample), remainingWeight);
                remainingWeight -= sampleWeight;

                samples.Add(sampleWeight);
            }

            return samples.ToArray();
        }
    }
}
