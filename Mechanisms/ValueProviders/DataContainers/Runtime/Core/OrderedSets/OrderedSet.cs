using System;
using System.Collections;
using System.Collections.Generic;

namespace EggCentric.ValueProviders.DataContainers
{
    public class OrderedSet<T> : ICollection<T>, IEnumerable<T>, IEnumerable, IList<T>, IReadOnlyCollection<T>, IReadOnlyList<T>, ICollection, IList
    {
        private readonly List<T> _entries;
        private readonly IEqualityComparer<T> _comparer;

        public OrderedSet() : this(EqualityComparer<T>.Default)
        {
        }

        public OrderedSet(IEqualityComparer<T> comparer)
        {
            _comparer = comparer ?? EqualityComparer<T>.Default;
            _entries = new List<T>();
        }

        public OrderedSet(IEnumerable<T> items, IEqualityComparer<T>? comparer = null) : this(comparer ?? EqualityComparer<T>.Default)
        {
            foreach (var item in items)
                Add(item);
        }

        // --------------------
        // IList<T>
        // --------------------

        public T this[int index]
        {
            get => _entries[index];
            set
            {
                if (IndexOf(value) is int existing && existing != -1 && existing != index)
                    throw new InvalidOperationException("Duplicate values are not allowed.");

                _entries[index] = value;
            }
        }

        public int Count => _entries.Count;
        public bool IsReadOnly => false;

        public void Add(T item)
        {
            if (Contains(item))
                return;

            _entries.Add(item);
        }

        public void Clear() => _entries.Clear();

        public bool Contains(T item) => IndexOf(item) != -1;

        public void CopyTo(T[] array, int arrayIndex) =>
            _entries.CopyTo(array, arrayIndex);

        public IEnumerator<T> GetEnumerator() => _entries.GetEnumerator();

        public int IndexOf(T item)
        {
            for (int i = 0; i < _entries.Count; i++)
            {
                if (_comparer.Equals(_entries[i], item))
                    return i;
            }
            return -1;
        }

        public void Insert(int index, T item)
        {
            if (Contains(item))
                throw new InvalidOperationException("Duplicate values are not allowed.");

            _entries.Insert(index, item);
        }

        public bool Remove(T item)
        {
            int index = IndexOf(item);
            if (index == -1)
                return false;

            _entries.RemoveAt(index);
            return true;
        }

        public void RemoveAt(int index) => _entries.RemoveAt(index);

        // --------------------
        // IEnumerable
        // --------------------

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        // --------------------
        // IList (non-generic)
        // --------------------

        bool IList.IsFixedSize => false;
        bool IList.IsReadOnly => false;

        object? IList.this[int index]
        {
            get => this[index]!;
            set
            {
                if (value is not T typed)
                    throw new ArgumentException($"Value must be of type {typeof(T)}");

                this[index] = typed;
            }
        }

        int IList.Add(object? value)
        {
            if (value is not T typed)
                throw new ArgumentException($"Value must be of type {typeof(T)}");

            Add(typed);
            return IndexOf(typed);
        }

        bool IList.Contains(object? value) =>
            value is T typed && Contains(typed);

        int IList.IndexOf(object? value) =>
            value is T typed ? IndexOf(typed) : -1;

        void IList.Insert(int index, object? value)
        {
            if (value is not T typed)
                throw new ArgumentException($"Value must be of type {typeof(T)}");

            Insert(index, typed);
        }

        void IList.Remove(object? value)
        {
            if (value is T typed)
                Remove(typed);
        }

        // --------------------
        // ICollection (non-generic)
        // --------------------

        bool ICollection.IsSynchronized => false;
        object ICollection.SyncRoot => this;

        void ICollection.CopyTo(Array array, int index) =>
            ((ICollection)_entries).CopyTo(array, index);
    }
}