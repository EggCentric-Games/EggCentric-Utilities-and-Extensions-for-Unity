using EggCentric.Entities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace EggCentric.DataStructures
{
    public abstract class HashGrid<TKey, TItem> : ISpatialStructure<TItem> where TItem : ISpatialEntity
    {
        private readonly Dictionary<TKey, SpatialNode<TItem>> _cells;
        private readonly Dictionary<Guid, EntryHandle<TItem>> _registeredItems;

        public HashGrid()
        {
            _cells = new Dictionary<TKey, SpatialNode<TItem>>();
            _registeredItems = new Dictionary<Guid, EntryHandle<TItem>>();
        }

        public bool TryFindClosest(Vector3 point, out IEntryHandle<TItem> result)
        {
            throw new NotImplementedException();
        }

        public int FindInRange(Vector3 point, float radius, ICollection<IEntryHandle<TItem>> result)
        {
            result.Clear();

            if (radius < 0)
                return 0;

            var overlappedNodes = new List<ISpatialNode<TItem>>();
            foreach (var node in _cells.Values)
                if (node.SqrDistance(point) > radius * radius)
                    continue;
                else
                    foreach (var handle in node.Items)
                        if ((point - handle.Item.Position).sqrMagnitude <= radius * radius)
                            result.Add(handle);

            return result.Count;
        }

        public int FindAll(ICollection<IEntryHandle<TItem>> result)
        {
            result.Clear();

            foreach (var node in _cells.Values)
                foreach (var handle in node.Items)
                    result.Add(handle);

            return result.Count;
        }

        public IEntryHandle<TItem> Insert(TItem item)
        {
            var key = ToKey(item);
            var cell = RetrieveCell(key);
            var handle = CreateHandle(item);

            cell.Insert(handle);
            return handle;
        }

        public void Update(IEntryHandle<TItem> handle)
        {
            if (!_registeredItems.TryGetValue(handle.Id, out var entry))
                return;

            if(entry.Parent == null)
                return;

            if (entry.Parent.IsInBounds(entry.Item.Position))
            {
                entry.MarkClean();
                return;
            }

            var key = ToKey(entry.Item);
            var cell = RetrieveCell(key);

            entry.Parent.Remove(entry);
            cell.Insert(entry);
            entry.MarkClean();
        }

        public void Remove(IEntryHandle<TItem> handle)
        {
            if (!_registeredItems.TryGetValue(handle.Id, out var entry))
                return;

            _registeredItems.Remove(handle.Id);

            if (entry.Parent == null)
                return;

            entry.Parent.Remove(entry);
        }

        private EntryHandle<TItem> CreateHandle(TItem item)
        {
            var guid = Guid.NewGuid();
            var handle = new EntryHandle<TItem>(item, guid);
            _registeredItems[guid] = handle;

            return handle;
        }

        private SpatialNode<TItem> RetrieveCell(TKey key)
        {
            if (_cells.TryGetValue(key, out var cell))
                return cell;

            return SpawnCell(key);
        }

        private SpatialNode<TItem> SpawnCell(TKey key)
        {
            var cell = CreateNewCell(key);
            _cells.Add(key, cell);

            return cell;
        }

        protected abstract SpatialNode<TItem> CreateNewCell(TKey key);
        protected abstract TKey ToKey(TItem item);
    }
}