using EggCentric.DataStructures;
using EggCentric.Sensors;
using EggCentric.ValueProviders.DataContainers;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HashMapTest : MonoBehaviour
{
    [SerializeField] private Droplet _dropletPrefab;

    [SerializeField] private float _cellSize;
    [SerializeField] private int _dropletCount;
    [SerializeField] private bool _isSpawning;
    [SerializeField] private Vector3 _center;
    [SerializeField] private Vector3 _extents;
    [SerializeField] private ProbeSettings3D _probeSettings;

    private HashGrid<Droplet> _grid;
    private NodeBounds _bounds;
    private List<Droplet> _droplets;
    private List<Droplet> _intersections;
    private HashSet<(Droplet a, Droplet b)> _pairs;
    private Dictionary<Droplet, int> _dictionary;
    private Register<Droplet> _register;

    private void Awake()
    {
        _bounds = new NodeBounds(_center, _extents);

        _grid = new HashGrid<Droplet>(_cellSize);
        _intersections = new List<Droplet>();
        _droplets = new List<Droplet>();
        _register = new Register<Droplet>();
        _register.OnItemExit += Remove;
        _pairs = new HashSet<(Droplet a, Droplet b)>();
        _dictionary = new Dictionary<Droplet, int>();

        var droplets = SpawnDroplets(_dropletCount);
        RegisterDroplets(droplets);
    }

    private void Update()
    {
        if (_isSpawning)
        {
            var droplets = SpawnDroplets(_dropletCount);
            RegisterDroplets(droplets);
        }

        foreach (var droplet in _droplets)
        {
            droplet.Tick(Time.deltaTime);
            _grid.Update(droplet);
        }

        _pairs?.Clear();
        _dictionary.Clear();
        foreach (var droplet in _droplets.ToList())
            HandleInstersections(droplet);

        HandlePairs(_pairs);
        CleanUp();

        _register.ProcessEntry(_droplets);

        //var weight = GetTotalWeight(_droplets);
        //Debug.Log($"Total weight: {weight}");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        foreach (var node in _grid.Nodes)
            Gizmos.DrawWireCube(node.Center, Vector3.one * _cellSize);

        //_grid?.DrawBounds(Color.green);
        //_grid?.DrawItems(Color.yellow, 0.05f);
    }

    private void HandleInstersections(Droplet droplet)
    {
        var intersections = GetAllFor(droplet);
        foreach(var intersection in intersections)
        {
            if(intersection == droplet)
                continue;

            var offset = intersection.Position - droplet.Position;
            var direction = offset.normalized;

            Debug.DrawLine(droplet.Position, intersection.Position, Color.red);
            Debug.DrawLine(droplet.Position, droplet.Position + direction * droplet.Radius, Color.green);

            if (offset.sqrMagnitude < droplet.Radius * droplet.Radius)
            {
                var pair = Normalize(droplet, intersection);
                if(_pairs.Contains(pair))
                    continue;

                _pairs.Add(pair);
                _dictionary[pair.lhs] = _dictionary.GetValueOrDefault(pair.lhs) + 1;
                _dictionary[pair.rhs] = _dictionary.GetValueOrDefault(pair.rhs) + 1;
            }
        }
    }

    private void HandlePairs(IEnumerable<(Droplet a, Droplet b)> pairs)
    {
        foreach (var pair in pairs)
        {
            if (!TryMerge(pair.a, pair.b, out var merged))
                continue;

            var handle = _grid.Insert(merged);
            _droplets.Add(merged);
        }
    }

    private void CleanUp()
    {
        for (int i = _droplets.Count - 1; i >= 0; i--)
        {
            if (_droplets[i].Weight <= 0f)
                _droplets.RemoveAt(i);
        }
    }

    private bool TryMerge(Droplet lhs, Droplet rhs, out Droplet merged)
    {
        merged = default;

        var lhsDivisions = _dictionary[lhs];
        var rhsDivisions = _dictionary[rhs];
        var lhsRelativeWeight = lhs.Weight / lhsDivisions;
        var rhsRelativeWeight = rhs.Weight / rhsDivisions;
        var totalWeight = lhsRelativeWeight + rhsRelativeWeight;

        _dictionary[lhs] = lhsDivisions - 1;
        _dictionary[rhs] = rhsDivisions - 1;

        if (totalWeight < lhs.Weight || totalWeight < rhs.Weight)
        {
            //Debug.Log($"Discarded: {lhs.Droplet.Weight} ({lhsDivisions}) and {rhs.Droplet.Weight} ({rhsDivisions})\nRelative weights: {lhsRelativeWeight} and {rhsRelativeWeight} - Total:{totalWeight}");
            return false;
        }

        var offset = rhs.Position - lhs.Position;
        var ratio = rhsRelativeWeight / totalWeight;
        var mergePosition = lhs.Position + offset * ratio;

        var droplet = Instantiate(_dropletPrefab, mergePosition, Quaternion.identity);
        droplet.SetValue(totalWeight);

        //Debug.Log($"Merged: {lhs.Droplet.Weight} ({lhsDivisions}) and {rhs.Droplet.Weight} ({rhsDivisions})\nRelative weights: {lhsRelativeWeight} and {rhsRelativeWeight} - Total:{totalWeight}\nOffset: {offset}\nRatio: {ratio}\nMerged offset: {offset * ratio}");

        lhs.SetValue(lhs.Weight - lhsRelativeWeight);
        rhs.SetValue(rhs.Weight - rhsRelativeWeight);

        merged = droplet;
        return true;
    }

    private List<Droplet> GetAllFor(Droplet handle)
    {
        _intersections?.Clear();
        _grid.FindInRange(handle.Position, handle.Radius, _intersections);

        return _intersections;
    }

    private IEnumerable<Droplet> SpawnDroplets(int dropletCount)
    {
        for (int i = 0; i < _dropletCount; i++)
        {
            var spawnPos = new Vector3(Random.Range(_bounds.Min.x, _bounds.Max.x), Random.Range(_bounds.Min.y + 1, _bounds.Max.y), Random.Range(_bounds.Min.z, _bounds.Max.z));
            var spawnWeight = Random.Range(0.1f, 1f);
            var droplet = Instantiate(_dropletPrefab, spawnPos, Quaternion.identity);
            droplet.SetValue(spawnWeight);

            yield return droplet;
        }
    }

    private void Remove(Droplet handle)
    {
        _grid.Remove(handle);
        _droplets.Remove(handle);
        Destroy(handle.gameObject);
    }

    private (Droplet lhs, Droplet rhs) Normalize(Droplet lhs, Droplet rhs)
    {
        if (RuntimeHelpers.GetHashCode(lhs) < RuntimeHelpers.GetHashCode(rhs))
            return (lhs, rhs);

        if (RuntimeHelpers.GetHashCode(lhs) > RuntimeHelpers.GetHashCode(rhs))
            return (rhs, lhs);

        return default;
    }

    private void RegisterDroplets(IEnumerable<Droplet> droplets)
    {
        foreach (var droplet in droplets)
        {
            var handle = _grid.Insert(droplet);
            _droplets.Add(droplet);
        }

        _register.ProcessEntry(_droplets);
    }

    private float GetTotalWeight(IEnumerable<Droplet> handles)
    {
        var totalWeight = 0f;
        foreach (var handle in handles)
            totalWeight += handle.Weight;

        return totalWeight;
    }
}