using System;
using System.Collections.Generic;

namespace EggCentric.Tags
{
    public class TagSet<TType> : ITagSet<TType> where TType : ITagType
    {
        public IEnumerable<Type> Tags => _tags;

        private readonly HashSet<Type> _tags;

        public void Add<TTag>() where TTag : ITag<TType> => _tags.Add(typeof(TTag));
        public bool Has<TTag>() where TTag : ITag<TType> => _tags.Contains(typeof(TTag));
        public void Remove<TTag>() where TTag : ITag<TType> => _tags.Remove(typeof(TTag));
    }
}
