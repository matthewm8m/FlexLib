using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace FlexLib.Sets
{
    /// <summary>
    /// Represents an ordered collection of a particular type with arbitrary finite length.
    /// </summary>
    /// <typeparam name="T">The type of elements of the sequence.</typeparam>
    public class FiniteSequence<T> : IEnumerable<T>
    {
        /// <summary>
        /// The list of elements contained in the sequence. Notice that we keep it immutable.
        /// </summary>
        private readonly ImmutableList<T> SequenceElements;

        /// <summary>
        /// The number of elements in the current <see cref="FiniteSequence{T}"/>.
        /// </summary>
        public int Count { get => SequenceElements.Count; }

        /// <summary>
        /// Creates a new <see cref="FiniteSequence{T}"/> object with no elements.
        /// </summary>
        public FiniteSequence()
        {
            SequenceElements = ImmutableList.Create<T>();
        }
        /// <summary>
        /// Creates a new <see cref="FiniteSequence{T}"/> object with elements from a specified array.
        /// </summary>
        /// <param name="elements">An array containing the ordered elements of the sequence.</param>
        public FiniteSequence(T[] elements)
        {
            SequenceElements = ImmutableList.Create<T>(elements);
        }
        /// <summary>
        /// Creates a new <see cref="FiniteSequence{T}"/> object with elements from a specified enumerable object.
        /// </summary>
        /// <param name="elements">An enumerable object containing the ordered elements of the sequence.</param>
        public FiniteSequence(IEnumerable<T> elements)
        {
            SequenceElements = ImmutableList.CreateRange<T>(elements);
        }

        /// <summary>
        /// Determines whether the current <see cref="FiniteSequence{T}"/> object contains the specified element.
        /// </summary>
        /// <param name="element">The element to search for inside the sequence.</param>
        /// <returns><c>true</c> if the element is contained in the sequence; otherwise, <c>false</c>.</returns>
        public bool Contains(T element)
        {
            return SequenceElements.Contains(element);
        }

        /// <summary>
        /// Determines whether the current <see cref="FiniteSequence{T}"/> object is exactly equal to another
        /// <see cref="FiniteSequence{T}"/> object. Equality of sequences occurs if the sequences have the same length
        /// and have the same elements.
        /// </summary>
        /// <param name="sequenceCompare">The <see cref="FiniteSequence{T}"/> for comparison.</param>
        /// <returns><c>true</c> if <see cref="Count"/> and elements match; otherwise, <c>false</c>.</returns>
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

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="FiniteSequence{T}"/> object in order.
        /// </summary>
        /// <returns>An <see cref="IEnumerator{T}"/> object that can be used to iterate through the elements.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return SequenceElements.GetEnumerator();
        }
        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="FiniteSequence{T}"/> object in order.
        /// </summary>
        /// <returns>An <see cref="IEnumerator"/> object that can be used to iterate throguh the elements.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return SequenceElements.GetEnumerator();
        }
    }
}
