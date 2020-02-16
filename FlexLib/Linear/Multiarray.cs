using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace FlexLib.Linear
{
    /// <summary>
    /// Represents a multi-dimensional array of elements. The elements can be easily accessed via a flat-index or a multi-index. Note that these indices are in row-major order. The object supports subdivision that references the original multi-dimensional array.
    /// </summary>
    /// <typeparam name="T">The type of elements to store.</typeparam>
    public class Multiarray<T> : IEnumerable<T>
    {
        /// <summary>
        /// The parent of the current <see cref="Multiarray{T}"/> object. If <c>null</c>, there is no parent. Otherwise, the parent contains the original <see cref="_Data"/> that the current <see cref="Multiarray{T}"/> object references.
        /// </summary>
        private Multiarray<T> _Parent;
        /// <summary>
        /// The data stored in the current <see cref="Multiarray{T}"/> object. The data may point to a <see cref="_Parent"/> data array.
        /// </summary>
        private T[] _Data;
        /// <summary>
        /// The mask over the <see cref="_Data"/> of the current <see cref="Multiarray{T}"/> object. If the <see cref="_Parent"/> is <c>null</c>, then the mask is <c>null</c>. Otherwise, the mask is an array with the same length as the <see cref="_Order"/>. If an entry of the mask is <c>null</c>, then, the corresponding axis is entirely included. If an entry of the mask is an array, then, the corresponding axis has only the specified indices included. This does not change over the course of the object lifetime.
        /// </summary>
        readonly private int[][] _Mask;
        /// <summary>
        /// The order of the <see cref="_Data"/> of the current <see cref="Multiarray{T}"/> object. Each element of the order is the number entries along each axis.  This does not change over the course of the object lifetime.
        /// </summary>
        readonly private int[] _Order;
        /// <summary>
        /// The number of entries in the <see cref="_Data"/> of the current <see cref="Multiarray{T}"/> object. This does not change over the course of the object lifetime.
        /// </summary>
        readonly private int _Size;

        /// <summary>
        /// The size, in number of elements, of the current <see cref="Multiarray{T}"/> object.
        /// </summary>
        /// <value>The number of elements of the current object.</value>
        public int Size
        {
            get => _Size;
        }
        /// <summary>
        /// The order, as a list of sizes of dimensions, of the current <see cref="Multiarray{T}"/> object.
        /// </summary>
        /// <value>The order of the current object.</value>
        public IList<int> Order
        {
            get => Array.AsReadOnly(_Order);
        }
        /// <summary>
        /// The degree, in number of dimensions, of the current object <see cref="Multiarray{T}"/> object.
        /// </summary>
        /// <value>The degree of the current object.</value>
        public int Degree
        {
            get => _Order.Length;
        }

        /// <summary>
        /// Gets the element at the specified flat-index in the current <see cref="Multiarray{T}"/> object.
        /// </summary>
        /// <param name="index">The flat-index.</param>
        /// <value>The element at the flat-index <c>index</c>.</value>
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Size)
                    throw new IndexOutOfRangeException();

                return _Data[AbsoluteIndex(index)];
            }
            set
            {
                if (index < 0 || index >= Size)
                    throw new IndexOutOfRangeException();

                _Data[AbsoluteIndex(index)] = value;
            }
        }
        /// <summary>
        /// Gets the element at the specified multi-index in the current <see cref="Multiarray{T}"/> object.
        /// </summary>
        /// <param name="indices">The multi-index.</param>
        /// <value>The element at the multi-index <c>indices</c>.</value>
        public T this[params int[] indices]
        {
            get
            {
                if (indices.Length != Degree)
                    throw new RankException("Index must have same size as degree.");

                for (int k = 0; k < Degree; k++)
                {
                    if (indices[k] < 0 || indices[k] >= Order[k])
                        throw new IndexOutOfRangeException();
                }

                return _Data[AbsoluteIndex(indices)];
            }
            set
            {
                if (indices.Length != Degree)
                    throw new RankException("Index must have same size as degree.");

                for (int k = 0; k < Degree; k++)
                {
                    if (indices[k] < 0 || indices[k] >= Order[k])
                        throw new IndexOutOfRangeException();
                }

                _Data[AbsoluteIndex(indices)] = value;
            }
        }

        /// <summary>
        /// Converts a flat-index for the masked data into the flat-index for the unmasked data for the current <see cref="Multiarray{T}"/> object.
        /// </summary>
        /// <param name="index">The flat-index.</param>
        /// <returns>The converted flat-index.</returns>
        private int AbsoluteIndex(int index)
        {
            // Return same index if no parent.
            if (_Parent == null)
                return index;

            // Initialize absolute index and index stride.
            int absolute = 0;
            int stride = 1;

            // Travel along each dimension calculating resulting
            // index from mask which may be null.
            for (int d = Degree - 1; d >= 0; d--)
            {
                // Partial index is amound in current dimension.
                // Index is updated for next dimension.
                index = Math.DivRem(index, Order[d], out int partial);

                // Add to absolute index based on mask * stride.
                if (_Mask[d] == null)
                    absolute += partial * stride;
                else
                    absolute += _Mask[d][partial] * stride;

                // Stride is updated for next dimension.
                stride *= _Parent.Order[d];
            }

            return absolute;
        }
        /// <summary>
        /// Converts a multi-index for the masked data into the flat-index for the unmasked data for the current <see cref="Multiarray{T}"/> object.
        /// </summary>
        /// <param name="indices">The multi-index.</param>
        /// <returns>The converted flat index.</returns>
        private int AbsoluteIndex(int[] indices)
        {
            // Return same raveled index if no parent.
            if (_Parent == null)
                return RavelIndex(indices);

            // Initialize absolute index and index stride.
            int absolute = 0;
            int stride = 1;

            // Travel along each dimension calculating resulting
            // index from mask which may be null.
            for (int d = Degree - 1; d >= 0; d--)
            {
                if (_Mask[d] == null)
                    absolute += indices[d] * stride;
                else
                    absolute += _Mask[d][indices[d]] * stride;

                // Stride is updated for next dimension.
                stride *= _Parent.Order[d];
            }

            return absolute;
        }

        /// <summary>
        /// Enumerates through all flat-indices of unmasked elements of the current <see cref="Multiarray{T}"/> object.
        /// </summary>
        /// <returns>An enumerator of each flat-index for the unmasked data.</returns>
        private IEnumerable<int> EnumerateAbsoluteIndex()
        {
            // Return normal index if no mask.
            if (_Parent == null)
            {
                foreach (int index in EnumerateIndex())
                    yield return index;
            }
            // Iterate index over mask.
            else
            {
                // Start at first available index.
                int index = AbsoluteIndex(0);

                int[] indices = new int[Degree];
                for (int k = 0; k < Size; k++)
                {
                    yield return index;

                    // We iterate the index in row-major order.
                    int stride = 1;
                    for (int d = Degree - 1; d >= 0; d--)
                    {
                        indices[d]++;
                        if (indices[d] == Order[d])
                        {
                            if (_Mask[d] == null)
                                index -= stride * Order[d];
                            else
                                index -= stride * (_Mask[d][Order[d] - 1] - _Mask[d][0]);
                            indices[d] = 0;
                        }
                        else
                        {
                            if (_Mask[d] == null)
                                index += stride;
                            else
                                index += stride * (_Mask[d][indices[d]] - _Mask[d][indices[d] - 1]);
                            break;
                        }
                        stride *= _Parent.Order[d];
                    }
                }
            }
        }

        /// <summary>
        /// Enumerates through all flat-indices of elements of the current <see cref="Multiarray{T}"/> object.
        /// </summary>
        /// <returns>An enumerator of each flat-index.</returns>
        public IEnumerable<int> EnumerateIndex()
        {
            // Enumerate raveled indices through size of array.
            for (int k = 0; k < Size; k++)
                yield return k;
        }
        /// <summary>
        /// Enumerates through all multi-indices of elements of the current <see cref="Multiarray{T}"/> object.
        /// </summary>
        /// <returns>An enumerator of each multi-index.</returns>
        public IEnumerable<IList<int>> EnumerateIndices()
        {
            // Enumerate unraveled indices through size of array.
            int[] indices = new int[Degree];
            IList<int> indicesReadOnly = Array.AsReadOnly(indices);
            for (int k = 0; k < Size; k++)
            {
                yield return indicesReadOnly;

                // Enumerate indices in row-major order.
                for (int d = Degree - 1; d >= 0; d--)
                {
                    indices[d]++;
                    if (indices[d] == Order[d])
                        indices[d] = 0;
                    else
                        break;
                }
            }
        }

        /// <summary>
        /// Enumerates through all elements of the current <see cref="Multiarray{T}"/> object.
        /// </summary>
        /// <returns>An enumerator of each element.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            // Return each item corresponding to each absolute index.
            foreach (int absoluteIndex in EnumerateAbsoluteIndex())
                yield return _Data[absoluteIndex];
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Converts a multi-index to a flat-index for the current <see cref="Multiarray{T}"/> object.
        /// </summary>
        /// <param name="indices">The multi-index.</param>
        /// <returns>The flat-index.</returns>
        public int RavelIndex(int[] indices)
        {
            // Check that indices are of same length as array order.
            if (indices.Length != Degree)
                throw new RankException("Index must have same size as degree.");

            // Initialize stride and index counters.
            int stride = 1;
            int index = 0;

            // Flatten across each dimension while checking
            // that index elements are within shape.
            for (int k = Degree - 1; k >= 0; k--)
            {
                if (indices[k] < 0 || indices[k] >= Order[k])
                    throw new IndexOutOfRangeException();

                // Update index and stride for next dimension.
                index += indices[k] * stride;
                stride *= Order[k];
            }

            return index;
        }
        /// <summary>
        /// Converts a flat-index to a multi-index for the current <see cref="Multiarray{T}"/> object.
        /// </summary>
        /// <param name="index">The flat-index.</param>
        /// <returns>The multi-index.</returns>
        public int[] UnravelIndex(int index)
        {
            // Check that index is within range of size.
            if (index < 0 || index >= Size)
                throw new IndexOutOfRangeException();

            // Compute unraveled index by dimension.
            int[] indices = new int[Degree];
            for (int k = Degree - 1; k >= 0; k--)
                index = Math.DivRem(index, Order[k], out indices[k]);

            return indices;
        }

        /// <summary>
        /// Creates a new <see cref="Multiarray{T}"/> object with the specified order and blank entries. Each element of the order specifies how many entries are stored across the corresponding axis. The length of the order corresponds with the <see cref="Degree"/> of the object. 
        /// </summary>
        /// <param name="order">The order.</param>
        public Multiarray(params int[] order)
        {
            // Compute the number of elements while
            // checking for non-negative dimensions.
            int elements = 1;
            foreach (int dim in order)
            {
                if (dim < 0)
                    throw new ArgumentException("Dimension must have non-negative size.", nameof(order));
                elements *= dim;
            }

            // Set data information.
            this._Data = new T[elements];
            this._Size = elements;
            this._Order = (int[])order.Clone();
        }
        /// <summary>
        /// Creates a new <see cref="Multiarray{T}"/> object with the specified order and entries from a data array. Each element of the order specifies how many entries are stored across the corresponding axis. The length of the order corresponds with the <see cref="Degree"/> of the object. The number of entries in the data array should match the number of entries specified by the order.
        /// </summary>
        /// <param name="data">The data array.</param>
        /// <param name="order">The order.</param>
        public Multiarray(T[] data, params int[] order)
            : this(order)
        {
            // Check that data size matches desired array size.
            if (data.Length != _Size)
                throw new ArgumentException("Dimensions do not match size of array", nameof(order));

            // Copy from data to array.
            for (int k = 0; k < data.Length; k++)
                _Data[k] = data[k];
        }
        /// <summary>
        /// Creates a new <see cref="Multiarray{T}"/> object from a parent <see cref="Multiarray{T}"/> object with a specified mask. The mask specifies what entries of the parent object will be considered by the new object.
        /// </summary>
        /// <param name="parent">The parent object.</param>
        /// <param name="mask">The element mask.</param>
        private Multiarray(Multiarray<T> parent, int[][] mask)
        {
            // Set data and mask based on parent.
            _Parent = parent;
            _Data = parent._Data;
            _Mask = mask;

            // Set order and size information based on parent.
            _Order = new int[parent.Degree];
            _Size = 1;
            for (int d = 0; d < parent.Degree; d++)
            {
                if (mask[d] == null)
                    _Order[d] = parent.Order[d];
                else
                    _Order[d] = mask[d].Length;
                _Size *= _Order[d];
            }
        }

        /// <summary>
        /// Creates a subarray of the current <see cref="Multiarray{T}"/> object along the specified axis with the specified indices. All other axes will be kept as-is. The subarray will use the same underlying data as the current object.
        /// </summary>
        /// <param name="axis">The axis to take a subarray from.</param>
        /// <param name="indices">The indices along the axis to keep.</param>
        /// <returns>The newly created <see cref="Multiarray{T}"/> object.</returns>
        public Multiarray<T> Subarray(int axis, params int[] indices)
        {
            // Check that axis is within degree.
            if (axis < 0 || axis >= Degree)
                throw new IndexOutOfRangeException();

            // Generate mask array to pass to complex subarray method.
            int[][] mask = new int[Degree][];
            for (int d = 0; d < Degree; d++)
            {
                if (d == axis)
                    mask[d] = indices;
                else
                    mask[d] = null;
            }

            // Return results of complex subarray method.
            return Subarray(mask);
        }
        /// <summary>
        /// Creates a subarray of the current <see cref="Multiarray{T}"/> object with a specified mask. The mask specifies which indices along each axis should be kept in the subarray. The mask should be the same length as the <see cref="Degree"/> of the current object. An entry of the mask can be set to <c>null</c> to keep every index along the corresponding axis. 
        /// </summary>
        /// <param name="mask"></param>
        /// <returns>The newly created <see cref="Multiarray{T}"/> object.</returns>
        public Multiarray<T> Subarray(params int[][] mask)
        {
            // Check that mask is same degree as array.
            if (mask.Length != Degree)
                throw new RankException("Mask must have same size as degree.");

            // Check that each value in the mask is in range of order.
            // Compute the new mask from parent mask (if existing).
            int[][] maskChained = new int[Degree][];
            for (int d = 0; d < Degree; d++)
            {
                // A value of null for the mask axis means keep the entire axis.
                if (mask[d] != null)
                {
                    for (int k = 0; k < mask[d].Length; k++)
                    {
                        if (mask[d][k] < 0 || mask[d][k] >= Order[d])
                            throw new IndexOutOfRangeException();
                    }

                    if (_Parent == null || _Mask[d] == null)
                        maskChained[d] = (int[])mask[d]?.Clone();
                    else
                    {
                        // We select that entries of the parent mask specified
                        // by the child mask to avoid chaining parents.
                        maskChained[d] = new int[mask[d].Length];
                        for (int k = 0; k < mask[d].Length; k++)
                        {
                            maskChained[d][k] = _Mask[d][mask[d][k]];
                        }
                    }
                }
                else
                {
                    if (_Parent == null)
                        maskChained[d] = null;
                    else
                        maskChained[d] = _Mask[d];
                }
            }

            // Return new masked array.
            if (_Parent == null)
                return new Multiarray<T>(this, maskChained);
            else
                return new Multiarray<T>(_Parent, maskChained);
        }

        /// <summary>
        /// Creates a <see cref="Multiarray{T}"/> that contains the same data as the current object in a specified order.
        /// </summary>
        /// <param name="order">The order of the new object.</param>
        /// <returns>A newly created <see cref="Multiarray{T}"/> with the specified order.</returns>
        public Multiarray<T> Reordered(int[] order)
        {
            // Calculate the desired number of elements
            // while checking that the each order is non-negative.
            int elements = 1;
            foreach (int dim in order)
            {
                if (dim < 0)
                    throw new ArgumentException("Dimension must have non-negative size.", nameof(order));
                elements *= dim;
            }
            // Check that desired number of elements equals current number of elements.
            if (elements != Size)
                throw new ArgumentException("Dimensions do not match size of array", nameof(order));

            // Create new array and copy over elements within mask.
            Multiarray<T> array = new Multiarray<T>(order);
            int k = 0;
            foreach (int absoluteIndex in EnumerateAbsoluteIndex())
                array._Data[k++] = _Data[absoluteIndex];

            // Return newly created array.
            return array;
        }

        /// <summary>
        /// Determines whether the current <see cref="Multiarray{T}"/> object is 1-dimensional.
        /// </summary>
        /// <returns><c>true</c> if <see cref="Degree"/> is 1; otherwise, <c>false</c>.</returns>
        public bool IsVector() => Degree == 1;
        /// <summary>
        /// Determines whether the current <see cref="Multiarray{T}"/> object is 2-dimensional.
        /// </summary>
        /// <returns><c>true</c> if <see cref="Degree"/> is 2; otherwise, <c>false</c>.</returns>
        public bool IsMatrix() => Degree == 2;

        /// <summary>
        /// Determines whether two <see cref="Multiarray{T}"/> objects are equal. Equality is defined as the containing the same elements at the same positions with the same order.
        /// </summary>
        /// <param name="arrayA">The first <see cref="Multiarray{T}"/> object.</param>
        /// <param name="arrayB">The second <see cref="Multiarray{T}"/> object.</param>
        /// <returns><c>true</c> if order and elements match; otherwise, <c>false</c>.</returns>
        public static bool operator ==(Multiarray<T> arrayA, Multiarray<T> arrayB)
        {
            // Check if arrays are same reference or if either are null.
            if (ReferenceEquals(arrayA, arrayB))
                return true;
            if (ReferenceEquals(arrayA, null))
                return false;
            if (ReferenceEquals(arrayB, null))
                return false;

            // Check if arrays have same degree.
            if (arrayA.Degree != arrayB.Degree)
                return false;
            // Check if arrays have same shape.
            for (int d = 0; d < arrayA.Degree; d++)
            {
                if (arrayA.Order[d] != arrayB.Order[d])
                    return false;
            }
            // Check if arrays have same elements.
            if (Enumerable.Zip(arrayA, arrayB, (x, y) => !x.Equals(y)).Any(b => b))
                return false;

            return true;
        }
        /// <summary>
        /// Determines whether two <see cref="Multiarray{T}"/> objects are inequal. Equality is defined as the containing the same elements at the same positions with the same order.
        /// </summary>
        /// <param name="arrayA">The first <see cref="Multiarray{T}"/> object.</param>
        /// <param name="arrayB">The second <see cref="Multiarray{T}"/> object.</param>
        /// <returns><c>true</c> if order and elements do not match; otherwise, <c>false</c>.</returns>
        public static bool operator !=(Multiarray<T> arrayA, Multiarray<T> arrayB)
        {
            // Check if arrays are same reference or if either are null.
            if (ReferenceEquals(arrayA, arrayB))
                return false;
            if (ReferenceEquals(arrayA, null))
                return true;
            if (ReferenceEquals(arrayB, null))
                return true;

            // Check if arrays have same degree.
            if (arrayA.Degree != arrayB.Degree)
                return true;
            // Check if arrays have same shape.
            for (int d = 0; d < arrayA.Degree; d++)
            {
                if (arrayA.Order[d] != arrayB.Order[d])
                    return true;
            }
            // Check if arrays have same elements.
            if (Enumerable.Zip(arrayA, arrayB, (x, y) => !x.Equals(y)).Any(b => b))
                return true;

            return false;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current <see cref="Multiarray{T}"/> object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the specified object is equal to the current object; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            // Non-array objects are not equal.
            if (!(obj is Multiarray<T> arr))
                return false;

            // Return behavior consistent with operators.
            return this == arr;
        }
        /// <summary>
        /// Creates a hash code for the current <see cref="Multiarray{T}"/> object. The current object is uniquely determined by its data, order, and mask.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            // The data, order, and mask uniquely determine the array.
            return new { _Data, _Order, _Mask }.GetHashCode();
        }

        /// <summary>
        /// Creates a <see cref="Multiarray{T}"/> object of a specified type from an array of data.
        /// </summary>
        /// <param name="data"> The array of data.</param>
        /// <returns>The newly created <see cref="Multiarray{T}"/> object.</returns>
        public static Multiarray<T> FromArray(Array data)
        {
            // Get the order of the array from the data object.
            int[] order = new int[data.Rank];
            for (int d = 0; d < order.Length; d++)
            {
                order[d] = data.GetLength(d);
            }

            // Create an array with matching dimensions.
            Multiarray<T> array = new Multiarray<T>(order);

            // Iterate through the elements of the original data
            // and assign to the new array checking if types match.
            int k = 0;
            foreach (int[] indices in array.EnumerateIndices())
            {
                if (data.GetValue(indices) is T entry)
                    array._Data[k++] = entry;
                else
                    throw new ArrayTypeMismatchException();
            }

            // Return newly created array.
            return array;
        }
        /// <summary>
        /// Creates a <see cref="Multiarray{T}"/> object of a specified type and filled with a specified value.
        /// </summary>
        /// <param name="order">The order.</param>
        /// <param name="value">The fill value.</param>
        /// <returns>The newly created <see cref="Multiarray{T}"/> object.</returns>
        public static Multiarray<T> Filled(int[] order, T value)
        {
            // Create an array of the specified order.
            Multiarray<T> array = new Multiarray<T>(order);

            // Fill in every entry of the array with the specified value.
            for (int k = 0; k < array.Size; k++)
                array._Data[k] = value;

            // Return newly created array.
            return array;
        }
    }
}