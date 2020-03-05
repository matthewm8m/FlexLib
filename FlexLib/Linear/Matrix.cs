using System;

using FlexLib.Algebra;

namespace FlexLib.Linear
{
    public class Matrix<T> : Multiarray<T> where T : BaseFieldElement<T>
    {
        public readonly int Rows;
        public readonly int Columns;

        private readonly BaseField<T> Field;

        public Matrix(BaseField<T> field, int rows, int columns)
            : base(rows, columns)
        {
            Rows = rows;
            Columns = columns;

            if (field == null)
                throw new ArgumentNullException();
            Field = field;
        }
        public Matrix(Matrix<T> copy)
            : base(copy)
        {
            Field = copy.Field;
            Rows = copy.Rows;
            Columns = copy.Columns;
        }
        private Matrix(BaseField<T> field, Multiarray<T> array)
            : base(array)
        {
            Field = field;

            Rows = array.Order[0];
            Columns = array.Order[1];
        }

        public Matrix<T> Submatrix(int axis, params int[] indices)
        {
            return Matrix<T>.FromMultiarray(Field, Subarray(axis, indices));
        }
        public Matrix<T> Submatrix(params int[][] mask)
        {
            return Matrix<T>.FromMultiarray(Field, Subarray(mask));
        }

        public Matrix<T> Row(int i)
        {
            return Submatrix(0, i);
        }
        public Matrix<T> Column(int j)
        {
            return Submatrix(1, j);
        }

        public bool IsSquare()
        {
            return Rows == Columns;
        }

        public T Trace()
        {
            if (!IsSquare())
                throw new InvalidOperationException();

            T sum = Field.Zero();

            for (int k = 0; k < Rows; k++)
                sum = sum + this[k, k];

            return sum;
        }

        public Matrix<T> Add(Matrix<T> matrix)
        {
            if (Rows != matrix.Rows || Columns != matrix.Columns)
                throw new InvalidOperationException();

            if (Field != matrix.Field)
                throw new InvalidOperationException();

            Matrix<T> sum = new Matrix<T>(Field, Rows, Columns);
            for (int n = 0; n < sum.Size; n++)
            {
                sum[n] = this[n] + matrix[n];
            }
            return sum;
        }
        public Matrix<T> Subtract(Matrix<T> matrix)
        {
            if (Rows != matrix.Rows || Columns != matrix.Columns)
                throw new InvalidOperationException();

            if (Field != matrix.Field)
                throw new InvalidOperationException();

            Matrix<T> difference = new Matrix<T>(Field, Rows, Columns);
            for (int n = 0; n < difference.Size; n++)
            {
                difference[n] = this[n] - matrix[n];
            }
            return difference;
        }
        public Matrix<T> Scale(T scalar)
        {
            Matrix<T> matrix = new Matrix<T>(Field, Rows, Columns);
            for (int n = 0; n < matrix.Size; n++)
            {
                matrix[n] = this[n] * scalar;
            }
            return matrix;
        }

        public override bool Equals(object obj)
        {
            if (obj is Matrix<T> matrix)
                return this == matrix;
            return false;
        }
        public override int GetHashCode()
        {
            int baseHash = base.GetHashCode();
            return new { baseHash, Field }.GetHashCode();
        }

        public static Matrix<T> FromArray(BaseField<T> field, Array data)
        {
            if (field == null)
                throw new ArgumentNullException();

            if (data.Rank != 2)
                throw new RankException();

            int rows = data.GetLength(0);
            int columns = data.GetLength(1);

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
        public static Matrix<T> FromMultiarray(BaseField<T> field, Multiarray<T> array)
        {
            if (field == null)
                throw new ArgumentNullException();

            if (!array.IsMatrix())
                throw new RankException();

            return new Matrix<T>(field, array);
        }

        public static Matrix<T> Filled(BaseField<T> field, int rows, int columns, T value)
        {
            if (field == null)
                throw new ArgumentNullException();

            Matrix<T> matrix = new Matrix<T>(field, rows, columns);
            for (int n = 0; n < matrix.Size; n++)
                matrix[n] = value;
            return matrix;
        }
        public static Matrix<T> Zeroes(BaseField<T> field, int rows, int columns)
        {
            if (field == null)
                throw new ArgumentNullException();

            return Filled(field, rows, columns, field.Zero());
        }
        public static Matrix<T> Ones(BaseField<T> field, int rows, int columns)
        {
            if (field == null)
                throw new ArgumentNullException();

            return Filled(field, rows, columns, field.One());
        }
        public static Matrix<T> Identity(BaseField<T> field, int size)
        {
            if (field == null)
                throw new ArgumentNullException();

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

        public static bool operator ==(Matrix<T> matrixA, Matrix<T> matrixB)
        {
            if (matrixA.Rows != matrixB.Rows || matrixA.Columns != matrixB.Columns)
                return false;

            if (matrixA.Field != matrixB.Field)
                return false;

            for (int n = 0; n < matrixA.Size; n++)
            {
                if (!matrixA.Field.ElementsEqual(matrixA[n], matrixB[n]))
                    return false;
            }
            return true;
        }
        public static bool operator !=(Matrix<T> matrixA, Matrix<T> matrixB)
        {
            if (matrixA.Rows != matrixB.Rows || matrixA.Columns != matrixB.Columns)
                return true;

            if (matrixA.Field != matrixB.Field)
                return true;

            for (int n = 0; n < matrixA.Size; n++)
            {
                if (!matrixA.Field.ElementsEqual(matrixA[n], matrixB[n]))
                    return true;
            }
            return false;
        }

        public static Matrix<T> operator +(Matrix<T> matrixA, Matrix<T> matrixB)
        {
            return matrixA.Add(matrixB);
        }
        public static Matrix<T> operator +(Matrix<T> matrix)
        {
            Matrix<T> positive = new Matrix<T>(matrix.Field, matrix.Rows, matrix.Columns);
            for (int n = 0; n < matrix.Size; n++)
                positive[n] = +matrix[n];
            return positive;
        }
        public static Matrix<T> operator -(Matrix<T> matrixA, Matrix<T> matrixB)
        {
            return matrixA.Subtract(matrixB);
        }
        public static Matrix<T> operator -(Matrix<T> matrix)
        {
            Matrix<T> negative = new Matrix<T>(matrix.Field, matrix.Rows, matrix.Columns);
            for (int n = 0; n < matrix.Size; n++)
                negative[n] = -matrix[n];
            return negative;
        }
        public static Matrix<T> operator *(Matrix<T> matrix, T scalar)
        {
            return matrix.Scale(scalar);
        }
        public static Matrix<T> operator *(T scalar, Matrix<T> matrix)
        {
            return matrix.Scale(scalar);
        }
        public static Matrix<T> operator /(Matrix<T> matrix, T scalar)
        {
            return matrix.Scale(scalar.Inverse());
        }
    }
}