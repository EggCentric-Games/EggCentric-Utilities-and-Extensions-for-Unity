using System;
using System.Collections.Generic;

namespace EggCentric.Tags
{
    public interface ITagSet<TType> where TType : ITagType
    {
        public IEnumerable<Type> Tags { get; }

        public void Add<TTag>() where TTag : ITag<TType>;
        public void Remove<TTag>() where TTag : ITag<TType>;
        public bool Has<TTag>() where TTag : ITag<TType>;
    }
}
