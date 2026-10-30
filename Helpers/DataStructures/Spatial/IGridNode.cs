using EggCentric.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace EggCentric.DataStructures
{
    public interface ISpatialNode<TItem> where TItem : ISpatialEntity
    {

    }

    public interface IGridNode<TItem> : ISpatialNode<TItem> where TItem : ISpatialEntity
    {
        public (int x, int y, int z) Key {  get; }
        public IReadOnlyCollection<IGridHandle<TItem>> Items { get; }
        public Vector3 Center { get; }

        public void Insert(GridEntryHandle<TItem> handle);
        public void Remove(GridEntryHandle<TItem> handle);
        public bool IsInBounds(Vector3 point);
        public float SqrDistance(Vector3 point);
    }
}