using EggCentric.Entities;
namespace EggCentric.DataStructures
{
    public interface IEntryHandle<TNode, TItem> where TNode : ISpatialNode<TItem> where TItem : ISpatialEntity
    {
        public TNode Host { get; }
        public TItem Item { get; }
    }

    public class EntryHandle<TNode, TItem> : IEntryHandle<TNode, TItem> where TNode : ISpatialNode<TItem> where TItem : ISpatialEntity
    {
        public TNode Host { get; private set; }
        public TItem Item { get; }

        public EntryHandle(TItem item, TNode parent = default)
        {
            Host = parent;
            Item = item;
        }

        public virtual void AssignTo(TNode parent) => Host = parent;
    }

    public interface IGridHandle<TItem> : IEntryHandle<IGridNode<TItem>, TItem> where TItem : ISpatialEntity
    {
    }
}