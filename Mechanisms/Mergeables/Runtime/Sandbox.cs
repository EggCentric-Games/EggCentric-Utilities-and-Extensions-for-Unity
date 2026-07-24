using EggCentric.Randomization;
using EggCentric.Randomization.Distributors;
using UnityEngine;

public class Sandbox : MonoBehaviour
{
    [SerializeField] private Droplet _blobPrefab;
    [SerializeField] private WeightDistributor _weightDistributor;

    [SerializeField] private float _weight;
    [SerializeField] private float _minDropletWeight;
    [SerializeField] private float _maxDropletWeight;
    [SerializeField] private AnimationCurve _weightDistribution;
    [SerializeField] private RandomType _randomType;

    private float _lastRadius;
    private float _currentOffset;

    private void Awake() => PerformTest();

    private void PerformTest()
    {
        _weightDistributor = new WeightDistributor(RandomTypes.ByType(_randomType), new EdgeDeviation());

        _weightDistributor.SetDistribution(_weightDistribution);
        var weights = _weightDistributor.RedistributeWeight(_weight, _minDropletWeight, _maxDropletWeight);
        foreach (var weight in weights)
        {
            _currentOffset += _lastRadius;

            var blob = SpawnBlob();
            blob.SetValue(weight);

            _currentOffset += blob.Radius + 0.1f;

            _lastRadius = blob.Radius;
        }
    }

    private Droplet SpawnBlob()
    {
        var newBlob = Instantiate(_blobPrefab, Vector3.right * _currentOffset, Quaternion.identity);
        return newBlob;
    }
}
