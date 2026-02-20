using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EggCentric.ValueProviders
{
    public class SequentialValueProvider<T> : IValueProvider<T>
    {
        public T Value { get; private set; }

        private IReadOnlyList<T> _sequence;
        private int _currentIndex;

        public SequentialValueProvider(IEnumerable<T> sequence) => SetSequence(sequence);

        public void SetSequence(IEnumerable<T> sequence)
        {
            if (sequence == null || sequence.Count() <= 0)
            {
                Debug.LogError($"Provided sequence is invalid! Reverting to default.");
                SetDefaultSequence();
                return;
            }

            _sequence = sequence.ToList();
            Reset();
        }

        public T GetNext()
        {
            var value = _sequence[_currentIndex];
            MoveNext();
            return value;
        }

        public void Reset() => _currentIndex = 0;

        private void MoveNext() => _currentIndex = (_currentIndex + 1) % _sequence.Count();

        private void SetDefaultSequence() => SetSequence(new T[] { default });
    }
}