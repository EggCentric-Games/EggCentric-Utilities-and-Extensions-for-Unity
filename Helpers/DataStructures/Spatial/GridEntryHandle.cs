using EggCentric.Entities;

namespace EggCentric.DataStructures
{
    public class GridEntryHandle<TItem> : EntryHandle<IGridNode<TItem>, TItem>, IGridHandle<TItem> where TItem : ISpatialEntity
    {
        public GridEntryHandle(TItem item, IGridNode<TItem> parent = null) : base(item, parent)
        {
        }
    }
}