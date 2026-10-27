using EggCentric.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace EggCentric.DataStructures
{
    public class SpatialNode<TItem> : ISpatialNode<TItem> where TItem : ISpatialEntity
    {
        IReadOnlyCollection<IEntryHandle<TItem>> ISpatialNode<TItem>.Items => _items;
        public IReadOnlyCollection<EntryHandle<TItem>> Items => _items;
        public NodeBounds Bounds { get; }

        private readonly List<EntryHandle<TItem>> _items;

        public SpatialNode(NodeBounds bounds) 
        {
            Bounds = bounds;

            _items = new List<EntryHandle<TItem>>();
        }

        public void Insert(EntryHandle<TItem> item)
        {
            if(_items.Contains(item))
                return;

            _items.Add(item);
            item.AssignTo(this);
        }

        public void Remove(EntryHandle<TItem> item)
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