using EggCentric.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace EggCentric.DataStructures
{
    public interface ISpatialStructure<TItem> where TItem : ISpatialEntity
    {
        public bool TryFindClosest(Vector3 point, out IEntryHandle<TItem> result);
        public int FindInRange(Vector3 point, float radius, ICollection<IEntryHandle<TItem>> result);
        public int FindAll(ICollection<IEntryHandle<TItem>> result);

        public IEntryHandle<TItem> Insert(TItem item);
        public void Update(IEntryHandle<TItem> handle);
        public void Remove(IEntryHandle<TItem> handle);
    }
}