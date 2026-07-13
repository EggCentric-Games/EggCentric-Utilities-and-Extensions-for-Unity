using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EggCentric.ValueProviders.PatternGenerators
{
    public class SequentialValueProvider<T> : PatternGenerator<T>
    {
        public IReadOnlyList<T> Sequence { get; set; }
        public int CurrentItem => _currentIndex;

        private int _currentIndex;

        public SequentialValueProvider(IEnumerable<T> sequence) => SetSequence(sequence);

        public override T Peek(int offset = 0) => Sequence[_currentIndex + offset];

        public void SetSequence(IEnumerable<T> sequence)
        {
            if (sequence == null || sequence.Count() <= 0)
            {
                if (Sequence == null)
                {
                    Debug.LogError($"Provided sequence is invalid! Reverting to default.");
                    SetDefaultSequence();
                    return;
                }

                Debug.LogWarning($"Provided sequence is invalid! Changes weren't applied.");
                return;
            }

            Sequence = sequence.ToArray();
            Reset();
        }

        public void Reset() => _currentIndex = 0;

        protected override void HandleItemChange() => MoveNext();

        private void MoveNext() => _currentIndex = (_currentIndex + 1) % Sequence.Count();

        private void SetDefaultSequence() => SetSequence(new T[] { default });
    }
}