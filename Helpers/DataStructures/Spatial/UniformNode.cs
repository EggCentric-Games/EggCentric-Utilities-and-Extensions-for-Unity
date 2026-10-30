using EggCentric.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace EggCentric.DataStructures
{
    public class UniformNode<TItem> : IGridNode<TItem> where TItem : ISpatialEntity
    {
        IReadOnlyCollection<IGridHandle<TItem>> IGridNode<TItem>.Items => _items;
        public IReadOnlyCollection<GridEntryHandle<TItem>> Items => _items;
        public Vector3 Center => Bounds.Center;
        public (int x, int y, int z) Key { get; }
        public NodeBounds Bounds { get; }

        private readonly List<GridEntryHandle<TItem>> _items;

        public UniformNode((int x, int y, int z)  key, NodeBounds bounds) 
        {
            Key = key;
            Bounds = bounds;

            _items = new List<GridEntryHandle<TItem>>();
        }

        public void Insert(GridEntryHandle<TItem> item)
        {
            if(_items.Contains(item))
                return;

            _items.Add(item);
            item.AssignTo(this);
        }

        public void Remove(GridEntryHandle<TItem> item)
        {
            if (!_items.Contains(item))
                return;

            _items.Remove(item);
            item.AssignTo(null);
        }

        public bool IsInBounds(Vector3 point) => Bounds.Contains(point);
        public float SqrDistance(Vector3 point) => Bounds.SqrDistance(point);
    }

    public interface IRecursiveNode<TNode, TItem> : IGridNode<TItem> where TNode : IRecursiveNode<TNode, TItem> where TItem : ISpatialEntity
    {
        public IReadOnlyList<TNode> Children { get; }
    }

    public class RecursiveNode<TItem> : IRecursiveNode<RecursiveNode<TItem>, TItem> where TItem : ISpatialEntity
    {
        IReadOnlyCollection<IGridHandle<TItem>> IGridNode<TItem>.Items => _items;
        public IReadOnlyCollection<GridEntryHandle<TItem>> Items => _items;
        public IReadOnlyList<RecursiveNode<TItem>> Children => throw new System.NotImplementedException();
        public Vector3 Center => Bounds.Center;
        public (int x, int y, int z) Key { get; }
        public NodeBounds Bounds { get; }


        private readonly List<GridEntryHandle<TItem>> _items;

        public RecursiveNode((int x, int y, int z) key, NodeBounds bounds)
        {
            Key = key;
            Bounds = bounds;

            _items = new List<GridEntryHandle<TItem>>();
        }

        public void Insert(GridEntryHandle<TItem> item)
        {
            if (_items.Contains(item))
                return;

            _items.Add(item);
            item.AssignTo(this);
        }

        public void Remove(GridEntryHandle<TItem> item)
        {
            if (!_items.Contains(item))
                return;

            _items.Remove(item);
            item.AssignTo(null);
        }

        public bool IsInBounds(Vector3 point) => Bounds.Contains(point);
        public float SqrDistance(Vector3 point) => Bounds.SqrDistance(point);
    }
}