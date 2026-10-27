using EggCentric.Entities;
using System;

namespace EggCentric.DataStructures
{
    public interface IEntryHandle<TItem> where TItem : ISpatialEntity
    {
        public Guid Id { get; }
        public ISpatialNode<TItem> Parent { get; }
        public TItem Item { get; }
        public bool IsDirty { get; }

        public event Action<IEntryHandle<TItem>> OnDirty;

        public void MarkDirty();
    }
}