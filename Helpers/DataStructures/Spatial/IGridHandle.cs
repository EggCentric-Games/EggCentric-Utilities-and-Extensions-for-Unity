using EggCentric.Entities;
namespace EggCentric.DataStructures
{
    public interface IEntryHandle<TNode, TItem> where TNode : ISpatialNode<TItem> where TItem : ISpatialEntity
    {
        public TNode Parent { get; }
        public TItem Item { get; }
    }

    public interface IGridHandle<TItem> : IEntryHandle<IGridNode<TItem>, TItem> where TItem : ISpatialEntity
    {
    }
}