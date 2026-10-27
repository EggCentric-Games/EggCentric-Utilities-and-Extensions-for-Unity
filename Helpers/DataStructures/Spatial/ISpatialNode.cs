using EggCentric.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace EggCentric.DataStructures
{
    public interface ISpatialNode<TItem> where TItem : ISpatialEntity
    {
        public IReadOnlyCollection<IEntryHandle<TItem>> Items { get; }

        public void Insert(EntryHandle<TItem> handle);
        public void Remove(EntryHandle<TItem> handle);
        public bool IsInBounds(Vector3 point);
        public float SqrDistance(Vector3 point);
    }
}