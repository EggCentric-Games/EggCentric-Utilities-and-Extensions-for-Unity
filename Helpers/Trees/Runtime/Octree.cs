using EggCentric.ValueProviders.DataContainers;
using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NodeBounds
{
    public Vector3 Center;
    public Vector3 Size;

    public readonly Vector3 Extents;
    public readonly Vector3 Min;
    public readonly Vector3 Max;

    public NodeBounds(Vector3 center, Vector3 extents)
    {
        Center = center;
        Extents = extents;

        Size = extents * 2f;
        Min = center - Extents;
        Max = center + Extents;
    }

    public float SqrDistance(Vector3 position)
    {
        float dx = 0f;
        if (position.x < Min.x)
            dx = Min.x - position.x;
        else if (position.x > Max.x)
            dx = position.x - Max.x;

        float dy = 0f;
        if (position.y < Min.y)
            dy = Min.y - position.y;
        else if (position.y > Max.y)
            dy = position.y - Max.y;

        float dz = 0f;
        if (position.z < Min.z)
            dz = Min.z - position.z;
        else if (position.z > Max.z)
            dz = position.z - Max.z;

        return dx * dx + dy * dy + dz * dz;
    }

    public bool Contains(Vector3 position) => position.x >= Min.x && position.x <= Max.x && position.y >= Min.y && position.y <= Max.y && position.z >= Min.z && position.z <= Max.z;
}

public interface ITree<TItem> where TItem : ITreeItem<TItem>
{
    public ITreeSettings Settings { get; }

    public void GetOverlaps(Vector3 position, float radius, List<TItem> result);
    public bool Insert(TItem item);
    public bool Update(TItem item);
    public bool Remove(TItem item);

    public void DrawBounds(Color color);
    public void DrawItems(Color color, float size = 0.1f);
}

public interface ITree<TNode, TItem> : ITree<TItem> where TNode : ITreeNode<TNode, TItem> where TItem : ITreeItem<TItem>
{
    public ITreeMergingPolicy<TNode, TItem> MergingPolicy { get; }

    public abstract TNode CreateNode(TNode parent, NodeBounds bounds);
}

public abstract class Tree<TNode, TItem> : ITree<TNode, TItem> where TNode : ITreeNode<TNode, TItem> where TItem : ITreeItem<TItem>
{
    public ITreeSettings Settings { get; }
    public ITreeMergingPolicy<TNode, TItem> MergingPolicy => _mergingPolicy;

    protected TNode root;
    private ITreeMergingPolicy<TNode, TItem> _mergingPolicy;

    public Tree(TreeSettings settings, NodeBounds bounds, ITreeMergingPolicy<TNode, TItem> mergingPolicy)
    {
        Settings = settings;
        _mergingPolicy = mergingPolicy;

        root = CreateRoot(bounds);
    }

    public void GetOverlaps(Vector3 position, float radius, List<TItem> result) => QueryNode(root, position, radius * radius, result);

    public bool Insert(TItem item) => root.Insert(item);

    public bool Update(TItem item)
    {
        var parent = item.Parent;
        if (parent == null)
            return false;

        if (item.Parent.Bounds.Contains(item.Position))
            return true;

        Remove(item);
        return InsertOrMoveUp(parent.Parent, item);
        //Insert(item);
    }

    private bool InsertOrMoveUp(ITreeNode<TItem> node, TItem item)
    {
        if (node == null)
            return false;

        if (node.Bounds.Contains(item.Position))
            node.Insert(item);
        else
            InsertOrMoveUp(node.Parent, item);

        return true;
    }

    public bool Remove(TItem item)
    {
        var parent = item.Parent;
        if(parent == null)
            return false;

        parent.Remove(item);
        return true;
    }

    public TNode CreateNode(TNode parent, NodeBounds bounds) => CreateNode(parent, bounds, parent != null ? parent.Depth + 1 : 0);

    protected abstract TNode CreateRoot(NodeBounds bounds);
    protected abstract TNode CreateNode(TNode parent, NodeBounds bounds, int depth);

    private void QueryNode(TNode node, Vector3 position, float radiusSqr, List<TItem> result)
    {
        if (!Intersects(node, position, radiusSqr))
            return;

        Vector3 center = node.Bounds.Center;

        float dx = position.x - center.x;
        float dy = position.y - center.y;
        float dz = position.z - center.z;

        var sqrDist = dx * dx + dy * dy + dz * dz;

        if (sqrDist <= node.MaxOffset)
            foreach (var obj in node.Items)
                if (Intersects(obj, position, radiusSqr))
                    result.Add(obj);

        if (node.Children == null)
            return;

        foreach (var child in node.Children)
            QueryNode(child, position, radiusSqr, result);
    }

    private bool Intersects(TNode node, Vector3 position, float radiusSqr) => node.Bounds.SqrDistance(position) < radiusSqr;
    private bool Intersects(TItem item, Vector3 position, float radiusSqr)
    {
        Vector3 itemPosition = item.Position;

        float dx = itemPosition.x - position.x;
        float dy = itemPosition.y - position.y;
        float dz = itemPosition.z - position.z;

        return dx * dx + dy * dy + dz * dz < radiusSqr;
    }

    public void DrawBounds(Color color) => root.DrawBounds(color);
    public void DrawItems(Color color, float size = 0.1f) => root.DrawItems(color, size);
}

public class Octree<TItem> : Tree<OctreeNode<TItem>, TItem> where TItem : ITreeItem<TItem>
{
    public Octree(TreeSettings settings, NodeBounds bounds, ITreeMergingPolicy<OctreeNode<TItem>, TItem> mergingPolicy) : base(settings, bounds, mergingPolicy)
    {
    }

    protected override OctreeNode<TItem> CreateRoot(NodeBounds bounds) => new OctreeNode<TItem>(this, null, bounds, 0);
    protected override OctreeNode<TItem> CreateNode(OctreeNode<TItem> parent, NodeBounds bounds, int depth) => new OctreeNode<TItem>(this, parent, bounds, depth);
}

public interface ITreeNode<TItem> where TItem : ITreeItem<TItem>
{
    public ITreeNode<TItem> Parent { get; }
    public NodeBounds Bounds { get; }
    public int Depth { get; }
    public bool IsLeaf { get; }

    public float MaxOffset { get; }

    public event Action<TItem> OnItemInserted;
    public event Action<TItem> OnItemRemoved;
    public event Action OnMergePerformed;

    public bool Insert(TItem item);
    public bool Remove(TItem item);

    public void DrawBounds(Color color);
    public void DrawItems(Color color, float size = 0.1f);
}

public interface ITreeNode<TNode, TItem> : ITreeNode<TItem> where TNode : ITreeNode<TNode, TItem> where TItem : ITreeItem<TItem>
{
    public ITree<TNode, TItem> Tree { get; }
    public new TNode Parent { get; }
    public IReadOnlyList<TNode> Children { get; }
    public IReadOnlyList<TItem> Items { get; }
}

public interface ITreeMergingPolicy<TNode, TItem> where TNode : ITreeNode<TNode, TItem> where TItem : ITreeItem<TItem>
{
    public bool IsSplitRequired(TNode node);
    public bool IsSplitAvailable(TNode node);
    public bool IsMergeAvailable(TNode node);
}

public abstract class TreeMergingPolicy<TSettings, TNode, TItem> : ITreeMergingPolicy<TNode, TItem> where TNode : ITreeNode<TNode, TItem> where TItem : ITreeItem<TItem>
{
    public TSettings Settings { get; }

    public TreeMergingPolicy(TSettings settings) => Settings = settings;

    public abstract bool IsSplitRequired(TNode node);
    public abstract bool IsMergeAvailable(TNode node);
    public abstract bool IsSplitAvailable(TNode node);
}

public class CommonMergingPolicy<TNode, TItem> : TreeMergingPolicy<TreeSettings, TNode, TItem> where TNode : ITreeNode<TNode, TItem> where TItem : ITreeItem<TItem>
{
    public CommonMergingPolicy(TreeSettings settings) : base(settings)
    {
    }

    public override bool IsSplitRequired(TNode node) => node.Items.Count > Settings.Capacity && node.IsLeaf;

    public override bool IsSplitAvailable(TNode node)
    {
        if (node.Depth >= Settings.MaxDepth)
            return false;

        return true;
    }

    public override bool IsMergeAvailable(TNode node)
    {
        if (node.IsLeaf)
            return true;

        var totalItemCount = 0;
        foreach (var child in node.Children)
            if (!IsMergeAvailable(child))
                return false;
            else
                totalItemCount += child.Items.Count;

        if (totalItemCount > Settings.Capacity)
            return false;

        return true;
    }
}

public abstract class TreeNode<TNode, TItem> : ITreeNode<TNode, TItem> where TNode : TreeNode<TNode, TItem> where TItem : ITreeItem<TItem>
{
    public ITree<TNode, TItem> Tree { get; }
    public TNode Parent => _parent;
    public NodeBounds Bounds => _bounds;
    public IReadOnlyList<TNode> Children => _children;
    public IReadOnlyList<TItem> Items => _items;
    public int Depth => _depth;
    public bool IsLeaf => _children == null;

    ITreeNode<TItem> ITreeNode<TItem>.Parent => Parent;

    public float MaxOffset => GetMaxOffset();
    private float _maxOffset; 
    private bool _isOffsetDirty;

    private TNode _parent;
    private TNode[] _children;
    private List<TItem> _items;
    private readonly NodeBounds _bounds;
    private readonly int _depth;

    public event Action<TItem> OnItemInserted;
    public event Action<TItem> OnItemRemoved;
    public event Action OnMergePerformed;

    public TreeNode(ITree<TNode, TItem> tree, TNode parent, NodeBounds bounds, int depth)
    {
        _bounds = bounds;
        Tree = tree;
        _parent = parent;
        _depth = depth;

        _items = new List<TItem>();
    }

    //private void Invalidate()
    //{
    //    _maxOffset.Invalidate();
    //    InvalidateParent();
    //}

    //private void InvalidateParent()
    //{
    //    if (_parent == null)
    //        return;

    //    _parent.Invalidate();
    //}

    private float GetMaxOffset()
    {
        if (!_isOffsetDirty)
            return _maxOffset;
        
        var newOffset = RecalculateMaxOffset();
        if (_maxOffset != newOffset && Parent != null)
            Parent._isOffsetDirty = true;
        
        _maxOffset = newOffset;
        _isOffsetDirty = false;

        return _maxOffset;
    }


    private float RecalculateMaxOffset()
    {
        var _maxOffset = 0f;
        foreach (var item in Items)
        {
            var sqrDistance = (item.Position - Bounds.Center).sqrMagnitude;
            if (sqrDistance > _maxOffset)
                _maxOffset = sqrDistance;
        }

        return _maxOffset;

        //var _largestItem = 0f;
        //foreach (var item in Items)
        //    if (item.Radius > _largestItem) _largestItem = item.Radius;

        //if (IsLeaf)
        //    return _largestItem;

        //foreach (var child in Children)
        //    if (child.GetClosest() > _largestItem)
        //        _largestItem = child.LargestItem;

        //return _largestItem;
    }

    public bool TrySplit()
    {
        if (!Tree.MergingPolicy.IsSplitAvailable((TNode)this))
            return false;

        Split();
        return true;
    }

    protected void Split()
    {
        _children = SplitNode();
        for (int i = 0; i < _children.Length; i++)
            _children[i] = Tree.CreateNode((TNode)this, GetBounds(i));

        var itemsSnapshot = new List<TItem>(_items);
        foreach (var item in itemsSnapshot)
        {
            if (!TryGetContainerFor(item, out var node))
                continue;

            node.Insert(item);
            RemoveItem(item);
        }
    }

    protected bool TryMerge()
    {
        if (!Tree.MergingPolicy.IsMergeAvailable((TNode)this))
            return false;

        Merge();
        return true;
    }

    protected void Merge()
    {
        if(IsLeaf)
            return;

        foreach (var child in _children)
            foreach (var item in child.Items)
                Assign(item);
        
        _children = null;
        OnMergePerformed?.Invoke();
        _parent?.TryMerge();
    }

    public bool Insert(TItem item)
    {
        if (!IsLeaf)
        {
            if(TryGetContainerFor(item, out var node))
                return node.Insert(item);
        }

        Assign(item);
        if (Tree.MergingPolicy.IsSplitRequired((TNode)this))
            TrySplit();

        return true;
    }

    public bool Remove(TItem item)
    {
        if(!RemoveItem(item))
            return false;

        item.AssignTo(null);
        OnItemRemoved?.Invoke(item);
        _isOffsetDirty = true;
        _parent?.TryMerge();

        return true;
    }

    public void DrawBounds(Color color)
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(Bounds.Center, Bounds.Size);

        if(!IsLeaf)
            foreach(var child in _children)
                child.DrawBounds(color);
    }

    public void DrawItems(Color color, float size = 0.1f)
    {
        Gizmos.color = color;
        foreach (var item in _items)
        {
            Gizmos.DrawSphere(item.Position, size);
            Debug.DrawLine(Bounds.Center, item.Position, color);
        }

        if (!IsLeaf)
            foreach (var child in _children)
                child.DrawItems(color, size);
    }

    protected abstract TNode[] SplitNode();
    protected abstract NodeBounds GetBounds(int index);

    protected abstract bool TryGetContainerFor(TItem item, out TNode node);

    private void Assign(TItem item)
    {
        _items.Add(item);
        item.AssignTo(this);
        _isOffsetDirty = true;
        OnItemInserted?.Invoke(item);
    }

    private bool RemoveItem(TItem item) => _items.Remove(item);
}

public interface ITreeParameters<TSettings> where TSettings : ITreeSettings
{
    public TSettings Bake();
}

[System.Serializable]
public class TreeParameters<TSettings> : ITreeParameters<TreeSettings>
{
    public int MaxDepth = 32;
    public int Capacity = 1;

    public TreeSettings Bake() => new TreeSettings(MaxDepth, Capacity);
}

public interface ITreeSettings
{

}

public class TreeSettings : ITreeSettings
{
    public readonly int MaxDepth;
    public readonly int Capacity;

    public TreeSettings(int maxDepth, int capacity)
    {
        MaxDepth = maxDepth;
        Capacity = capacity;
    }
}

public class OctreeNode<TItem> : TreeNode<OctreeNode<TItem>, TItem> where TItem : ITreeItem<TItem>
{

    public OctreeNode(ITree<OctreeNode<TItem>, TItem> tree, OctreeNode<TItem> parent, NodeBounds bounds, int depth) : base(tree, parent, bounds, depth)
    {
    }

    protected override OctreeNode<TItem>[] SplitNode() => new OctreeNode<TItem>[8];
    protected override NodeBounds GetBounds(int index)
    {
        Vector3 extents = Bounds.Extents / 2f;
        Vector3 offset = new Vector3(
                (index & 4) == 0 ? -1 : 1,
                (index & 2) == 0 ? -1 : 1,
                (index & 1) == 0 ? -1 : 1
            );

        var center = Bounds.Center + Vector3.Scale(offset, extents);
        return new NodeBounds(center, extents);
    }

    protected override bool TryGetContainerFor(TItem item, out OctreeNode<TItem> node)
    {
        var x = item.Position.x >= Bounds.Center.x ? 4 : 0;
        var y = item.Position.y >= Bounds.Center.y ? 2 : 0;
        var z = item.Position.z >= Bounds.Center.z ? 1 : 0;
        var index = x + y + z;

        var isIndexValid = index >= 0 && index < Children.Count;
        node = isIndexValid ? Children[index] : default;
        return isIndexValid;
    }
}

public interface ITreeItem<TSelf> where TSelf : ITreeItem<TSelf>
{
    public ITreeNode<TSelf> Parent { get; }
    public Vector3 Position { get; }

    public void MoveTo(Vector3 position);
    public void AssignTo(ITreeNode<TSelf> parent);
}