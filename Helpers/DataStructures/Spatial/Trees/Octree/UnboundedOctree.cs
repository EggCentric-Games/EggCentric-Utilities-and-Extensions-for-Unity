using EggCentric.Entities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace EggCentric.DataStructures.Trees
{
    public struct UnboundedOctreeSettings
    {
        public int NodeCapacity;
        public int MaxDepth;
        public float MinSize;
    }

    public interface IUnboundedOctreeHandle<TItem> : IGridHandle<TItem> where TItem : ISpatialEntity
    {

    }

    public class UnboundedOctreeNode<TItem> where TItem : ISpatialEntity
    {
        public Vector3 Center => _nodeBounds.Center;
        public Vector3 Extents => _nodeBounds.Extents;

        private UnboundedOctreeSettings _settings;

        private NodeBounds _nodeBounds;
        private UnboundedOctreeNode<TItem>[] _childs;
        private List<TItem> _items;

        public UnboundedOctreeNode(NodeBounds bounds, in UnboundedOctreeSettings settings)
        {
            _items = new List<TItem>();

            _nodeBounds = bounds;
            _settings = settings;
        }

        public void Join(UnboundedOctreeNode<TItem> node)
        {
            if (_childs == null)
                _childs = new UnboundedOctreeNode<TItem>[8];

            var childCenter = node._nodeBounds.Center;
            var center = _nodeBounds.Center;
            var id = (childCenter.x, childCenter.y, childCenter.z).PosToId((center.x, center.y, center.z));
            _childs[id] = node;
        }

        public bool Insert(TItem item, int depth)
        {
            if(!_nodeBounds.Contains(item.Position))
                return false;

            InsertItem(item, depth);
            FinalizeItemAddition(depth);
            return true;
        }

        public void Insert(IEnumerable<TItem> items, int depth)
        {
            foreach(var item in items)
                if (_nodeBounds.Contains(item.Position))
                    InsertItem(item, depth);

            FinalizeItemAddition(depth);
        }

        private void InsertItem(TItem item, int depth)
        {
            var id = (item.Position.x, item.Position.y, item.Position.z).PosToId((_nodeBounds.Center.x, _nodeBounds.Center.y, _nodeBounds.Center.z));

            if (_childs?[id] != null)
            {
                _childs[id].Insert(item, depth + 1);
                return;
            }

            _items.Add(item);
        }

        private void RemoveItem(TItem item)
        {
            _items.Remove(item);
        }

        private void FinalizeItemAddition(int depth)
        {
            if (IsSplitRequired() && CanBeSplit(depth))
                Split(depth);
        }

        private void Split(int depth)
        {
            if(_childs == null)
                _childs = new UnboundedOctreeNode<TItem>[8];

            var itemsCount = _items.Count;
            for(int i = itemsCount - 1; i >= 0; i--)
            {
                var item = _items[i];
                var id = (item.Position.x, item.Position.y, item.Position.z).PosToId((_nodeBounds.Center.x, _nodeBounds.Center.y, _nodeBounds.Center.z));
                var child = _childs[id] != null ? _childs[id] : SpawnChild(id);
                MoveToChild(item, child, depth);
            }
        }

        private bool MoveToChild(TItem item, UnboundedOctreeNode<TItem> node, int depth)
        {
            var isInsertionSuccesful = node.Insert(item, depth + 1);
            if(!isInsertionSuccesful)
                return false;

            RemoveItem(item);
            return true;
        }

        private void Merge()
        {
            if(_childs == null)
                return;

            foreach (var child in _childs)
            {
                if (child == null)
                    continue;

                Merge(child);
            }
        }

        private void Merge(UnboundedOctreeNode<TItem> child)
        {
            child.Merge();
            foreach (var item in child._items)
                _items.Add(item);

            var childCenter = child._nodeBounds.Center;
            var center = _nodeBounds.Center;
            var id = (childCenter.x, childCenter.y, childCenter.z).PosToId((center.x, center.y, center.z));
            _childs[id] = null;
        }

        private bool IsSplitRequired() => _items.Count > _settings.NodeCapacity;
        private bool CanBeSplit(int depth)
        {
            var maxSize = Mathf.Max(_nodeBounds.Size.x, _nodeBounds.Size.y, _nodeBounds.Size.z);
            return maxSize >= _settings.MinSize * 2f && depth < _settings.MaxDepth;
        }

        private UnboundedOctreeNode<TItem> SpawnChild(int id)
        {
            var child = CreateChild(id);
            _childs[id] = child;

            return child;
        }

        private UnboundedOctreeNode<TItem> CreateChild(int id)
        {
            var x = (id & 1) == 1 ? 1 : -1;
            var y = (id & 2) == 2 ? 1 : -1;
            var z = (id & 4) == 4 ? 1 : -1;
            var posMultiplier = new Vector3(x, y, z);

            var centerOffset = _nodeBounds.Extents;
            centerOffset.Scale(posMultiplier);
            var childCenter = _nodeBounds.Center + centerOffset * 0.5f;
            var childExtents = _nodeBounds.Extents * 0.5f;

            NodeBounds childBounds = new NodeBounds(childCenter, childExtents);
            return new UnboundedOctreeNode<TItem>(childBounds, in _settings);
        }
    }

    public class UnboundedOctreeRoot<TItem> where TItem : ISpatialEntity
    {
        public IReadOnlyList<UnboundedOctreeNode<TItem>> Nodes => _nodes;

        private readonly UnboundedOctreeSettings _settings;
        private readonly UnboundedOctreeNode<TItem>[] _nodes;
        private NodeBounds _nodeBounds;

        public UnboundedOctreeRoot(float size, in UnboundedOctreeSettings settings)
        {
            _nodes = new UnboundedOctreeNode<TItem>[8];
            _nodeBounds = new NodeBounds(Vector3.zero, Vector3.one * size);

            CreateInitialNodes();
        }

        public void Expand()
        {
            _nodeBounds = new NodeBounds(Vector3.zero, _nodeBounds.Extents * 2f);

            for (int i = 0; i < 8; i++)
            {
                var rootNode = _nodes[i];
                var newRootNode = new UnboundedOctreeNode<TItem>(new NodeBounds(rootNode.Center * 2f, rootNode.Extents * 2f), in _settings);
                newRootNode.Join(rootNode);
                _nodes[i] = newRootNode;
            }
        }

        public bool Contains(Vector3 point) => _nodeBounds.Contains(point);

        private void CreateInitialNodes()
        {
            for (int id = 0; id < 8; id++)
                _nodes[id] = CreateChild(id);
        }

        private UnboundedOctreeNode<TItem> CreateChild(int id)
        {
            var multiplier = id.ToMultiplier();
            var centerOffset = _nodeBounds.Extents;
            centerOffset.Scale(new Vector3(multiplier.x, multiplier.y, multiplier.z));
            var childCenter = _nodeBounds.Center + centerOffset * 0.5f;
            var childExtents = _nodeBounds.Extents * 0.5f;

            NodeBounds childBounds = new NodeBounds(childCenter, childExtents);
            return new UnboundedOctreeNode<TItem>(childBounds, in _settings);
        }
    }

    public class UnboundedOctree<TItem> : ISpatialStructure<TItem> where TItem : ISpatialEntity
    {
        private readonly UnboundedOctreeSettings _settings;
        private UnboundedOctreeRoot<TItem> _root;
        private readonly Dictionary<TItem, GridEntryHandle<TItem>> _registeredItems;

        public UnboundedOctree(float baseSize, UnboundedOctreeSettings settings)
        {
            _settings = settings;
        }

        public int FindAll(ICollection<TItem> result)
        {
            throw new NotImplementedException();
        }

        public int FindInRange(Vector3 point, float radius, ICollection<TItem> result)
        {
            throw new NotImplementedException();
        }

        public bool TryFindClosest(Vector3 point, out TItem result)
        {
            throw new NotImplementedException();
        }

        public bool Insert(TItem item)
        {
            while(!_root.Contains(item.Position))
                _root.Expand();

            var id = (item.Position.x, item.Position.y, item.Position.z).PosToId((0, 0, 0));
            _root.Nodes[id].Insert(item, 0);
            return CreateHandle(item);
        }

        public void Update(TItem handle)
        {
            if (!_registeredItems.TryGetValue(handle, out var entry))
                return;

            if (entry.Parent == null)
            {
                _registeredItems.Remove(handle);
                return;
            }

            if (entry.Parent.IsInBounds(entry.Item.Position))
                return;

            var key = ToKey(entry.Item);
            var cell = RetrieveCell(key);

            DetachItem(entry);

            cell.Insert(entry);
        }

        public void Remove(TItem handle)
        {
            throw new NotImplementedException();
        }

        private GridEntryHandle<TItem> CreateHandle(TItem item)
        {
            var handle = new GridEntryHandle<TItem>(item);
            _registeredItems[handle] = handle;

            return handle;
        }
    }

    public static class OctreeUtils
    {
        public static int PosToId(this (float x, float y, float z) position, (float x, float y, float z) origin)
        {
            int childIndex =
            (position.x >= origin.x ? 1 : 0) |
            (position.y >= origin.y ? 2 : 0) |
            (position.z >= origin.z ? 4 : 0);

            return childIndex;
        }

        public static (float x, float y, float z) ToMultiplier(this int id)
        {
            var x = (id & 1) == 1 ? 1 : -1;
            var y = (id & 2) == 2 ? 1 : -1;
            var z = (id & 4) == 4 ? 1 : -1;

            return (x, y, z);
        }
    }
}