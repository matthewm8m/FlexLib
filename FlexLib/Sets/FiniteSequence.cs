using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Immutable;

namespace FlexLib.Sets
{
    public class FiniteSequence<T> : IEnumerable<T>
    {
        private readonly ImmutableList<T> SequenceElements;

        public int Count { get => SequenceElements.Count; }

        public FiniteSequence()
        {
            SequenceElements = ImmutableList.Create<T>();
        }
        public FiniteSequence(T[] elements)
        {
            SequenceElements = ImmutableList.Create<T>(elements);
        }
        public FiniteSequence(IEnumerable<T> elements)
        {
            SequenceElements = ImmutableList.CreateRange<T>(elements);
        }

        public bool Contains(T element)
        {
            return SequenceElements.Contains(element);
        }

        public bool SequenceEquals(FiniteSequence<T> sequenceCompare)
        {
            if (Count != sequenceCompare.Count)
            {
                return false;
            }
            for (int i = 0; i < Count; i++)
            {
                if (!SequenceElements[i].Equals(sequenceCompare.SequenceElements[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return SequenceElements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return SequenceElements.GetEnumerator();
        }
    }
}
