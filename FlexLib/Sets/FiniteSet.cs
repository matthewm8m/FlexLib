using System.Collections.Generic;
using System.Collections.Immutable;

namespace FlexLib.Sets
{
    public class FiniteSet<T>
    {
        private readonly ImmutableHashSet<T> SetElements;

        public int Count { get => SetElements.Count; }

        public FiniteSet()
        {
            SetElements = ImmutableHashSet.Create<T>();
        }
        public FiniteSet(T[] elements)
        {
            SetElements = ImmutableHashSet.Create<T>(elements);
        }
        public FiniteSet(IEnumerable<T> elements)
        {
            SetElements = ImmutableHashSet.CreateRange<T>(elements);
        }

        public bool Contains(T element)
        {
            return SetElements.Contains(element);
        }

        public bool SetEquals(FiniteSet<T> setCompare)
        {
            return SetElements.SetEquals(setCompare.SetElements);
        }
    }
}
