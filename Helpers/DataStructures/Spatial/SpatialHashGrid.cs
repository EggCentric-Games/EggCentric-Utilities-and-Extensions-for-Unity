using EggCentric.Entities;
using EggCentric.QoL;
using UnityEngine;

namespace EggCentric.DataStructures
{
    public class SpatialHashGrid<TItem> : HashGrid<(int x, int y, int z), TItem> where TItem : ISpatialEntity
    {
        private readonly float _cellSize;

        public SpatialHashGrid(float cellSize = 1f)
        {
            _cellSize = cellSize;
        }

        protected override SpatialNode<TItem> CreateNewCell((int x, int y, int z) key)
        {
            Vector3 cellPosition = new Vector3(IdToPosition(key.x), IdToPosition(key.y), IdToPosition(key.z));
            Vector3 extents = Vector3.one * _cellSize * 0.5f;

            return new SpatialNode<TItem>(new NodeBounds(cellPosition, extents));
        }

        protected override (int x, int y, int z) ToKey(TItem item) => (item.Position.x.RoundAwayFromZeroToInt(), item.Position.y.RoundAwayFromZeroToInt(), item.Position.z.RoundAwayFromZeroToInt());
        private float IdToPosition(int id) => Mathf.Sign(id) * (_cellSize * 0.5f) + (id - 1) * _cellSize;
    }
}