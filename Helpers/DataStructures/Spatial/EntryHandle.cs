using EggCentric.Entities;
using System;

namespace EggCentric.DataStructures
{
    public class EntryHandle<TItem> : IEntryHandle<TItem> where TItem : ISpatialEntity
    {
        public ISpatialNode<TItem> Parent { get; private set; }
        public bool IsDirty { get; private set; }
        public Guid Id { get; }
        public TItem Item { get; }

        public event Action<IEntryHandle<TItem>> OnDirty;

        public EntryHandle(TItem item, Guid guid, ISpatialNode<TItem> parent = null)
        {
            Parent = parent;
            Item = item;
            Id = guid;
        }

        public void AssignTo(ISpatialNode<TItem> parent) => Parent = parent;

        public void MarkDirty()
        {
            if (IsDirty)
                return;

            IsDirty = true;
            OnDirty?.Invoke(this);
        }

        public void MarkClean() => IsDirty = false;
    }
}