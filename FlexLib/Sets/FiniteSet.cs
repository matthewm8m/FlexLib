using System.Collections.Generic;
using System.Collections.Immutable;

namespace FlexLib.Sets
{
    /// <summary>
    /// Represents an unordered collection of a particular type with arbitrary finite size.
    /// </summary>
    /// <typeparam name="T">The type of elements of the sequence.</typeparam>
    public class FiniteSet<T>
    {
        /// <summary>
        /// The set of elements contained in the set. Notice that we keep it immutable.
        /// </summary>
        private readonly ImmutableHashSet<T> SetElements;

        /// <summary>
        /// The number of elements in the current <see cref="FiniteSet{T}"/>.
        /// </summary>
        public int Count { get => SetElements.Count; }

        /// <summary>
        /// Creates a new <see cref="FiniteSet{T}"/> object with no elements.
        /// </summary>
        public FiniteSet()
        {
            SetElements = ImmutableHashSet.Create<T>();
        }
        /// <summary>
        /// Creates a new <see cref="FiniteSet{T}"/> object with elements from a specified array.
        /// </summary>
        /// <param name="elements">An array containing the elements of the set.</param>
        public FiniteSet(T[] elements)
        {
            SetElements = ImmutableHashSet.Create<T>(elements);
        }
        /// <summary>
        /// Creates a new <see cref="FiniteSet{T}"/> object with elements from a specified enumerable object.
        /// </summary>
        /// <param name="elements">An enumerable object containing the elements of the set.</param>
        public FiniteSet(IEnumerable<T> elements)
        {
            SetElements = ImmutableHashSet.CreateRange<T>(elements);
        }

        /// <summary>
        /// Determines whether the current <see cref="FiniteSet{T}"/> object contains the specified element.
        /// </summary>
        /// <param name="element">The element to search for inside the set.</param>
        /// <returns><c>true</c> if the element is contained in the set; otherwise, <c>false</c>.</returns>
        public bool Contains(T element)
        {
            return SetElements.Contains(element);
        }

        /// <summary>
        /// Determines whether the current <see cref="FiniteSet{T}" /> object is exactly equal to another
        /// <see cref="FiniteSet{T}"/> object. Equality of sequences occurs if the sequences have the same elements.
        /// </summary>
        /// <param name="setCompare">The <see cref="FiniteSet{T}"/> for comparison.</param>
        /// <returns><c>true</c> if the elements match; otherwise, <c>false</c>.</returns>
        public bool SetEquals(FiniteSet<T> setCompare)
        {
            return SetElements.SetEquals(setCompare.SetElements);
        }
    }
}
