using EggCentric.Entities;

namespace EggCentric.DataStructures
{
    public class GridEntryHandle<TItem> : IGridHandle<TItem> where TItem : ISpatialEntity
    {
        public IGridNode<TItem> Parent { get; private set; }
        public TItem Item { get; }

        public GridEntryHandle(TItem item, IGridNode<TItem> parent = null)
        {
            Parent = parent;
            Item = item;
        }

        public void AssignTo(IGridNode<TItem> parent) => Parent = parent;
    }
}