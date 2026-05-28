using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EggCentric.ValueProviders.PatternGenerators
{
    public class SequentialValueProvider<T> : PatternGenerator<T>
    {
        private IReadOnlyList<T> _sequence;
        private int _currentIndex;

        public SequentialValueProvider(IEnumerable<T> sequence) => SetSequence(sequence);

        public override T Peek() => _sequence[_currentIndex];

        public void SetSequence(IEnumerable<T> sequence)
        {
            if (sequence == null || sequence.Count() <= 0)
            {
                if (_sequence == null)
                {
                    Debug.LogError($"Provided sequence is invalid! Reverting to default.");
                    SetDefaultSequence();
                    return;
                }

                Debug.LogWarning($"Provided sequence is invalid! Changes weren't applied.");
                return;
            }

            _sequence = sequence.ToArray();
            Reset();
        }

        public void Reset() => _currentIndex = 0;

        protected override void HandleItemChange() => MoveNext();

        private void MoveNext() => _currentIndex = (_currentIndex + 1) % _sequence.Count();

        private void SetDefaultSequence() => SetSequence(new T[] { default });
    }
}