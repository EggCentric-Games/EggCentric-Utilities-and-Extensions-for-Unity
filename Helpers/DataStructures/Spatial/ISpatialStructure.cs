using EggCentric.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace EggCentric.DataStructures
{
    public interface ISpatialStructure<TItem> : IItemStructure<TItem> where TItem : ISpatialEntity
    {
        public bool TryFindClosest(Vector3 point, out TItem result);
        public int FindInRange(Vector3 point, float radius, ICollection<TItem> result);
        public int FindAll(ICollection<TItem> result);
    }
}