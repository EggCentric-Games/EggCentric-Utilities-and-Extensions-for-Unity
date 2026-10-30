using EggCentric.Entities;
using EggCentric.QoL;
using System.Collections.Generic;
using UnityEngine;

namespace EggCentric.DataStructures
{
    public class HashGrid<TItem> : ISpatialStructure<TItem> where TItem : ISpatialEntity
    {
        public IReadOnlyCollection<IGridNode<TItem>> Nodes => _cells.Values;

        private readonly Dictionary<(int x, int y, int z), UniformNode<TItem>> _cells;
        private readonly Dictionary<TItem, GridEntryHandle<TItem>> _registeredItems;

        private readonly float _cellSize;

        public HashGrid(float cellSize = 1f)
        {
            _cells = new Dictionary<(int x, int y, int z), UniformNode<TItem>>();
            _registeredItems = new Dictionary<TItem, GridEntryHandle<TItem>>();

            _cellSize = cellSize;
        }

        public bool TryFindClosest(Vector3 point, out TItem result)
        {
            throw new System.NotImplementedException();
        }

        public int FindInRange(Vector3 point, float radius, ICollection<TItem> result)
        {
            result.Clear();

            if (radius < 0)
                return 0;

            var overlappedNodes = new List<IGridNode<TItem>>();
            foreach (var node in Nodes)
                if (node.SqrDistance(point) > radius * radius)
                    continue;
                else
                    foreach (var handle in node.Items)
                        if ((point - handle.Item.Position).sqrMagnitude <= radius * radius)
                            result.Add(handle.Item);

            return result.Count;
        }

        public int FindAll(ICollection<TItem> result)
        {
            result.Clear();

            foreach (var node in Nodes)
                foreach (var handle in node.Items)
                    result.Add(handle.Item);

            return result.Count;
        }

        public bool Insert(TItem item)
        {
            var key = ToKey(item);
            var cell = RetrieveCell(key);
            var handle = CreateHandle(item);

            cell.Insert(handle);
            return true;
        }

        public void Update(TItem item)
        {
            if (!_registeredItems.TryGetValue(item, out var entry))
                return;

            if (entry.Parent == null)
            {
                _registeredItems.Remove(item);
                return;
            }

            if (entry.Parent.IsInBounds(entry.Item.Position))
                return;

            var key = ToKey(entry.Item);
            var cell = RetrieveCell(key);

            DetachItem(entry);

            cell.Insert(entry);
        }

        public void Remove(TItem item)
        {
            if (!_registeredItems.TryGetValue(item, out var entry))
                return;

            _registeredItems.Remove(item);
            DetachItem(entry);
        }

        private UniformNode<TItem> RetrieveCell((int x, int y, int z) key)
        {
            if (_cells.TryGetValue(key, out var cell))
                return cell;

            return SpawnCell(key);
        }

        private UniformNode<TItem> SpawnCell((int x, int y, int z) key)
        {
            var cell = CreateNewCell(key);
            _cells.Add(key, cell);

            return cell;
        }

        private UniformNode<TItem> CreateNewCell((int x, int y, int z) key)
        {
            Vector3 cellPosition = new Vector3(IdToPosition(key.x), IdToPosition(key.y), IdToPosition(key.z));
            Vector3 extents = Vector3.one * _cellSize * 0.5f;

            return new UniformNode<TItem>(key, new NodeBounds(cellPosition, extents));
        }

        private void RemoveCell((int x, int y, int z) key) => _cells.Remove(key);

        private GridEntryHandle<TItem> CreateHandle(TItem item)
        {
            var handle = new GridEntryHandle<TItem>(item);
            _registeredItems[item] = handle;

            return handle;
        }

        private void DetachItem(GridEntryHandle<TItem> entryHandle)
        {
            var parentCell = entryHandle.Parent;
            if (parentCell == null)
                return;

            parentCell.Remove(entryHandle);
            if (parentCell.Items.Count <= 0)
                RemoveCell(parentCell.Key);
        }

        private (int x, int y, int z) ToKey(TItem item)
        {
            var x = (item.Position.x / _cellSize).RoundAwayFromZeroToInt();
            var y = (item.Position.y / _cellSize).RoundAwayFromZeroToInt();
            var z = (item.Position.z / _cellSize).RoundAwayFromZeroToInt();

            return (x, y, z);
        }

        private float IdToPosition(int id) => Mathf.Sign(id) * (Mathf.Abs(id) - 0.5f) * _cellSize;
    }
}