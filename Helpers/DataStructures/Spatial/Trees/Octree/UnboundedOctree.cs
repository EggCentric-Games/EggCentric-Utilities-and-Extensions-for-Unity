using EggCentric.Entities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace EggCentric.DataStructures.Trees
{
    public interface IUnboundedOctreeHandle<TItem> : IGridHandle<TItem> where TItem : ISpatialEntity
    {

    }

    public class UnboundedOctreeNode<TItem> : ISpatialNode<TItem> where TItem : ISpatialEntity
    {
        public UnboundedOctreeNode<TItem> Parent { get; private set; }
        public IReadOnlyList<UnboundedOctreeNode<TItem>> Children => _childs;
        public IReadOnlyCollection<EntryHandle<UnboundedOctreeNode<TItem>, TItem>> Items => _items;
        public Vector3 Center => _nodeBounds.Center;
        public Vector3 Extents => _nodeBounds.Extents;
        public int Depth => Parent != null ? Parent.Depth + 1 : 0;

        private readonly UnboundedOctreeSettings _settings;
        private readonly NodeBounds _nodeBounds;
        private readonly List<EntryHandle<UnboundedOctreeNode<TItem>, TItem>> _items;

        public float MaxOffset => GetMaxOffset();
        private float _maxOffset;
        private bool _isOffsetDirty;
        private UnboundedOctreeNode<TItem>[] _childs;

        public UnboundedOctreeNode(NodeBounds bounds, in UnboundedOctreeSettings settings, UnboundedOctreeNode<TItem> parent = default)
        {
            _items = new List<EntryHandle<UnboundedOctreeNode<TItem>, TItem>>();

            _nodeBounds = bounds;
            _settings = settings;
            Parent = parent;
        }

        public void Draw()
        {
            Gizmos.color = Color.green / (Depth + 1);
            Gizmos.DrawWireCube(Center, Extents * 2f);

            foreach(var handle in Items)
                Debug.DrawLine(Center, handle.Item.Position, Color.yellow);

            if(_childs != null)
                foreach(var child in _childs)
                    if(child != null)
                        child.Draw();
        }

        public bool Contains(Vector3 point) => _nodeBounds.Contains(point);

        public float Distance(Vector3 point) => Mathf.Sqrt(_nodeBounds.SqrDistance(point));

        public float SqrDistance(Vector3 point) => _nodeBounds.SqrDistance(point);

        public bool Insert(EntryHandle<UnboundedOctreeNode<TItem>, TItem> handle)
        {
            if(!_nodeBounds.Contains(handle.Item.Position))
                return false;

            if (!InsertItem(handle))
                return false;

            if (IsSplitRequired(this))
                TrySplit();

            return true;
        }

        public bool Update(EntryHandle<UnboundedOctreeNode<TItem>, TItem> handle)
        {
            if(!Contains(handle.Item.Position))
                return false;

            if(_childs == null)
            {
                if (IsSplitRequired(this))
                    TrySplit();
                return true;
            }

            var item = handle.Item;
            var id = (item.Position.x, item.Position.y, item.Position.z).PosToId((_nodeBounds.Center.x, _nodeBounds.Center.y, _nodeBounds.Center.z));
            var child = _childs[id];

            if(child == null)
            {
                if (IsSplitRequired(this))
                    TrySplit();
                return true;
            }

            if (handle.Host == child)
                return child.Update(handle);

            handle.Host.Remove(handle);
            child.Insert(handle);
            return true;
        }

        public bool Remove(EntryHandle<UnboundedOctreeNode<TItem>, TItem> handle)
        {
            var index = _items.IndexOf(handle);
            var wasRemovalSuccessful = index >= 0 && RemoveItem(index);

            if (wasRemovalSuccessful)
                TryMerge();

            return wasRemovalSuccessful;
        }

        public void Join(UnboundedOctreeNode<TItem> node)
        {
            if (_childs == null)
                _childs = new UnboundedOctreeNode<TItem>[8];

            var childCenter = node._nodeBounds.Center;
            var center = _nodeBounds.Center;
            var id = (childCenter.x, childCenter.y, childCenter.z).PosToId((center.x, center.y, center.z));

            node.Parent = this;
            _childs[id] = node;
        }

        private bool TryMerge()
        {
            if (!IsMergeAvailable(this, out _))
                return false;

            Merge();
            Parent?.TryMerge();
            return true;
        }

        private bool TrySplit()
        {
            if (!IsSplitAvailable(this))
                return false;

            Split();
            return true;
        }

        private void Split()
        {
            if(_childs == null)
                _childs = new UnboundedOctreeNode<TItem>[8];

            var itemsCount = _items.Count;
            for(int i = itemsCount - 1; i >= 0; i--)
            {
                var handle = _items[i];
                var item = handle.Item;
                var id = (item.Position.x, item.Position.y, item.Position.z).PosToId((_nodeBounds.Center.x, _nodeBounds.Center.y, _nodeBounds.Center.z));
                var child = _childs[id] != null ? _childs[id] : SpawnChild(id);
                MoveToChild(child, i);
            }
        }

        private void Merge()
        {
            if(_childs == null)
                return;

            foreach (var child in _childs)
            {
                if (child == null)
                    continue;

                Consume(child);
            }
            _childs = null;
        }

        private void Consume(UnboundedOctreeNode<TItem> child)
        {
            child.Merge();

            for (int i = child._items.Count - 1; i >= 0; i--)
            {
                var handle = child._items[i];
                child.RemoveItem(i);
                AssignItem(handle);
            }

            DetachChild(child);
        }

        private bool MoveToChild(UnboundedOctreeNode<TItem> node, int itemId)
        {
            var handle = _items[itemId];
            var isInsertionSuccesful = node.Insert(handle);
            if (isInsertionSuccesful)
                RemoveItem(itemId);

            return isInsertionSuccesful;
        }

        private bool InsertItem(EntryHandle<UnboundedOctreeNode<TItem>, TItem> handle)
        {
            var item = handle.Item;
            var id = (item.Position.x, item.Position.y, item.Position.z).PosToId((_nodeBounds.Center.x, _nodeBounds.Center.y, _nodeBounds.Center.z));

            if (_childs?[id] != null)
                return _childs[id].Insert(handle);

            AssignItem(handle);
            return true;
        }

        private bool RemoveItem(int id)
        {
            var handle = _items[id];
            if(handle == null)
                return false;

            _items.RemoveAt(id);
            if(handle.Host == this)
                handle.AssignTo(null);

            _isOffsetDirty = true;
            return true;
        }

        private void AssignItem(EntryHandle<UnboundedOctreeNode<TItem>, TItem> handle)
        {
            _items.Add(handle);
            _isOffsetDirty = true;
            handle.AssignTo(this);
        }

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
            foreach (var handle in _items)
            {
                var sqrDistance = (handle.Item.Position - _nodeBounds.Center).sqrMagnitude;
                if (sqrDistance > _maxOffset)
                    _maxOffset = sqrDistance;
            }

            return _maxOffset;
        }

        private bool IsMergeAvailable(UnboundedOctreeNode<TItem> node, out int mergedItemsCount)
        {
            mergedItemsCount = node.Items.Count;

            if (node._childs == null)
                return true;

            foreach (var child in node._childs)
            {
                if(child == null)
                    continue;

                if (!IsMergeAvailable(child, out var childItemsCount))
                    return false;

                mergedItemsCount += childItemsCount;

                if (mergedItemsCount > node._settings.NodeCapacity)
                    return false;
            }

            return true;
        }

        private bool IsSplitRequired(UnboundedOctreeNode<TItem> node) => node._items.Count > node._settings.NodeCapacity;
        private bool IsSplitAvailable(UnboundedOctreeNode<TItem> node)
        {
            var maxSize = Mathf.Max(node._nodeBounds.Size.x, node._nodeBounds.Size.y, node._nodeBounds.Size.z);
            return maxSize >= node._settings.MinSize * 2f && node.Depth < node._settings.MaxDepth;
        }

        private void DetachChild(UnboundedOctreeNode<TItem> child)
        {
            if (_childs == null)
                return;

            var childCenter = child._nodeBounds.Center;
            var center = _nodeBounds.Center;
            var id = (childCenter.x, childCenter.y, childCenter.z).PosToId((center.x, center.y, center.z));

            if (_childs[id] != child)
                return;

            _childs[id] = null;
        }

        private UnboundedOctreeNode<TItem> SpawnChild(int id)
        {
            var child = CreateChild(id);
            child.Parent = this;
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

    public class UnboundedOctree<TItem> : ISpatialStructure<TItem> where TItem : ISpatialEntity
    {
        private readonly UnboundedOctreeSettings _settings;
        private readonly UnboundedOctreeRoot<TItem> _root;
        private readonly Dictionary<TItem, EntryHandle<UnboundedOctreeNode<TItem>, TItem>> _registeredItems;

        public UnboundedOctree(float baseSize, UnboundedOctreeSettings settings)
        {
            if (baseSize <= 0)
            {
                Debug.LogWarning($"Base size of an Octree must be greater than 0. Current size is: {baseSize}. Set to 0.01f.");
                baseSize = 0.01f;
            }

            _settings = settings;
            _root = new UnboundedOctreeRoot<TItem>(baseSize, in _settings);
            _registeredItems = new Dictionary<TItem, EntryHandle<UnboundedOctreeNode<TItem>, TItem>>();
        }

        public void Draw() => _root.Draw();

        public int FindAll(ICollection<TItem> result)
        {
            foreach (var item in _registeredItems.Keys)
                result.Add(item);

            return _registeredItems.Count;
        }

        public int FindInRange(Vector3 point, float radius, ICollection<TItem> result)
        {
            foreach(var node in _root.Nodes)
                QueryNode(node, point, radius * radius, result);

            return result.Count;
        }

        public bool TryFindClosest(Vector3 point, out TItem result)
        {
            throw new NotImplementedException();
        }

        public bool Insert(TItem item)
        {
            if(_registeredItems.ContainsKey(item))
                return false;

            while(!_root.Contains(item.Position))
                _root.Expand();

            var id = (item.Position.x, item.Position.y, item.Position.z).PosToId((0, 0, 0));
            var handle = CreateHandle(item);
            return _root.Nodes[id].Insert(handle);
        }

        public bool Update(TItem item)
        {
            if (!_registeredItems.TryGetValue(item, out var entry))
                return false;

            var host = entry.Host;
            if (host == null)
            {
                RemoveItem(entry.Item);
                return false;
            }

            //host.Remove(entry);
            //var id = (item.Position.x, item.Position.y, item.Position.z).PosToId((0, 0, 0));
            //_root.Nodes[id].Insert(entry);
            //return true;

            if (host.Update(entry))
                return true;

            host.Remove(entry);
            return InsertOrMoveUp(host.Parent, entry);
        }

        public bool Remove(TItem item)
        {
            if(!_registeredItems.TryGetValue(item, out var handle))
                return false;

            if (handle.Host != null)
                handle.Host.Remove(handle);

            RemoveItem(item);
            return true;
        }

        private bool InsertOrMoveUp(UnboundedOctreeNode<TItem> node, EntryHandle<UnboundedOctreeNode<TItem>, TItem> handle)
        {
            if (node == null)
            {
                while (!_root.Contains(handle.Item.Position))
                    _root.Expand();

                var id = (handle.Item.Position.x, handle.Item.Position.y, handle.Item.Position.z).PosToId((0, 0, 0));
                _root.Nodes[id].Insert(handle);
                return true;
            }

            if (node.Contains(handle.Item.Position))
                return node.Insert(handle);

            return InsertOrMoveUp(node.Parent, handle);
        }

        private void QueryNode(UnboundedOctreeNode<TItem> node, Vector3 position, float radiusSqr, ICollection<TItem> result)
        {
            if (!Intersects(node, position, radiusSqr))
                return;

            Vector3 center = node.Center;

            float dx = position.x - center.x;
            float dy = position.y - center.y;
            float dz = position.z - center.z;

            var sqrDist = dx * dx + dy * dy + dz * dz;

            if (sqrDist <= node.MaxOffset)
                foreach (var handle in node.Items)
                    if (Intersects(handle.Item, position, radiusSqr))
                        result.Add(handle.Item);

            if (node.Children == null)
                return;

            foreach (var child in node.Children)
                if(child != null)
                    QueryNode(child, position, radiusSqr, result);
        }

        private bool Intersects(UnboundedOctreeNode<TItem> node, Vector3 position, float radiusSqr) => node.SqrDistance(position) <= radiusSqr;
        private bool Intersects(TItem item, Vector3 position, float radiusSqr)
        {
            Vector3 itemPosition = item.Position;

            float dx = itemPosition.x - position.x;
            float dy = itemPosition.y - position.y;
            float dz = itemPosition.z - position.z;

            return dx * dx + dy * dy + dz * dz <= radiusSqr;
        }

        private void RemoveItem(TItem item) => _registeredItems.Remove(item);

        private EntryHandle<UnboundedOctreeNode<TItem>, TItem> CreateHandle(TItem item)
        {
            var handle = new EntryHandle<UnboundedOctreeNode<TItem>, TItem>(item);
            _registeredItems[item] = handle;

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