using EggCentric.Sensors;
using EggCentric.ValueProviders.DataContainers;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TreeTest : MonoBehaviour
{
    [SerializeField] private Droplet _dropletPrefab;

    [SerializeField] private TreeParameters<TreeSettings> _parameters;
    [SerializeField] private int _dropletCount;
    [SerializeField] private bool _isSpawning;
    [SerializeField] private Vector3 _center;
    [SerializeField] private Vector3 _extents;
    [SerializeField] private ProbeSettings3D _probeSettings;

    private MergeableTree _tree;
    private NodeBounds _bounds;
    private List<DropletTreeItem> _droplets;
    private List<DropletTreeItem> _intersections;
    private HashSet<(DropletTreeItem a, DropletTreeItem b)> _pairs;
    private Dictionary<DropletTreeItem, int> _dictionary;
    private Register<DropletTreeItem> _register;

    private void Awake()
    {
        _bounds = new NodeBounds(_center, _extents);

        var settings = _parameters.Bake();
        _tree = new MergeableTree(settings, _bounds);
        _intersections = new List<DropletTreeItem>();
        _droplets = new List<DropletTreeItem>();
        _register = new Register<DropletTreeItem>();
        _register.OnItemEntry += Add;
        _register.OnItemExit += Remove;
        _pairs = new HashSet<(DropletTreeItem a, DropletTreeItem b)>();
        _dictionary = new Dictionary<DropletTreeItem, int>();

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
            _tree.Update(droplet);
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
        _tree?.DrawBounds(Color.green);
        _tree?.DrawItems(Color.yellow, 0.05f);
    }

    private void HandleInstersections(DropletTreeItem treeItem)
    {
        var intersections = GetAllFor(treeItem);
        foreach(var intersection in intersections)
        {
            if(intersection.Droplet == treeItem.Droplet)
                continue;

            var offset = intersection.Position - treeItem.Position;
            var direction = offset.normalized;

            Debug.DrawLine(treeItem.Position, intersection.Position, Color.red);
            Debug.DrawLine(treeItem.Position, treeItem.Position + direction * treeItem.Radius, Color.green);

            if (offset.sqrMagnitude < treeItem.Radius * treeItem.Radius)
            {
                var pair = Normalize(treeItem, intersection);
                if(_pairs.Contains(pair))
                    continue;

                _pairs.Add(pair);
                _dictionary[pair.lhs] = _dictionary.GetValueOrDefault(pair.lhs) + 1;
                _dictionary[pair.rhs] = _dictionary.GetValueOrDefault(pair.rhs) + 1;
            }
        }
    }

    private void HandlePairs(IEnumerable<(DropletTreeItem a, DropletTreeItem b)> pairs)
    {
        foreach (var pair in pairs)
        {
            if (!TryMerge(pair.a, pair.b, out var merged))
                continue;

            var item = CreateItem(merged);
            _droplets.Add(item);
        }
    }

    private void CleanUp()
    {
        for (int i = _droplets.Count - 1; i >= 0; i--)
        {
            if (_droplets[i].Droplet.Weight <= 0f)
                _droplets.RemoveAt(i);
        }
    }

    private bool TryMerge(DropletTreeItem lhs, DropletTreeItem rhs, out Droplet merged)
    {
        merged = default;

        var lhsDivisions = _dictionary[lhs];
        var rhsDivisions = _dictionary[rhs];
        var lhsRelativeWeight = lhs.Droplet.Weight / lhsDivisions;
        var rhsRelativeWeight = rhs.Droplet.Weight / rhsDivisions;
        var totalWeight = lhsRelativeWeight + rhsRelativeWeight;

        _dictionary[lhs] = lhsDivisions - 1;
        _dictionary[rhs] = rhsDivisions - 1;

        if (totalWeight < lhs.Droplet.Weight || totalWeight < rhs.Droplet.Weight)
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

        lhs.Droplet.SetValue(lhs.Droplet.Weight - lhsRelativeWeight);
        rhs.Droplet.SetValue(rhs.Droplet.Weight - rhsRelativeWeight);

        merged = droplet;
        return true;
    }

    private List<DropletTreeItem> GetAllFor(DropletTreeItem treeItem)
    {
        _intersections?.Clear();
        _tree.GetOverlaps(treeItem.Position, treeItem.Radius, _intersections);

        return _intersections;
    }

    private IEnumerable<DropletTreeItem> SpawnDroplets(int dropletCount)
    {
        for (int i = 0; i < _dropletCount; i++)
        {
            var spawnPos = new Vector3(Random.Range(_bounds.Min.x, _bounds.Max.x), Random.Range(_bounds.Min.y + 1, _bounds.Max.y), Random.Range(_bounds.Min.z, _bounds.Max.z));
            var spawnWeight = Random.Range(0.1f, 1f);
            var droplet = Instantiate(_dropletPrefab, spawnPos, Quaternion.identity);
            droplet.SetValue(spawnWeight);

            yield return CreateItem(droplet);
        }
    }

    private void Remove(DropletTreeItem item)
    {
        _tree.Remove(item);
        Destroy(item.Droplet.gameObject);
    }

    private void Add(DropletTreeItem item)
    {
        _tree.Insert(item);
    }

    private IEnumerable<DropletTreeItem> CreateTreeItems(IEnumerable<Droplet> droplets)
    {
        foreach (var droplet in droplets)
            yield return CreateItem(droplet);
    }

    private DropletTreeItem CreateItem(Droplet droplet) => new DropletTreeItem(droplet, _probeSettings);

    private (DropletTreeItem lhs, DropletTreeItem rhs) Normalize(DropletTreeItem lhs, DropletTreeItem rhs)
    {
        if (RuntimeHelpers.GetHashCode(lhs) < RuntimeHelpers.GetHashCode(rhs))
            return (lhs, rhs);

        if (RuntimeHelpers.GetHashCode(lhs) > RuntimeHelpers.GetHashCode(rhs))
            return (rhs, lhs);

        return default;
    }

    private void RegisterDroplets(IEnumerable<DropletTreeItem> droplets)
    {
        foreach (var droplet in droplets)
            _droplets.Add(droplet);

        _register.ProcessEntry(_droplets);
    }

    private float GetTotalWeight(IEnumerable<DropletTreeItem> items)
    {
        var totalWeight = 0f;
        foreach (var item in items)
            totalWeight += item.Droplet.Weight;

        return totalWeight;
    }
}

public class MergeableTree : Octree<DropletTreeItem>
{
    public MergeableTree(TreeSettings settings, NodeBounds bounds) : base(settings, bounds)
    {
    }
}