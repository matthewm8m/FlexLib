using System;

using FlexLib.Algebra;

namespace FlexLib.Linear
{
    /// <summary>
    /// Represents a two-dimensional array of elements of a field.
    /// Supports all of the operations of a <see cref="Multiarray{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of field elements to store.</typeparam>
    public class Matrix<T> : Multiarray<T> where T : BaseFieldElement<T>
    {
        /// <summary>
        /// The number of rows of the current <see cref="Matrix{T}"/> object.
        /// </summary>
        public readonly int Rows;
        /// <summary>
        /// The number of columns of the current <see cref="Matrix{T}"/> object.
        /// </summary>
        public readonly int Columns;

        /// <summary>
        /// The <see cref="BaseField{T}"/> object that is used to represent the field elements of the current <see cref="Matrix{T}"/> object.
        /// </summary>
        private readonly BaseField<T> Field;

        /// <summary>
        /// Creates a new <see cref="Matrix{T}"/> object with the specified number of rows and columns and associated mathematical field.
        /// </summary>
        /// <param name="field">The mathematical field containing elements of type <c>T</c>.</param>
        /// <param name="rows">The number of rows.</param>
        /// <param name="columns">The number of columns.</param>
        public Matrix(BaseField<T> field, int rows, int columns)
            : base(rows, columns)
        {
            Rows = rows;
            Columns = columns;

            // Must check that field is non-null.
            if (field == null)
                throw new ArgumentNullException();
            Field = field;
        }
        /// <summary>
        /// Creates a new <see cref="Matrix{T}"/> object that copies all properties from another <see cref="Matrix{T}"/> object. The new object can be treated exactly the same as the copied object.
        /// </summary>
        /// <param name="copy">The object to copy from.</param>
        public Matrix(Matrix<T> copy)
            : base(copy)
        {
            Field = copy.Field;
            Rows = copy.Rows;
            Columns = copy.Columns;
        }
        /// <summary>
        /// Creates a new <see cref="Matrix{T}"/> object that copies all properties from a compatiable <see cref="Multiarray{T}"/> object subject to an associated mathematical field.
        /// </summary>
        /// <param name="field">The mathematical field containing elements of type <c>T</c>.</param>
        /// <param name="array">The multi-array to copy from.</param>
        private Matrix(BaseField<T> field, Multiarray<T> array)
            : base(array)
        {
            Field = field;

            Rows = array.Order[0];
            Columns = array.Order[1];
        }

        /// <summary>
        /// Creates a submatrix of the current <see cref="Matrix{T}"/> object along the specified axis with the specified indices. All other axes will be kept as-is. The subarray will use the same underlying data as the current object.
        /// </summary>
        /// <param name="axis">The axis to take a subarray from. Must be <c>0</c> for rows and <c>1</c> for columns.</param>
        /// <param name="indices">The indices along the axis to keep.</param>
        /// <returns></returns>
        public Matrix<T> Submatrix(int axis, params int[] indices)
        {
            return Matrix<T>.FromMultiarray(Field, Subarray(axis, indices));
        }
        /// <summary>
        /// Creates a submatrix of the current <see cref="Matrix{T}"/> object with a specified mask. The mask specifies which indices along each axis should be kept in the subarray. The mask should have length two for rows and columns respectively. An entry of the mask can be set to <c>null</c> to keep every index along the corresponding axis.
        /// </summary>
        /// <param name="mask">The mask to apply.</param>
        /// <returns>The newly created <see cref="Matrix{T}"/> object.</returns>
        public Matrix<T> Submatrix(params int[][] mask)
        {
            return Matrix<T>.FromMultiarray(Field, Subarray(mask));
        }

        /// <summary>
        /// Gets the row <see cref="Matrix{T}"/> submatrix object with specified row index.
        /// </summary>
        /// <param name="i">The index of the row to get.</param>
        /// <returns>A submatrix corresponding to the specified row.</returns>
        public Matrix<T> Row(int i)
        {
            return Submatrix(0, i);
        }
        /// <summary>
        /// Gets the row <see cref="Matrix{T}"/> submatrix object with specified column index.
        /// </summary>
        /// <param name="j">The index of the column to get.</param>
        /// <returns>A submatrix corresponding to the specified column.</returns>
        public Matrix<T> Column(int j)
        {
            return Submatrix(1, j);
        }

        /// <summary>
        /// Determines whether the current <see cref="Matrix{T}"/> object is square (has a matching number of rows and columns).
        /// </summary>
        /// <returns><c>true</c> if <see cref="Rows"/> = <see cref="Columns"/> ; otherwise, <c>false</c>.</returns>
        public bool IsSquare()
        {
            return Rows == Columns;
        }
        /// <summary>
        /// Computes the traces of the current <see cref="Matrix{T}"/> object. This is only applicable if <see cref="IsSquare()"/> is <c>true</c>.
        /// </summary>
        /// <returns>The sum across the main diagonal.</returns>
        public T Trace()
        {
            // Non-square matrices not allowed.
            if (!IsSquare())
                throw new InvalidOperationException();

            // Initialize trace to zero.
            T trace = Field.Zero();

            // Add elements along diagonal to trace.
            for (int k = 0; k < Rows; k++)
                trace = trace + this[k, k];

            return trace;
        }

        /// <summary>
        /// Computes the sum of the current and another <see cref="Matrix{T}"/> object.
        /// </summary>
        /// <param name="matrix">The other matrix.</param>
        /// <returns>The element-wise sum matrix.</returns>
        public Matrix<T> Add(Matrix<T> matrix)
        {
            // Make sure that matrices are additively compatible.
            if (Rows != matrix.Rows || Columns != matrix.Columns)
                throw new InvalidOperationException();
            if (Field != matrix.Field)
                throw new InvalidOperationException();

            // Calculate sum element-wise.
            Matrix<T> sum = new Matrix<T>(Field, Rows, Columns);
            for (int n = 0; n < sum.Size; n++)
                sum[n] = this[n] + matrix[n];
            return sum;
        }
        /// <summary>
        /// Computes the difference of the current and another <see cref="Matrix{T}"/> object.
        /// </summary>
        /// <param name="matrix">The other matrix.</param>
        /// <returns>The element-wise difference matrix.</returns>
        public Matrix<T> Subtract(Matrix<T> matrix)
        {
            // Make sure that matrices are additively compatible.
            if (Rows != matrix.Rows || Columns != matrix.Columns)
                throw new InvalidOperationException();
            if (Field != matrix.Field)
                throw new InvalidOperationException();

            // Calculate difference element-wise.
            Matrix<T> difference = new Matrix<T>(Field, Rows, Columns);
            for (int n = 0; n < difference.Size; n++)
                difference[n] = this[n] - matrix[n];
            return difference;
        }
        /// <summary>
        /// Computes the scaled matrix of the current <see cref="Matrix{T}"/> object with specified scalar.
        /// </summary>
        /// <param name="scalar">The scalar to scale by.</param>
        /// <returns>The element-wise scaled matrix.</returns>
        public Matrix<T> Scale(T scalar)
        {
            // Calculate scaling element-wise.
            Matrix<T> matrix = new Matrix<T>(Field, Rows, Columns);
            for (int n = 0; n < matrix.Size; n++)
                matrix[n] = this[n] * scalar;
            return matrix;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current <see cref="Matrix{T}"/> object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the specified object is equal to the current object; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Matrix<T> matrix)
                return this == matrix;
            return false;
        }
        /// <summary>
        /// Creates a hash code for the current <see cref="Matrix{T}"/> object. The current object is uniquely determined by its field, data, order, and mask.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            int baseHash = base.GetHashCode();
            return new { baseHash, Field }.GetHashCode();
        }

        /// <summary>
        /// Creates a <see cref="Matrix{T}"/> object of a specified type with an associated mathematical field from an array of data.
        /// </summary>
        /// <param name="field">The mathematical field.</param>
        /// <param name="data">The array of data.</param>
        /// <returns>The newly created <see cref="Matrix{T}"/> object.</returns>
        public static Matrix<T> FromArray(BaseField<T> field, Array data)
        {
            // Must check that field is non-null and data is matrix.
            if (field == null)
                throw new ArgumentNullException();
            if (data.Rank != 2)
                throw new RankException();

            // Get dimensions of matrix.
            int rows = data.GetLength(0);
            int columns = data.GetLength(1);

            // Copy each element of the data array into the matrix.
            Matrix<T> matrix = new Matrix<T>(field, rows, columns);
            int n = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (data.GetValue(i, j) is T entry)
                        matrix[n++] = entry;
                    else
                        throw new ArrayTypeMismatchException();
                }
            }
            return matrix;
        }
        /// <summary>
        /// Creates a <see cref="Matrix{T}"/> object of a specified type with an associated mathematical field from a <see cref="Multiarray{T}"/> of the same type.
        /// </summary>
        /// <param name="field">The mathematical field.</param>
        /// <param name="array">The multi-array of data.</param>
        /// <returns>The newly created <see cref="Matrix{T}"/> object.</returns>
        public static Matrix<T> FromMultiarray(BaseField<T> field, Multiarray<T> array)
        {
            // Must check that field is non-null and multi-array is matrix.
            if (field == null)
                throw new ArgumentNullException();
            if (!array.IsMatrix())
                throw new RankException();

            return new Matrix<T>(field, array);
        }

        /// <summary>
        /// Creates a <see cref="Matrix{T}"/> object of a specified type and filled with a specified value from a particular mathematical field.
        /// </summary>
        /// <param name="field">The mathematical field.</param>
        /// <param name="rows">The number of rows for the new matrix.</param>
        /// <param name="columns">The number of columns for the new matrix.</param>
        /// <param name="value">The fill value.</param>
        /// <returns>The newly created <see cref="Matrix{T}"/> object.</returns>
        public static Matrix<T> Filled(BaseField<T> field, int rows, int columns, T value)
        {
            // Must check that field is non-null.
            if (field == null)
                throw new ArgumentNullException();

            // Create matrix and fill each entry with specified value.
            Matrix<T> matrix = new Matrix<T>(field, rows, columns);
            for (int n = 0; n < matrix.Size; n++)
                matrix[n] = value;
            return matrix;
        }
        /// <summary>
        /// Creates a <see cref="Matrix{T}"/> object of a specified type and filled with zeros from a particular mathematical field.
        /// </summary>
        /// <param name="field">The mathematical field</param>
        /// <param name="rows">The number of rows for the new matrix.</param>
        /// <param name="columns">The number of columns for the new matrix.</param>
        /// <returns>The newly created <see cref="Matrix{T}"/> object.</returns>
        public static Matrix<T> Zeros(BaseField<T> field, int rows, int columns)
        {
            // Must check that field is non-null.
            if (field == null)
                throw new ArgumentNullException();

            return Filled(field, rows, columns, field.Zero());
        }
        /// <summary>
        /// Creates a <see cref="Matrix{T}"/> object of a specified type and filled with ones from a particular mathematical field.
        /// </summary>
        /// <param name="field">The mathematical field.</param>
        /// <param name="rows">The number of rows for the new matrix.</param>
        /// <param name="columns">The number of columns for the new matrix.</param>
        /// <returns>The newly created <see cref="Matrix{T}"/> object.</returns>
        public static Matrix<T> Ones(BaseField<T> field, int rows, int columns)
        {
            // Must check that field is non-null.
            if (field == null)
                throw new ArgumentNullException();

            return Filled(field, rows, columns, field.One());
        }
        /// <summary>
        /// Creates a <see cref="Matrix{T}"/> object of a specified type from a particular mathematical field. This matrix is the multiplicative identity for matrices. The diagonal entries are one and the non-diagonal entries are zero.
        /// </summary>
        /// <param name="field">The mathematical field.</param>
        /// <param name="size">The number of rows and columns for the new matrix.</param>
        /// <returns>The newly created <see cref="Matrix{T}"/> object.</returns>
        public static Matrix<T> Identity(BaseField<T> field, int size)
        {
            // Must check that field is non-null.
            if (field == null)
                throw new ArgumentNullException();

            // Create matrix and fill.
            // Diagonal entries are one.
            // Non-diagonal entries are zero.
            Matrix<T> matrix = new Matrix<T>(field, size, size);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    int n = i * size + j;
                    if (i == j)
                        matrix[n] = field.One();
                    else
                        matrix[n] = field.Zero();
                }
            }
            return matrix;
        }

        /// <summary>
        /// Determines whether two <see cref="Matrix{T}"/> objects are equal. Equality is defined as having equal rows and columns with the same elements in each position from equal fields.
        /// </summary>
        /// <param name="matrixA">The first matrix.</param>
        /// <param name="matrixB">The second matrix.</param>
        /// <returns><c>true</c> if size, elements, and field match; otherwise, <c>false</c>.</returns>
        public static bool operator ==(Matrix<T> matrixA, Matrix<T> matrixB)
        {
            // Make sure that matrices are compatible.
            if (matrixA.Rows != matrixB.Rows || matrixA.Columns != matrixB.Columns)
                return false;
            if (matrixA.Field != matrixB.Field)
                return false;

            // Check that elements match according to field.
            for (int n = 0; n < matrixA.Size; n++)
            {
                if (!matrixA.Field.ElementsEqual(matrixA[n], matrixB[n]))
                    return false;
            }
            return true;
        }
        /// <summary>
        /// Determines whether two <see cref="Matrix{T}"/> objects are inequal. Equality is defined as having equal rows and columns with the same elements in each position from equal fields.
        /// </summary>
        /// <param name="matrixA">The first matrix.</param>
        /// <param name="matrixB">The second matrix.</param>
        /// <returns><c>true</c> if size, elements, or field do not match; otherwise, <c>false</c>.</returns>
        public static bool operator !=(Matrix<T> matrixA, Matrix<T> matrixB)
        {
            // Make sure that matrices are compatible.
            if (matrixA.Rows != matrixB.Rows || matrixA.Columns != matrixB.Columns)
                return true;
            if (matrixA.Field != matrixB.Field)
                return true;

            // Check that elements match according to field.
            for (int n = 0; n < matrixA.Size; n++)
            {
                if (!matrixA.Field.ElementsEqual(matrixA[n], matrixB[n]))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Computes the element-wise sum of two <see cref="Matrix{T}"/> objects.
        /// </summary>
        /// <param name="matrixA">The first matrix.</param>
        /// <param name="matrixB">The second matrix.</param>
        /// <returns>The element-wise sum of matrices.</returns>
        public static Matrix<T> operator +(Matrix<T> matrixA, Matrix<T> matrixB)
        {
            return matrixA.Add(matrixB);
        }
        /// <summary>
        /// Creates an element-wise copy of a <see cref="Matrix{T}"/> object with the same signs.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <returns>An element-wise copy of same sign.</returns>
        public static Matrix<T> operator +(Matrix<T> matrix)
        {
            // Return a copy of the matrix with the same signs.
            Matrix<T> positive = new Matrix<T>(matrix.Field, matrix.Rows, matrix.Columns);
            for (int n = 0; n < matrix.Size; n++)
                positive[n] = +matrix[n];
            return positive;
        }
        /// <summary>
        /// Computes the element-wise difference of two <see cref="Matrix{T}"/> objects.
        /// </summary>
        /// <param name="matrixA">The first matrix.</param>
        /// <param name="matrixB">The second matrix.</param>
        /// <returns>The element-wise difference of matrices.</returns>
        public static Matrix<T> operator -(Matrix<T> matrixA, Matrix<T> matrixB)
        {
            return matrixA.Subtract(matrixB);
        }
        /// <summary>
        /// Creates an element-wise copy of a <see cref="Matrix{T}"/> object with the opposite signs.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <returns>An element-wise copy of opposite sign.</returns>
        public static Matrix<T> operator -(Matrix<T> matrix)
        {
            // Return a copy of the matrix with the opposite signs.
            Matrix<T> negative = new Matrix<T>(matrix.Field, matrix.Rows, matrix.Columns);
            for (int n = 0; n < matrix.Size; n++)
                negative[n] = -matrix[n];
            return negative;
        }

        /// <summary>
        /// Computes the element-wise scaled matrix of a <see cref="Matrix{T}"/> object.
        /// </summary>
        /// <param name="matrix">The matrix to scale.</param>
        /// <param name="scalar">The scalar to scale by.</param>
        /// <returns>The element-wise scaled matrix.</returns>
        public static Matrix<T> operator *(Matrix<T> matrix, T scalar)
        {
            return matrix.Scale(scalar);
        }
        /// <summary>
        /// Computes the element-wise scaled matrix of a <see cref="Matrix{T}"/> object.
        /// </summary>
        /// <param name="scalar">The scalar to scale by.</param>
        /// <param name="matrix">The matrix to scale.</param>
        /// <returns>The element-wise scaled matrix.</returns>
        public static Matrix<T> operator *(T scalar, Matrix<T> matrix)
        {
            return matrix.Scale(scalar);
        }
        /// <summary>
        /// Computes the element-wise inverse scaled matrix of a <see cref="Matrix{T}"/> object.
        /// </summary>
        /// <param name="matrix">The inverse of the scalar to scale by.</param>
        /// <param name="scalar">The matrix to scale.</param>
        /// <returns>The element-wise scaled matrix.</returns>
        public static Matrix<T> operator /(Matrix<T> matrix, T scalar)
        {
            return matrix.Scale(scalar.Inverse());
        }
    }
}