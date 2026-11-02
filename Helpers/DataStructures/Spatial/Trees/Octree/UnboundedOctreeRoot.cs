using EggCentric.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace EggCentric.DataStructures.Trees
{
    public class UnboundedOctreeRoot<TItem> where TItem : ISpatialEntity
    {
        public IReadOnlyList<UnboundedOctreeNode<TItem>> Nodes => _nodes;

        private readonly UnboundedOctreeSettings _settings;
        private readonly UnboundedOctreeNode<TItem>[] _nodes;
        private NodeBounds _nodeBounds;

        public UnboundedOctreeRoot(float size, in UnboundedOctreeSettings settings)
        {
            _settings = settings;
            _nodes = new UnboundedOctreeNode<TItem>[8];
            _nodeBounds = new NodeBounds(Vector3.zero, Vector3.one * size * 0.5f);

            CreateInitialNodes();
        }

        public void Draw()
        {
            foreach (var node in _nodes)
                node.Draw();
        }

        public void Expand()
        {
            _nodeBounds = new NodeBounds(Vector3.zero, _nodeBounds.Extents * 2f);

            for (int i = 0; i < 8; i++)
            {
                var rootNode = _nodes[i];
                var newRootNode = new UnboundedOctreeNode<TItem>(new NodeBounds(rootNode.Center * 2f, rootNode.Extents * 2f), in _settings);
                newRootNode.Join(rootNode);
                _nodes[i] = newRootNode;
            }
        }

        public bool Contains(Vector3 point) => _nodeBounds.Contains(point);

        private void CreateInitialNodes()
        {
            for (int id = 0; id < 8; id++)
                _nodes[id] = CreateChild(id);
        }

        private UnboundedOctreeNode<TItem> CreateChild(int id)
        {
            var multiplier = id.ToMultiplier();
            var centerOffset = _nodeBounds.Extents;
            centerOffset.Scale(new Vector3(multiplier.x, multiplier.y, multiplier.z));
            var childCenter = _nodeBounds.Center + centerOffset * 0.5f;
            var childExtents = _nodeBounds.Extents * 0.5f;

            NodeBounds childBounds = new NodeBounds(childCenter, childExtents);
            return new UnboundedOctreeNode<TItem>(childBounds, in _settings);
        }
    }
}